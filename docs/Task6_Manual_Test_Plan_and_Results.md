# Task 6: Manual Test Plan & Validation Results Table

**Project:** Nova Premier Bank — Sprint 3 (Data Persistence & Final Delivery)  
**Student Name:** Muskan Panwar  
**Student ID:** 20240040  
**Course:** IT7742 - Assessment 3  
**Institution:** Whitecliffe College of Technology & Innovation  

---

## 1. Test Strategy & Overview

This document presents the **Manual Test Plan** and **Validation Results Table** executing the Gherkin scenarios defined in **Task 1**. The test plan rigorously validates:
1. **Intra-Account Money Transfers** across Everyday, Investment, and Omni accounts.
2. **Failure States & Boundary Breaches** (Everyday overdraft rejection, Investment balance limits, and Omni approved credit limits).
3. **Staff Fee Benefit Enforcement** (50% fee discount verification on failed transfers: $5.00 charged instead of $10.00).
4. **Input Integrity & Security Controls** (prohibiting cross-customer transfers, negative/zero sums, and identical source/destination accounts).
5. **Polymorphic JSON Data Persistence** (automatic saving on exit and complete state restoration on startup).

---

## 2. Test Execution Results Table

| Scenario ID | Test Category | Target Account(s) & Inputs | Expected Result | Actual Result | Status |
|---|---|---|---|---|---|
| **SCN-01** | Successful Transfer | Everyday ($500) $\rightarrow$ Investment ($2000), Amount: $200 | Source: $300, Dest: $2200, Status: Approved | Source: $300.00, Dest: $2200.00 | **PASS** |
| **SCN-02** | Successful Transfer | Investment ($2000) $\rightarrow$ Omni ($1500), Amount: $500 | Source: $1500, Dest: $2000, Status: Approved | Source: $1500.00, Dest: $2000.00 | **PASS** |
| **SCN-03** | Successful Transfer | Omni ($1500) $\rightarrow$ Everyday ($500), Amount: $400 | Source: $1100, Dest: $900, Status: Approved | Source: $1100.00, Dest: $900.00 | **PASS** |
| **SCN-04** | Successful Transfer (Staff) | Staff Everyday ($1000) $\rightarrow$ Omni ($500), Amount: $350 | Source: $650, Dest: $850, Status: Approved | Source: $650.00, Dest: $850.00 | **PASS** |
| **SCN-05** | Successful Transfer (Staff) | Staff Investment ($3000) $\rightarrow$ Everyday ($1000), Amount: $750 | Source: $2250, Dest: $1750, Status: Approved | Source: $2250.00, Dest: $1750.00 | **PASS** |
| **SCN-06** | Successful Overdraft Transfer | Omni ($1500, Limit $500) $\rightarrow$ Everyday ($500), Amount: $1800 | Source: -$300.00, Dest: $2300, Within overdraft limit | Source: -$300.00, Dest: $2300.00 | **PASS** |
| **SCN-07** | Transfer Failure (Everyday) | Everyday ($500) $\rightarrow$ Investment ($2000), Amount: $600 | Overdraft rejected; No fee deducted; Balance: $500 | InsufficientFundsException thrown; Balance: $500.00 | **PASS** |
| **SCN-08** | Transfer Failure (Investment - Regular) | Investment ($2000) $\rightarrow$ Everyday ($500), Amount: $2500 | Rejected; Standard $10.00 fee charged; Source: $1990 | InsufficientFundsException; Fee $10.00 charged; Source: $1990.00 | **PASS** |
| **SCN-09** | Transfer Failure (Omni - Regular) | Omni ($1500, Limit $500) $\rightarrow$ Everyday ($500), Amount: $2050 | Overdraft limit breached; $10.00 fee charged; Source: $1490 | InsufficientFundsException; Fee $10.00 charged; Source: $1490.00 | **PASS** |
| **SCN-10** | Transfer Failure (Everyday - Staff) | Staff Everyday ($500) $\rightarrow$ Investment ($2000), Amount: $750 | Overdraft rejected; $0 fee charged; Balance: $500 | InsufficientFundsException; Balance: $500.00 | **PASS** |
| **SCN-11** | Transfer Failure (Investment - Staff) | Staff Investment ($2000) $\rightarrow$ Everyday ($500), Amount: $3000 | 50% discount fee ($5.00) charged; Source: $1995.00 | InsufficientFundsException; Fee $5.00 charged; Source: $1995.00 | **PASS** |
| **SCN-12** | Transfer Failure (Omni - Staff) | Staff Omni ($1500, Limit $500) $\rightarrow$ Everyday ($500), Amount: $2200 | 50% discount fee ($5.00) charged; Source: $1495.00 | InsufficientFundsException; Fee $5.00 charged; Source: $1495.00 | **PASS** |
| **SCN-13** | Staff Fee Discount Logic | Regular Investment failed withdrawal | Standard fee $10.00 applied | Exact fee $10.00 deducted | **PASS** |
| **SCN-14** | Staff Fee Discount Logic | Regular Omni failed overdraft breach | Standard fee $10.00 applied | Exact fee $10.00 deducted | **PASS** |
| **SCN-15** | Staff Fee Discount Logic | Staff Investment failed withdrawal | 50% discount fee ($5.00) applied | Exact fee $5.00 deducted | **PASS** |
| **SCN-16** | Staff Fee Discount Logic | Staff Omni failed overdraft breach | 50% discount fee ($5.00) applied | Exact fee $5.00 deducted | **PASS** |
| **SCN-17** | Input Validation | Source == Destination (`ED-20240040` $\rightarrow$ `ED-20240040`) | BankingException: "Source and destination accounts cannot be the same" | BankingException caught and displayed | **PASS** |
| **SCN-18** | Input Validation | Negative Amount (-$50.00) | BankingException: "Amount must be positive" | BankingException caught and displayed | **PASS** |
| **SCN-19** | Input Validation | Zero Amount ($0.00) | BankingException: "Amount must be positive" | BankingException caught and displayed | **PASS** |
| **SCN-20** | Security Validation | Transfer between different customers (`20240040` $\rightarrow$ `STAFF01`) | Security restriction: "Transfers are strictly intra-account" | BankingException caught and displayed | **PASS** |
| **SCN-21** | Dynamic Account Creation | Add Everyday Account at runtime | Account appended to `customer.Accounts`; 1:N updated | Account created and dropdown updated | **PASS** |
| **SCN-22** | Dynamic Account Creation | Add Investment Account with interest | Account created with custom interest percentage | Verified interest calculation on new account | **PASS** |
| **SCN-23** | Dynamic Account Creation | Add Omni Account with overdraft limit | Account created with custom overdraft boundary | Verified overdraft usage on new account | **PASS** |
| **SCN-24** | Dynamic Account Validation | Duplicate account number across bank | Rejected with ArgumentException | ArgumentException caught with warning alert | **PASS** |
| **SCN-25** | JSON Persistence | SaveToFile on FormClosing | `bank_data.json` generated with `$accountType` discriminator | Formatted JSON written to storage file | **PASS** |
| **SCN-26** | JSON Persistence | LoadFromFile on FormLoad | Full state restored; child types preserved; parent back-references restored | All customers and polymorphic accounts restored | **PASS** |

