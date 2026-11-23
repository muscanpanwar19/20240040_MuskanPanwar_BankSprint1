# Nova Premier Bank — Sprint 3 (Data Persistence & Final Delivery)

**Student Name:** Muskan Panwar  
**Student ID:** 20240040  
**Course:** IT7742 - Assessment 3: Data Persistence & Final Delivery  
**Institution:** Whitecliffe College of Technology & Innovation  

---

## 1. Project Overview & Sprint 3 Features

This repository contains the completed, production-ready release of the **Nova Premier Bank Account Management System (Sprint 3)**. Integrating domain logic from Sprint 1 and the MVC pattern from Sprint 2, Sprint 3 transitions the system into a persistent, multi-account banking platform featuring:

1. **Intra-Account Money Transfers (Task 1 & Task 4)**:
   - Coordinated money movement between an individual customer's Everyday, Investment, and Omni accounts.
   - Enforces overdraft boundaries and failed transaction penalties ($10.00 regular, $5.00 staff).
   - Strict security control preventing transfers between different customers.
   - Dedicated modal interface (`TransferForm`) with real-time ComboBox account selection and balance comparisons.

2. **Architectural Evolution & Dynamic Account Allocation (Task 3)**:
   - Full realization of the 1:N relationship between `Customer` and `List<Account>`.
   - Dynamic account creation at runtime (`CustomerController.AddAccount()` and `AddAccountForm`) supporting Everyday, Investment, and Omni accounts with custom interest and overdraft boundaries.
   - Clear architectural distinction for Bank Staff (`bool IsStaff`, `CustomerType Role`) providing a 50% discount on all transaction fees.

3. **High-Fidelity UI & Form Inheritance (Task 4)**:
   - Reusable `BaseBankForm` standardizing corporate slate-navy branding, Segoe UI typography, and notification helpers.
   - Form Inheritance implemented across `Form1`, `TransferForm`, and `AddAccountForm`.

4. **Data Persistence via Polymorphic JSON Serialization (Task 5)**:
   - Standardized `System.Text.Json` serialization using `[JsonDerivedType]` and `[JsonPolymorphic]` metadata.
   - Preserves abstract polymorphic account hierarchies (`EverydayAccount`, `InvestmentAccount`, `OmniAccount`).
   - Prevents circular graph cycles via `[JsonIgnore]` on parent references while cleanly restoring them post-deserialization.
   - Automatic execution of `SaveToFile()` on application exit (`FormClosing`) and `LoadFromFile()` on startup (`FormLoad`).

5. **Automated Unit Testing Suite (MSTest) (Task 6)**:
   - Comprehensive test suite in `BankSystem.Tests` expanded to **65 automated tests — 100% passing rate**.
   - Verified coverage of transfer paths, overdraft buffers, staff discounts, dynamic account additions, and JSON serialization roundtrips.

6. **Comprehensive Documentation Suite (Tasks 1, 3, 4, 5, 6, 7 & 8)**:
   - Gherkin feature files with Scenario Outlines and Examples tables (`docs/Task1_Gherkin_Scenarios.feature`).
   - Updated UML Class Diagram and Class Extensions Report (`docs/Task3_Class_Extensions_and_UML_Report.md`).
   - UI Navigation Flow & Sitemap Wireframes (`docs/Task4_UI_Wireframes_and_Sitemap.md`).
   - Mandatory Sequence Diagrams for Save/Load persistence (`docs/Task5_Persistence_Sequence_Diagram.md`).
   - Manual Test Plan & Results Table with Debugging Log (`docs/Task6_Manual_Test_Plan_and_Results.md`).
   - Structured XML Documentation Report in HTML (`docs/Task7_XML_Technical_Documentation_Report.html`).
   - Professional 3-page Bank Staff User Guide (`docs/Task8_Bank_Staff_User_Guide.md`).

---

## 2. Solution Structure

