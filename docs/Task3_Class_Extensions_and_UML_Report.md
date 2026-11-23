# Task 3: Architectural Evolution Report & UML Class Diagram

**Project:** Nova Premier Bank — Sprint 3 (Data Persistence & Final Delivery)  
**Student Name:** Muskan Panwar  
**Student ID:** 20240040  
**Course:** IT7742 - Assessment 3  
**Institution:** Whitecliffe College of Technology & Innovation  

---

## 1. Architectural Overview & Sprint 3 Evolution

In Sprint 3, the Nova Premier Bank architecture evolved from a basic prototype to an enterprise-grade banking system. Building upon the Model-View-Controller (MVC) pattern established in Sprint 2, the system now features:
1. **Dynamic One-to-Many (1:N) Relationship**: A single `Customer` can own an arbitrary number of accounts (`List<Account>`), with the ability to dynamically attach new accounts at runtime via `CustomerController.AddAccount()`.
2. **Architectural Bank Staff Distinction**: Explicit encapsulation of staff privileges (`bool IsStaff` and `CustomerType Role`) providing a 50% discount on all transaction penalty fees.
3. **Polymorphic Data Persistence**: Native polymorphic serialization via `System.Text.Json` using `[JsonPolymorphic]` and `[JsonDerivedType]` metadata.
4. **Intra-Account Money Transfers**: Controller-level coordination for moving funds between accounts belonging to the same customer while strictly preventing cross-customer transfers.

---

## 2. Updated UML Class Diagram

```mermaid
classDiagram
    direction TB

    class CustomerType {
        <<enumeration>>
        Regular
        Staff
    }

    class Customer {
        -string customerNumber
        -string name
        -string contactDetails
        -bool isStaff
        -List~Account~ accounts
        +string CustomerNumber
        +string Name
        +string ContactDetails
        +bool IsStaff
        +CustomerType Role
        +List~Account~ Accounts
        +Customer(string, string, string, bool, List~Account~)
        +AddAccount(Account) void
        +GetAccount(string) Account
        +RemoveAccount(string) bool
        +PrePopulateAccounts() void
        +GetCustomerDetails() string
    }

    class Account {
        <<abstract>>
        #string accountNumber
        #decimal balance
        #string lastTransactionStatus
        #Customer owner
        +string AccountNumber
        +decimal Balance
        +string LastTransactionStatus
        +Customer Owner
        +string AccountType*
        +Account(string, decimal, Customer)
        +SetOwner(Customer) void
        +Deposit(decimal) bool
        +Withdraw(decimal)* bool
        +CalculateInterest()* decimal
        #GetFee(decimal) decimal
        +GetAccountInfo() string
    }

    class EverydayAccount {
        +string AccountType = "Everyday"
        +EverydayAccount(string, decimal, Customer)
        +Withdraw(decimal) bool
        +CalculateInterest() decimal
    }

    class InvestmentAccount {
        -decimal interestRate
        -decimal failedFee
        +decimal InterestRate
        +decimal FailedFee
        +string AccountType = "Investment"
        +InvestmentAccount(string, decimal, decimal, Customer, decimal)
        +Withdraw(decimal) bool
        +CalculateInterest() decimal
    }

    class OmniAccount {
        -decimal interestRate
        -decimal overdraftLimit
        -decimal failedFee
        +decimal InterestRate
        +decimal OverdraftLimit
        +decimal FailedFee
        +string AccountType = "Omni"
        +OmniAccount(string, decimal, decimal, decimal, Customer, decimal)
        +Withdraw(decimal) bool
        +CalculateInterest() decimal
    }

    class BankingException {
        +Account Account
        +decimal AttemptedAmount
        +BankingException(string)
        +BankingException(string, Account, decimal)
        +BankingException(string, Exception)
    }

    class InsufficientFundsException {
        +InsufficientFundsException(string)
        +InsufficientFundsException(string, Account, decimal)
    }

    class CustomerController {
        -List~Customer~ _customers
        -string _persistenceFilePath
        -JsonSerializerOptions _jsonOptions
        +int CustomerCount
        +string PersistenceFilePath
        +CustomerController()
        +InitializeDefaultCustomers() void
        +CreateCustomer(string, string, string, bool) Customer
        +GetAllCustomers() IReadOnlyList~Customer~
        +GetCustomerByNumber(string) Customer
        +UpdateCustomer(string, string, string, bool) bool
        +DeleteCustomer(string) bool
        +AddAccount(string, string, string, decimal, decimal, decimal) Account
        +Deposit(Account, decimal) bool
        +Withdraw(Account, decimal) bool
        +CalculateInterest(Account) decimal
        +Transfer(string, string, string, decimal) bool
        +Transfer(Customer, Account, Account, decimal) bool
        +ExportToJson() string
        +ImportFromJson(string) void
        +SaveToFile(string) void
        +LoadFromFile(string) bool
    }

    %% Relationships
    Customer "1" *-- "0..*" Account : owns (1:N relationship)
    Customer --> CustomerType : has role
    Account <|-- EverydayAccount : inherits
    Account <|-- InvestmentAccount : inherits
    Account <|-- OmniAccount : inherits
    Account --> Customer : owner reference
    BankingException <|-- InsufficientFundsException : inherits
    CustomerController "1" o-- "*" Customer : manages collection
    CustomerController ..> BankingException : throws
```

