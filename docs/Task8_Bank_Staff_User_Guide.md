# NOVA PREMIER BANK — BANK STAFF OPERATIONAL USER GUIDE
**Internal Operational Manual | Assessment 3 Final Delivery**  
**Student Name:** Muskan Panwar &bull; **ID:** 20240040 &bull; **Institution:** Whitecliffe College of Technology & Innovation  

---

<!-- PAGE 1 BREAK -->
# PAGE 1: Customer Management & Dynamic Account Assignment

### 1.1 Welcome & System Overview
The **Nova Premier Bank Management System** is a mission-critical desktop application designed for bank staff to oversee customer accounts, execute financial transactions, coordinate intra-account fund transfers, and maintain persistent audit logs.

```
+========================================================================================================+
|  NOVA PREMIER BANK                                              Active: Muskan Panwar (20240040)       |
|  Sprint 3: JSON Persistence | Form Inheritance | Intra-Account Transfers     [Role: Regular Customer]  |
+========================================================================================================+
```

### 1.2 Creating a New Customer Record
To register a new customer:
1. Click the **Reset Form** button in the *Customer Management* panel.
2. Enter a unique **Customer ID** (e.g., `CUST05`), the customer's **Full Name**, and **Contact Details** (email or phone).
3. If the individual is an employee of Nova Premier Bank, check **Staff (50% Discount)**. This entitles them to institutional fee waivers.
4. Click **Add Customer**. The system automatically creates the customer profile and generates their initial three standard accounts:
   - **Everyday Account:** `ED-CUST05` ($500.00 opening balance)
   - **Investment Account:** `INV-CUST05` ($2000.00 opening balance, 4.0% interest)
   - **Omni Account:** `OMN-CUST05` ($1500.00 opening balance, $500.00 overdraft limit)
5. A green confirmation badge will appear, and the customer will immediately become active in the top dropdown.

```
+--------------------------------------------------------------------------------------------------------+
| [SCREENSHOT 1: Customer Management Form & Pre-Populated Account Allocation]                            |
|                                                                                                        |
|  Select Customer: [ CUST05 - Jane Doe [Staff]                                                    v ]   |
|  Customer ID:     [ CUST05      ]   Full Name: [ Jane Doe             ]   [x] Staff (50% Discount)     |
|  Contact:         [ jane.doe@novabank.com                                                        ]     |
|  [ + Add Customer ]    [ Update Details ]    [ Delete Customer ]    [ Reset Form ]                     |
|  STATUS: Customer 'Jane Doe' added successfully with standard accounts. Total Accounts: 3.             |
+--------------------------------------------------------------------------------------------------------+
```

### 1.3 Dynamically Adding Accounts at Runtime (1:N Expansion)
Nova Premier Bank allows customers to hold multiple accounts of any type:
1. Select the target customer from the dropdown.
2. Click **+ Add Account** located in the *Account Overview & Allocation* panel.
3. The dedicated, branded **Add New Account** window will open.
4. Choose the desired **Account Type**:
   - **Everyday Account:** Checking facility for daily use ($0 fee, $0 overdraft, 0% interest).
   - **Investment Account:** Long-term deposit earning compound interest. Enter the custom annual **Interest Rate (%)**.
   - **Omni Account:** Flexible account with credit buffer. Specify both the **Interest Rate (%)** and the approved **Overdraft Limit ($)**.
5. Review the auto-suggested account number and opening deposit, then click **+ Open Account**.
6. The new account is instantly bound to the customer profile.

---

<!-- PAGE 2 BREAK -->
# PAGE 2: Performing Intra-Account Money Transfers

### 2.1 Intra-Account Transfer Overview
Intra-account money transfers allow an individual customer to transfer funds between their own accounts (e.g., moving savings from an Everyday account into an Investment account).
> **Security Policy:** For regulatory and security reasons, cross-customer transfers are strictly disallowed. Customers can only transfer funds between accounts they personally own.

```
+--------------------------------------------------------------------------------------------------------+
| [SCREENSHOT 2: Dedicated Intra-Account Transfer Portal (TransferForm)]                                 |
|                                                                                                        |
|  Active Customer: Muskan Panwar (ID: 20240040)                      Role: Regular Customer             |
|  +--------------------------------------------------------------------------------------------------+  |
|  | Transfer Details (Intra-Account Only)                                                            |  |
|  | Source Account (Debit):       [ ED-20240040 (Everyday) - $500.00            v ]  Avail: $500.00   |  |
|  | Destination Account (Credit): [ INV-20240040 (Investment) - $2000.00        v ]  Avail: $2000.00  |  |
|  | Transfer Amount ($):          [ 200.00       ]                                                   |  |
|  |                                                                                                  |  |
|  | [i] Transfers are strictly intra-account. Overdraft limits and failed fees apply to source.      |  |
|  +--------------------------------------------------------------------------------------------------+  |
|                                                     [ ⇄ Execute Transfer ]      [ Cancel / Close ]     |
+--------------------------------------------------------------------------------------------------------+
```

