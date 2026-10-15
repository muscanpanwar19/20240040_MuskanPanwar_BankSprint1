# Nova Premier Bank - Sprint 2 (Customer Management & Reliability)

**Student Name:** Muskan Panwar  
**Student ID:** 20240040  
**Course:** IT7742 - Assessment 2: Customer Management & Reliability  
**Institution:** Whitecliffe College of Technology & Innovation  

---

## 1. Project Overview & Sprint 2 Features

This repository contains **Sprint 2** of the **Nova Premier Bank** system prototype. Building directly upon the foundation established in Sprint 1, Sprint 2 enhances architecture, maintainability, and reliability through:

1. **Model-View-Controller (MVC) Architecture**:
   - Decoupled business logic and customer state from the presentation layer.
   - Introduced `CustomerController` to manage customer collections and coordinate banking operations.
   - The user interface (View) handles only user inputs and visual rendering; direct manipulation of customer collections inside the Form is strictly eliminated.

2. **Customer Information Management (CRUD)**:
   - Full **Create, Read, Update, and Delete** functionality implemented in both the controller and the branded Windows Forms GUI.
   - Dynamic selection of customers with immediate synchronization across account balances, rules, and transaction history.

3. **Hardened Reliability & Custom Exceptions**:
   - Implemented custom `BankingException` and `InsufficientFundsException` classes.
   - Exception handling tailored to account types:
     - **Everyday Account**: Rejects overdrafts with a clear message that overdrafts are not permitted.
     - **Investment Account**: Charges a failed transaction fee ($10 regular, $5 bank staff with 50% discount) and provides feedback with remaining balance.
     - **Omni Account**: Allows overdraft up to the limit; triggers a tailored exception and charges a failed transaction fee ($10 regular, $5 staff) when exceeded.
   - Business validation ensuring transaction amounts must be strictly positive.

4. **Automated Unit Testing Suite (MSTest)**:
   - Comprehensive test project (`BankSystem.Tests`) covering all business logic, boundary conditions, edge cases, fee calculations, interest rules, and controller CRUD operations.
   - **46 tests implemented — 100% passing rate**.

---

## 2. Solution Structure

```
d:\20240040_MuskanPanwar_BankSprint1\
│
├── 20240040_MuskanPanwar_BankSprint1.slnx     # Visual Studio Solution File
├── README.md                                  # Project Documentation & Run Guide
│
├── 20240040_MuskanPanwar_BankSprint1/         # Domain Model & Controller Class Library (.NET 10.0)
│   ├── Account.cs                             # Abstract Base Account
│   ├── EverydayAccount.cs                     # Everyday Account (No interest, No overdraft, No fee)
│   ├── InvestmentAccount.cs                   # Investment Account (Variable interest, Failed fee)
│   ├── OmniAccount.cs                         # Omni Account (Overdraft, Interest on >$1,000, Failed fee)
│   ├── Customer.cs                            # Customer Model
│   ├── CustomerType.cs                        # Customer Type Enumeration (Regular, Staff)
│   ├── CustomerController.cs                  # MVC Controller managing CRUD & business transactions
│   ├── BankingException.cs                    # Custom Exception Hierarchy
│   └── Program.cs                             # CLI Test Harness & Console Demonstration
│
├── GUI/                                       # Windows Forms Presentation Layer (.NET Framework 4.7.2)
│   ├── Form1.cs                               # Code-Behind (View) interfacing exclusively with Controller
│   ├── Form1.Designer.cs                      # Branded UI Designer (Optimized for Laptop Displays)
│   ├── Program.cs                             # WinForms Entry Point
│   └── App.config                             # Application Configuration
│
└── BankSystem.Tests/                          # Automated Unit Test Suite (MSTest, .NET 10.0)
    ├── EverydayAccountTests.cs                # 7 Unit Tests for Everyday Account
    ├── InvestmentAccountTests.cs              # 8 Unit Tests for Investment Account & Fee Application
    ├── OmniAccountTests.cs                    # 9 Unit Tests for Omni Account & Boundary Cases
    └── CustomerControllerTests.cs             # 12 Unit Tests for MVC Controller & CRUD Operations
```

---

## 3. Prerequisites & Dependencies

* **Operating System**: Windows 10 or Windows 11
* **IDE**: Microsoft Visual Studio 2022 / 2026 (Community, Professional, or Enterprise)
  * Workloads required:
    * **.NET desktop development** (includes Windows Forms and MSBuild)
* **SDK**: .NET SDK 10.0 (or .NET 8.0) and .NET Framework 4.7.2 targeting pack

---

## 4. How to Open the Solution in Visual Studio