---

## 3. Class Extensions & Method Signature Analysis

### 3.1 Customer Class (`Customer.cs`)

| Member | Type | Access | Description / Sprint 3 Role |
|---|---|---|---|
| `CustomerNumber` | `string` | `public get/set` | Primary identification key for the customer. |
| `Name` | `string` | `public get/set` | Full legal customer name. |
| `ContactDetails` | `string` | `public get/set` | Email address or phone contact information. |
| `IsStaff` | `bool` | `public get/set` | Distinguishes Bank Staff from Regular Customers. Directly drives the 50% discount on transaction penalty fees. |
| `Role` | `CustomerType` | `public get` | Read-only property returning `CustomerType.Staff` or `CustomerType.Regular`. |
| `Accounts` | `List<Account>` | `public get/set` | Evolved 1:N container holding all polymorphic accounts owned by the customer. |
| `AddAccount(Account)` | `void` | `public` | Appends an account and sets its bidirectional `Owner` back-reference. |
| `GetAccount(string)` | `Account?` | `public` | Searches customer's accounts by unique account number. |
| `RemoveAccount(string)` | `bool` | `public` | Removes an account from the customer's collection. |

### 3.2 Account Base & Derived Classes (`Account.cs`)

* **Polymorphic Metadata:** Annotated with `[JsonPolymorphic(TypeDiscriminatorPropertyName = "$accountType")]` and `[JsonDerivedType]` mappings for `EverydayAccount`, `InvestmentAccount`, and `OmniAccount`.
* **Cycle Prevention:** `Owner` property is marked with `[JsonIgnore]` so that serialization serializes `Customer` as the aggregate root without infinite recursive reference loops.

| Method / Property | Signature | Behavior & Rules |
|---|---|---|
| `AccountType` | `public abstract string` | Returns `"Everyday"`, `"Investment"`, or `"Omni"` for UI binding and reporting. |
| `SetOwner(Customer)` | `public void` | Reconnects the parent customer reference following deserialization. |
| `GetFee(decimal)` | `protected decimal` | Returns `standardFee * 0.5m` if `owner.IsStaff == true`; else `standardFee`. |
| `EverydayAccount.Withdraw(decimal)` | `override bool` | Rejects any withdrawal exceeding balance. Overdraft not permitted ($0 fee). |
| `InvestmentAccount.Withdraw(decimal)` | `override bool` | Rejects withdrawals exceeding balance. Debits failed fee ($10 regular, $5 staff). |
| `OmniAccount.Withdraw(decimal)` | `override bool` | Permits balance to drop to `-OverdraftLimit`. Breaches debit failed fee ($10 regular, $5 staff). |

### 3.3 CustomerController Class (`CustomerController.cs`)

| Method Signature | Parameters | Return | Description |
|---|---|---|---|
| `AddAccount(...)` | `(string custId, string type, string accNum, decimal initBal, decimal rate, decimal overdraft)` | `Account` | Dynamically creates an Everyday, Investment, or Omni account at runtime, validates uniqueness across the bank, and links to customer. |
| `Transfer(...)` | `(string custId, string srcAcc, string destAcc, decimal amount)` | `bool` | Performs intra-account transfer between accounts owned by the same customer. |
| `Transfer(...)` | `(Customer cust, Account src, Account dest, decimal amount)` | `bool` | Core transfer logic validating ownership, positive amount, non-identical accounts, and executing withdrawal with overdraft/fee handling before crediting destination. |
| `SaveToFile(string?)` | `(string? filePath = null)` | `void` | Serializes the entire customer collection into indented JSON and writes to storage. |
| `LoadFromFile(string?)` | `(string? filePath = null)` | `bool` | Reads JSON, deserializes polymorphic accounts, re-establishes parent back-references, and updates controller state. |