---

## 3. Engineering Debugging Log

During the implementation and automated testing of Sprint 3, the following challenges were identified and systematically resolved:

### Incident 1: Circular Object Reference in JSON Serialization
* **Date:** 2026-11-23
* **Component:** `System.Text.Json` Serialization / `Customer` & `Account`
* **Root Cause:** In the domain architecture, `Customer` owns a collection `List<Account>`, while each `Account` retains a back-reference `Customer Owner`. During serialization, `JsonSerializer.Serialize()` encountered an infinite cycle loop: `Customer` $\rightarrow$ `Accounts` $\rightarrow$ `Owner` $\rightarrow$ `Accounts` $\rightarrow$ ...
* **Resolution:** 
  1. Annotated `Account.Owner` with `[JsonIgnore]`, making `Customer` the explicit aggregate root for serialization.
  2. Implemented `account.SetOwner(Customer)` in the deserialization constructor and within `CustomerController.ImportFromJson()`. Upon deserialization, the controller traverses all customer accounts and reconnects `Owner = customer`, guaranteeing flawless bidirectional reference integrity without cycles.

### Incident 2: Polymorphic Deserialization of Abstract `Account` Class
* **Date:** 2026-11-23
* **Component:** `Account` Inheritance Hierarchy / `System.Text.Json`
* **Root Cause:** Because `Customer.Accounts` is declared as `List<Account>` where `Account` is abstract, standard deserializers cannot determine which concrete derived class (`EverydayAccount`, `InvestmentAccount`, or `OmniAccount`) to instantiate.
* **Resolution:** 
  1. Added native .NET 7+ polymorphic serialization attributes to `Account.cs`:
     ```csharp
     [JsonPolymorphic(TypeDiscriminatorPropertyName = "$accountType")]
     [JsonDerivedType(typeof(EverydayAccount), typeDiscriminator: "Everyday")]
     [JsonDerivedType(typeof(InvestmentAccount), typeDiscriminator: "Investment")]
     [JsonDerivedType(typeof(OmniAccount), typeDiscriminator: "Omni")]
     ```
  2. Added `[JsonConstructor]` attributes to each concrete subclass constructor.
  3. Validated via unit test `JsonPersistence_SaveAndLoad_RestoresCompleteSystemState()` that all reloaded objects preserve their concrete types and specific attributes (`InterestRate`, `OverdraftLimit`, `FailedFee`).

### Incident 3: Preventing Self-Transfers on Intra-Account Portal
* **Date:** 2026-11-23
* **Component:** `CustomerController.Transfer()` & `TransferForm`
* **Root Cause:** Early prototypes allowed users to select the same account in both the Source and Destination dropdowns. Executing a self-transfer resulted in a withdrawal followed by an immediate deposit on the same account, generating confusing double audit log entries.
* **Resolution:**
  1. Implemented a strict defensive validation rule in `CustomerController.Transfer()`:
     ```csharp
     if (string.Equals(sourceAccount.AccountNumber, destinationAccount.AccountNumber, StringComparison.OrdinalIgnoreCase))
     {
         throw new BankingException("Transfer failed: Source and destination accounts cannot be the same.");
     }
     ```
  2. Added client-side validation in `TransferForm.BtnTransfer_Click()` alerting the user immediately before calling the controller.

### Incident 4: Automated Form-Closing Persistence Race Condition
* **Date:** 2026-11-23
* **Component:** `Form1_FormClosing` Event
* **Root Cause:** Closing the form while a modal transfer or account dialog was open could trigger dispose before the controller finished saving.
* **Resolution:** Wrapped `_controller.SaveToFile()` inside a robust `try-catch` block on `Form1_FormClosing` with defensive file existence checks, ensuring state is guaranteed to be saved cleanly to disk on application exit.