```
d:\20240040_MuskanPanwar_BankSprint1\
│
├── 20240040_MuskanPanwar_BankSprint1.slnx     # Visual Studio Solution File
├── README.md                                  # Comprehensive System Documentation
├── .gitignore                                 # Visual Studio / .NET Ignore Configuration
│
├── 20240040_MuskanPanwar_BankSprint1/         # Domain Model & Controller (.NET 10.0)
│   ├── Account.cs                             # Abstract Polymorphic Base Account ([JsonDerivedType])
│   ├── EverydayAccount.cs                     # Everyday Account (No interest, No overdraft, $0 fee)
│   ├── InvestmentAccount.cs                   # Investment Account (Interest, $10/$5 failed fee)
│   ├── OmniAccount.cs                         # Omni Account (Overdraft, Interest on >$1,000, $10/$5 fee)
│   ├── Customer.cs                            # Customer Model (1:N accounts, Staff status)
│   ├── CustomerType.cs                        # Customer Role Enum (Regular, Staff)
│   ├── CustomerController.cs                  # MVC Controller (CRUD, Transfers, Persistence, XML Docs)
│   ├── BankingException.cs                    # Custom Exception Hierarchy
│   └── Program.cs                             # CLI Test Harness
│
├── GUI/                                       # Windows Forms Presentation Layer (.NET 10.0 Windows)
│   ├── BaseBankForm.cs                        # Base Form for Visual/Structural Form Inheritance
│   ├── Form1.cs                               # Primary Banking Dashboard View (inherits BaseBankForm)
│   ├── Form1.Designer.cs                      # WinForms Designer layout
│   ├── TransferForm.cs                        # Dedicated Intra-Account Transfer Portal (inherits BaseBankForm)
│   ├── AddAccountForm.cs                      # Dynamic Account Creator Dialog (inherits BaseBankForm)
│   ├── Program.cs                             # WinForms Entry Point
│   └── App.config                             # Application Configuration
│
├── BankSystem.Tests/                          # Automated Unit Test Suite (MSTest, .NET 10.0)
│   ├── CustomerControllerTests.cs             # 12 Unit Tests for Controller & CRUD Operations
│   ├── EverydayAccountTests.cs                # 7 Unit Tests for Everyday Account
│   ├── InvestmentAccountTests.cs              # 8 Unit Tests for Investment Account & Fees
│   ├── OmniAccountTests.cs                    # 9 Unit Tests for Omni Account & Overdraft Boundaries
│   └── Sprint3TransferAndPersistenceTests.cs   # 19 Unit Tests for Transfers, Staff Discount & JSON Persistence
│                                              # (Total: 65 Tests - 100% Passing)
│
└── docs/                                      # Final Assessment Documentation Suite
    ├── Task1_Gherkin_Scenarios.feature        # BDD Gherkin Feature File (Outlines & Examples)
    ├── Task3_Class_Extensions_and_UML_Report.md# UML Class Diagram & Extensions Analysis
    ├── Task4_UI_Wireframes_and_Sitemap.md     # Sitemap & ASCII High-Fidelity Wireframes
    ├── Task5_Persistence_Sequence_Diagram.md  # Mandatory Sequence Diagrams (Save & Load)
    ├── Task6_Manual_Test_Plan_and_Results.md  # Test Results Table & Engineering Debugging Log
    ├── Task7_XML_Technical_Documentation_Report.html # Formatted XML Comments API Reference Report
    └── Task8_Bank_Staff_User_Guide.md         # 3-Page Operational Manual for Bank Staff
```

---

## 3. How to Build, Test, and Run

### 3.1 Running the Automated Test Suite (CLI)
Open PowerShell or Command Prompt in the repository root:
```powershell
dotnet test
```
*Expected Output:* `Passed! - Failed: 0, Passed: 65, Skipped: 0, Total: 65`

### 3.2 Running the Windows Forms Application
```powershell
dotnet run --project GUI/GUI.csproj
```
Or open `20240040_MuskanPanwar_BankSprint1.slnx` in **Visual Studio 2022/2026**, right-click `GUI`, choose **Set as Startup Project**, and press **F5**.

### 3.3 Verifying Sprint 3 Deliverables in GUI
1. **Intra-Account Transfers:** Select a customer $\rightarrow$ Click **⇄ Intra-Account Transfer...** $\rightarrow$ Choose Source and Destination $\rightarrow$ Transfer funds.
2. **Dynamic Account Creation:** Select a customer $\rightarrow$ Click **+ Add Account** $\rightarrow$ Choose account type $\rightarrow$ Specify opening deposit and interest/overdraft limits $\rightarrow$ Click **+ Open Account**.
3. **Data Persistence:** Click **💾 Save JSON** or close the form. Inspect `bank_data.json` in the application directory. Re-open the app to observe full state restoration.