1. Open **Visual Studio**.
2. Click **File** $\rightarrow$ **Open** $\rightarrow$ **Project/Solution...**
3. Navigate to `d:\20240040_MuskanPanwar_BankSprint1\` and select `20240040_MuskanPanwar_BankSprint1.slnx` (or open via Solution Explorer).
4. Wait for Visual Studio to load and restore all project dependencies.

---

## 5. How to Run the GUI Application

1. In the **Solution Explorer** panel, right-click on the **GUI** project.
2. Select **Set as Startup Project**.
3. Press **F5** (or click the green **Start / Debug** button on the top toolbar).
4. **Using the GUI**:
   - **Select Customer**: Choose an existing customer from the dropdown (e.g., Muskan Panwar or Rakshika) to populate details.
   - **Create Customer**: Click *Reset Form*, type an ID (e.g., `CUST03`), Full Name, and Contact, then click **Add Customer**. Standard Everyday, Investment, and Omni accounts are automatically initialized.
   - **Update Details**: Change the name, contact, or staff checkbox, and click **Update Details**.
   - **Delete Customer**: Select a customer and click **Delete Customer** (with confirmation dialog).
   - **Execute Transactions**: Select an account, input an amount, and click **+ Deposit**, **- Withdraw**, or **% Calculate & Apply Interest**. All exceptions (e.g., exceeding balance or overdraft limits) will trigger friendly popup warnings, red status indicators, and audit logging.

---

## 6. How to Run the Automated Unit Tests

### In Visual Studio (Test Explorer)
1. Open the top menu: **Test** $\rightarrow$ **Test Explorer** (or press `Ctrl + E, T`).
2. Click the **Run All Tests in View** icon (double green play button `▶▶`).
3. All **46 tests** will execute and display green checkmarks (`✔`).

### In the Terminal (PowerShell / CLI)
Run the following command from the repository root:

```powershell
dotnet test BankSystem.Tests\BankSystem.Tests.csproj
```

To view individual test names and execution duration:
```powershell
dotnet test BankSystem.Tests\BankSystem.Tests.csproj --logger "console;verbosity=detailed"
```

---

## 7. Key Unit Test Scenarios & Edge Cases Tested

| Test Suite | Method Tested | Test Type | Description / Edge Case |
|---|---|---|---|
| `EverydayAccountTests` | `Withdraw` | Happy Path | Successful withdrawal within positive balance |
| `EverydayAccountTests` | `Withdraw` | Edge Case | **Exact balance withdrawal** ($500.00 from $500.00 $\rightarrow$ leaves $0.00) |
| `EverydayAccountTests` | `Withdraw` | Edge Case | **$0.00 withdrawal** $\rightarrow$ throws `BankingException` |
| `EverydayAccountTests` | `Withdraw` | Failure Path | **Exceeding balance** $\rightarrow$ throws tailored `InsufficientFundsException` |
| `InvestmentAccountTests` | `Withdraw` | Failure Path | Regular customer overdraws $\rightarrow$ **Charges $10.00 standard fee** and throws tailored exception |
| `InvestmentAccountTests` | `Withdraw` | Failure Path | Bank staff overdraws $\rightarrow$ **Charges $5.00 discounted fee (50% off)** and throws tailored exception |
| `InvestmentAccountTests` | `CalculateInterest` | Business Logic | Applies 4.0% interest to positive balances; 0% if balance is non-positive |
| `OmniAccountTests` | `Withdraw` | Boundary Case | Withdraws up to the exact combined limit (Balance + Overdraft Limit) |
| `OmniAccountTests` | `Withdraw` | **Edge Case** | **Exceeding overdraft limit by exactly $0.01** $\rightarrow$ charges fee and triggers tailored exception |
| `OmniAccountTests` | `Withdraw` | Failure Path | Bank staff exceeds overdraft $\rightarrow$ applies 50% discount to fee ($5.00) |
| `OmniAccountTests` | `CalculateInterest` | Business Logic | **5.0% interest applied only to balance portion exceeding $1,000**; $0 interest if balance $\le \$1,000$ |
| `CustomerControllerTests` | `CreateCustomer` | CRUD / Validation | Rejects duplicate IDs and blank names; pre-populates default account set |
| `CustomerControllerTests` | `GetAllCustomers` | MVC Encapsulation | Returns read-only collection preventing View from mutating domain list |
| `CustomerControllerTests` | `UpdateCustomer` | CRUD | Modifies existing customer details in-place |
| `CustomerControllerTests` | `DeleteCustomer` | CRUD | Removes customer and associated accounts |
| `CustomerControllerTests` | `Withdraw` | Transaction Routing | Controller safely routes withdrawal and propagates custom exceptions |

---

## 8. Definition of Done Checklist for Sprint 2

- [x] Model-View-Controller (MVC) architectural pattern fully implemented.
- [x] `CustomerController` handles all CRUD operations and data retrieval.
- [x] `Form1` (View) does NOT store any `List<Customer>` field directly.
- [x] Branded Windows Form with consistent Nova Premier Bank styling and compact height (595px) for laptop screens.
- [x] Custom `BankingException` and `InsufficientFundsException` hierarchy created with tailored account-specific messages.
- [x] Automated Unit Test suite implemented with MSTest (46 / 46 passing).
- [x] Edge cases verified: exact balance, $0.00 withdrawal, exceeding overdraft by $0.01.
- [x] Mandatory README.md included with step-by-step build, run, and test instructions.