### 2.2 Step-by-Step Transfer Execution Guide
1. Select the active customer from the primary customer dropdown.
2. In the *Financial Operations* group, click the blue **⇄ Intra-Account Transfer...** button.
3. The inherited **TransferForm** dialog will open:
   - **Step 1: Select Source Account.** Choose the account from which funds will be debited. The current available balance updates in real-time.
   - **Step 2: Select Destination Account.** Choose the receiving account. Note: The system will disallow selecting the same account as both source and destination.
   - **Step 3: Specify Transfer Amount.** Input a positive dollar amount (e.g., `200.00`).
4. Click **⇄ Execute Transfer**:
   - The system validates that available balance or approved overdraft covers the transfer.
   - The source account is debited, and the destination account is credited.
   - Both account balances update dynamically across all UI displays.
   - A success popup summarizes new balances, and an audit trail entry is logged.

### 2.3 Regular Daily Operations (Deposits, Withdrawals & Interest)
* **Depositing Funds:** Select an account, input the amount, and click **+ Deposit**. The balance updates immediately.
* **Withdrawing Funds:** Select an account, input the amount, and click **- Withdraw**. The system verifies funds and overdraft limits.
* **Applying Interest:** Click **% Apply Interest**. The system calculates variable interest according to account rules:
  - *Everyday:* 0% interest applied.
  - *Investment:* Applied on entire positive balance (e.g., 4.0%).
  - *Omni:* Applied strictly on the portion of the balance exceeding $1,000.00.

---

<!-- PAGE 3 BREAK -->
# PAGE 3: Error Handling, Overdraft Limits & Data Persistence

### 3.1 Understanding Account Rules & Error Messages
When an operation breaches banking regulations, the system displays descriptive error alerts and logs the event in the audit console:

| Account Type | Overdraft Allowed? | Failed Transaction Penalty | Error Dialog Example |
|---|---|---|---|
| **Everyday** | ❌ No ($0.00 buffer) | $0.00 (No fee charged) | `Everyday Account withdrawal failed: Insufficient funds. Requested $600.00, but available balance is $500.00. Overdraft is not permitted.` |
| **Investment** | ❌ No ($0.00 buffer) | **$10.00** ($5.00 for Staff) | `Investment Account withdrawal failed: Insufficient funds. Requested $2500.00, but available balance was $2000.00. A failed transaction fee of $10.00 has been charged. Remaining balance is $1990.00.` |
| **Omni** | ✅ Yes (up to limit) | **$10.00** ($5.00 for Staff) | `Omni Account withdrawal failed: Overdraft limit exceeded. Requested $2200.00, but maximum available with overdraft limit of $500.00 was $2000.00. A failed fee of $10.00 has been charged.` |

```
+--------------------------------------------------------------------------------------------------------+
| [SCREENSHOT 3: Descriptive Warning Alert & Staff Benefit Application]                                  |
|                                                                                                        |
|  +--------------------------------------------------------------------------------------------------+  |
|  |  (X) Transaction Declined — Insufficient Funds                                                   |  |
|  |  Omni Account withdrawal failed: Overdraft limit exceeded. Requested $2200.00,                  |  |
|  |  but maximum available with overdraft limit of $500.00 was $2000.00.                             |  |
|  |  A failed transaction fee of $5.00 has been charged (Staff 50% Benefit applied).                 |  |
|  |  Remaining balance is $1495.00.                                                                  |  |
|  |                                                         [ OK ]                                   |  |
|  +--------------------------------------------------------------------------------------------------+  |
+--------------------------------------------------------------------------------------------------------+
```

### 3.2 Bank Staff Benefit: 50% Fee Waiver
When a customer is flagged as **Bank Staff**, the system automatically applies a 50% discount on all penalty fees:
* **Standard Customer Penalty Fee:** $10.00
* **Staff Member Penalty Fee:** **$5.00** (50% institutional discount)
* The discount is calculated automatically by `Account.GetFee()` without manual staff intervention.

### 3.3 Data Persistence Management (JSON Serialization)
Nova Premier Bank automatically preserves the entire system state to disk:
* **Automatic Save on Exit:** When bank staff closes the application (`FormClosing`), the system automatically serializes all customer profiles and polymorphic accounts into `bank_data.json`.
* **Automatic Load on Launch:** When launched (`FormLoad`), the system checks for `bank_data.json` and restores all customers, accounts, balances, interest rates, and overdraft limits.
* **Manual Persistence Controls:**
  - Click **💾 Save JSON** in the lower-right corner to take an immediate snapshot of the system state.
  - Click **📂 Load JSON** to restore the state from disk at any time.

---
**End of Staff User Guide &bull; Nova Premier Bank &bull; All Rights Reserved**
