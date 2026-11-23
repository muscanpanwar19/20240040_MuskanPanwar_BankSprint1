using System;
using System.Drawing;
using System.Windows.Forms;
using _20240040_MuskanPanwar_BankSprint1;

namespace GUI
{
    /// <summary>
    /// Primary Application Window for Nova Premier Bank.
    /// Demonstrates Form Inheritance by deriving from BaseBankForm.
    /// Acts strictly as the View layer in the MVC architecture, delegating all operations to CustomerController.
    /// </summary>
    public partial class Form1 : BaseBankForm
    {
        // MVC Architecture: Form (View) only holds a reference to the Controller.
        // Direct storage or manipulation of List<Customer> is strictly prohibited inside the Form.
        private readonly CustomerController _controller;
        private Customer? _selectedCustomer;

        public Form1()
        {
            InitializeComponent();
            _controller = new CustomerController();
        }

        // =========================================================================
        // LIFECYCLE & DATA PERSISTENCE (TASK 5)
        // =========================================================================

        private void Form1_Load(object sender, EventArgs e)
        {
            AddHistoryLog("=== Nova Premier Bank System Initialized (Sprint 3) ===");

            // Task 5: Automatically trigger Load() during initial application startup
            try
            {
                bool loaded = _controller.LoadFromFile();
                if (loaded)
                {
                    AddHistoryLog($"[PERSISTENCE] System state loaded from '{_controller.PersistenceFilePath}' ({_controller.CustomerCount} customers restored).");
                }
                else
                {
                    AddHistoryLog($"[PERSISTENCE] No existing '{_controller.PersistenceFilePath}' detected. Seeded default demonstration customer records.");
                }
            }
            catch (Exception ex)
            {
                AddHistoryLog($"[PERSISTENCE ERROR] Failed to load data file: {ex.Message}");
                ShowError($"Failed to restore saved data: {ex.Message}\nStarting with default records.", "Persistence Warning");
            }

            RefreshCustomerDropdown();
        }

        // Task 5: Automatically trigger Save() during FormClosing event
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                _controller.SaveToFile();
                AddHistoryLog($"[PERSISTENCE AUTO-SAVE] Successfully saved {_controller.CustomerCount} customers to '{_controller.PersistenceFilePath}'.");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to auto-save application state on exit:\n{ex.Message}", "Auto-Save Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSaveState_Click(object sender, EventArgs e)
        {
            try
            {
                _controller.SaveToFile();
                AddHistoryLog($"[MANUAL SAVE] Persisted {_controller.CustomerCount} customers to '{_controller.PersistenceFilePath}'.");
                ShowSuccess($"All system records successfully serialized to '{_controller.PersistenceFilePath}'.", "Data Saved");
            }
            catch (Exception ex)
            {
                ShowError("Error saving system state: " + ex.Message, "Save Failed");
            }
        }

        private void btnReloadState_Click(object sender, EventArgs e)
        {
            try
            {
                bool ok = _controller.LoadFromFile();
                if (ok)
                {
                    RefreshCustomerDropdown();
                    AddHistoryLog($"[MANUAL LOAD] Reloaded {_controller.CustomerCount} customers from '{_controller.PersistenceFilePath}'.");
                    ShowSuccess($"System state successfully reloaded ({_controller.CustomerCount} customers).", "Data Loaded");
                }
                else
                {
                    ShowWarning($"Persistence file '{_controller.PersistenceFilePath}' does not exist yet.", "File Not Found");
                }
            }
            catch (Exception ex)
            {
                ShowError("Error reloading system state: " + ex.Message, "Load Failed");
            }
        }

        // =========================================================================
        // CUSTOMER DATA BINDING & SELECTION (READ)
        // =========================================================================

        private void RefreshCustomerDropdown(string? selectCustomerNumber = null)
        {
            cboCustomers.Items.Clear();

            var customers = _controller.GetAllCustomers();
            if (customers.Count == 0)
            {
                _selectedCustomer = null;
                ClearCustomerFields();
                cboAccounts.Items.Clear();
                ResetAccountDisplay();
                lblActiveCustomer.Text = "No Customers Registered";
                lblRoleBadge.Text = "Role: None";
                lblCustMessage.Text = "No customer records exist. Use the form above to add a customer.";
                return;
            }

            int selectedIndex = 0;
            for (int i = 0; i < customers.Count; i++)
            {
                Customer c = customers[i];
                string roleTag = c.IsStaff ? " [Staff]" : " [Regular]";
                cboCustomers.Items.Add($"{c.CustomerNumber} - {c.Name}{roleTag}");

                if (selectCustomerNumber != null && string.Equals(c.CustomerNumber, selectCustomerNumber, StringComparison.OrdinalIgnoreCase))
                {
                    selectedIndex = i;
                }
            }

            cboCustomers.SelectedIndex = selectedIndex;
        }

        private void cboCustomers_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboCustomers.SelectedIndex < 0) return;

            var customers = _controller.GetAllCustomers();
            if (cboCustomers.SelectedIndex < customers.Count)
            {
                _selectedCustomer = customers[cboCustomers.SelectedIndex];
                PopulateCustomerFields(_selectedCustomer);
                PopulateAccountDropdown();
                UpdateHeaderInfo();
            }
        }

        private void PopulateCustomerFields(Customer customer)
        {
            if (customer == null) return;

            txtCustId.Text = customer.CustomerNumber;
            txtCustId.ReadOnly = true; // Primary key locked during edit
            txtCustName.Text = customer.Name;
            txtCustContact.Text = customer.ContactDetails;
            chkCustStaff.Checked = customer.IsStaff;

            lblCustMessage.ForeColor = Color.FromArgb(71, 85, 105);
            lblCustMessage.Text = $"Viewing record for {customer.Name} ({customer.CustomerNumber}). Total Accounts: {customer.Accounts.Count}.";
        }

        private void UpdateHeaderInfo()
        {
            if (_selectedCustomer == null) return;

            lblActiveCustomer.Text = $"Active: {_selectedCustomer.Name} ({_selectedCustomer.CustomerNumber})";
            if (_selectedCustomer.IsStaff)
            {
                lblRoleBadge.Text = "Role: Bank Staff (50% Fee Discount)";
                lblRoleBadge.ForeColor = Color.FromArgb(245, 158, 11);
            }
            else
            {
                lblRoleBadge.Text = "Role: Regular Customer";
                lblRoleBadge.ForeColor = Color.FromArgb(148, 163, 184);
            }
        }

        // =========================================================================
        // CUSTOMER CRUD OPERATIONS
        // =========================================================================

        private void btnCustCreate_Click(object sender, EventArgs e)
        {
            string id = txtCustId.Text.Trim();
            string name = txtCustName.Text.Trim();
            string contact = txtCustContact.Text.Trim();
            bool isStaff = chkCustStaff.Checked;

            try
            {
                Customer newCust = _controller.CreateCustomer(id, name, contact, isStaff);
                AddHistoryLog($"[CRUD CREATE] Successfully created customer {newCust.Name} ({newCust.CustomerNumber}).");

                lblCustMessage.ForeColor = Color.FromArgb(21, 128, 61);
                lblCustMessage.Text = $"Customer '{newCust.Name}' added successfully with standard accounts.";

                RefreshCustomerDropdown(newCust.CustomerNumber);
            }
            catch (ArgumentException ex)
            {
                ShowWarning(ex.Message, "Validation Error");
                lblCustMessage.ForeColor = Color.FromArgb(185, 28, 28);
                lblCustMessage.Text = "Validation Failed: " + ex.Message;
            }
            catch (InvalidOperationException ex)
            {
                ShowError(ex.Message, "Duplicate Error");
                lblCustMessage.ForeColor = Color.FromArgb(185, 28, 28);
                lblCustMessage.Text = ex.Message;
            }
            catch (Exception ex)
            {
                ShowError("Unexpected error creating customer: " + ex.Message, "Error");
            }
        }

        private void btnCustUpdate_Click(object sender, EventArgs e)
        {
            string id = txtCustId.Text.Trim();
            string name = txtCustName.Text.Trim();
            string contact = txtCustContact.Text.Trim();
            bool isStaff = chkCustStaff.Checked;

            if (string.IsNullOrWhiteSpace(id))
            {
                ShowWarning("Please select a customer to update.", "Selection Required");
                return;
            }

            try
            {
                _controller.UpdateCustomer(id, name, contact, isStaff);
                AddHistoryLog($"[CRUD UPDATE] Updated details for Customer {id} ({name}). Staff: {isStaff}.");

                lblCustMessage.ForeColor = Color.FromArgb(3, 105, 161);
                lblCustMessage.Text = $"Customer '{name}' was updated successfully.";

                RefreshCustomerDropdown(id);
            }
            catch (Exception ex)
            {
                ShowError(ex.Message, "Update Error");
                lblCustMessage.ForeColor = Color.FromArgb(185, 28, 28);
                lblCustMessage.Text = "Update Failed: " + ex.Message;
            }
        }

        private void btnCustDelete_Click(object sender, EventArgs e)
        {
            if (_selectedCustomer == null)
            {
                ShowWarning("Please select a customer to delete.", "Selection Required");
                return;
            }

            string customerId = _selectedCustomer.CustomerNumber;
            string customerName = _selectedCustomer.Name;

            DialogResult confirm = MessageBox.Show(
                $"Are you sure you want to delete customer '{customerName}' ({customerId}) and all associated accounts?",
                "Confirm Customer Deletion",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (confirm == DialogResult.Yes)
            {
                bool deleted = _controller.DeleteCustomer(customerId);
                if (deleted)
                {
                    AddHistoryLog($"[CRUD DELETE] Customer {customerName} ({customerId}) was removed.");
                    lblCustMessage.ForeColor = Color.FromArgb(185, 28, 28);
                    lblCustMessage.Text = $"Customer '{customerName}' was deleted.";
                    RefreshCustomerDropdown();
                }
            }
        }

        private void btnCustClear_Click(object sender, EventArgs e)
        {
            ClearCustomerFields();
            txtCustId.Focus();
            lblCustMessage.ForeColor = Color.FromArgb(71, 85, 105);
            lblCustMessage.Text = "Enter new customer ID, name, and contact details, then click 'Add Customer'.";
        }

        private void ClearCustomerFields()
        {
            txtCustId.ReadOnly = false;
            txtCustId.Clear();
            txtCustName.Clear();
            txtCustContact.Clear();
            chkCustStaff.Checked = false;
        }

        // =========================================================================
        // ACCOUNT OVERVIEW, SELECTION & DYNAMIC ACCOUNT CREATION (TASK 3)
        // =========================================================================

        private void PopulateAccountDropdown(string? selectAccountNumber = null)
        {
            cboAccounts.Items.Clear();
            if (_selectedCustomer == null || _selectedCustomer.Accounts == null) return;

            int selectedIndex = 0;
            for (int i = 0; i < _selectedCustomer.Accounts.Count; i++)
            {
                Account acc = _selectedCustomer.Accounts[i];
                cboAccounts.Items.Add($"{acc.AccountNumber} ({acc.AccountType})");

                if (selectAccountNumber != null && string.Equals(acc.AccountNumber, selectAccountNumber, StringComparison.OrdinalIgnoreCase))
                {
                    selectedIndex = i;
                }
            }

            if (cboAccounts.Items.Count > 0)
            {
                cboAccounts.SelectedIndex = selectedIndex;
            }
            else
            {
                ResetAccountDisplay();
            }
        }

        private Account? GetSelectedAccount()
        {
            if (_selectedCustomer == null) return null;

            if (cboAccounts.SelectedIndex >= 0 && cboAccounts.SelectedIndex < _selectedCustomer.Accounts.Count)
            {
                return _selectedCustomer.Accounts[cboAccounts.SelectedIndex];
            }
            return null;
        }

        private void cboAccounts_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateAccountDisplay();
        }

        private void UpdateAccountDisplay()
        {
            Account? acc = GetSelectedAccount();
            if (acc == null)
            {
                ResetAccountDisplay();
                return;
            }

            lblAccountNumber.Text = "Account No: " + acc.AccountNumber;
            lblBalanceValue.Text = (acc.Balance < 0 ? "-$" + Math.Abs(acc.Balance).ToString("F2") : "$" + acc.Balance.ToString("F2"));
            lblBalanceValue.ForeColor = acc.Balance < 0 ? Color.FromArgb(220, 38, 38) : Color.FromArgb(14, 165, 233);

            lblStatusMessage.ForeColor = Color.FromArgb(15, 23, 42);
            lblStatusMessage.Text = acc.LastTransactionStatus;

            if (acc is EverydayAccount)
            {
                lblAccountRules.Text = "• Type: Everyday Account\n• Interest: 0% | Overdraft: $0.00 | Fee: $0.00\n• Pure checking account for day-to-day transactions.";
            }
            else if (acc is InvestmentAccount inv)
            {
                lblAccountRules.Text = $"• Type: Investment Account\n• Interest: {inv.InterestRate:F1}% on positive balance | Overdraft: $0.00\n• Failed Fee: ${inv.FailedFee:F2} (50% Staff Discount applies: ${inv.FailedFee * 0.5m:F2}).";
            }
            else if (acc is OmniAccount omni)
            {
                lblAccountRules.Text = $"• Type: Omni Account\n• Interest: {omni.InterestRate:F1}% on portion >$1,000 | Overdraft: ${omni.OverdraftLimit:F2}\n• Failed Fee: ${omni.FailedFee:F2} (50% Staff Discount applies: ${omni.FailedFee * 0.5m:F2}).";
            }
        }

        private void ResetAccountDisplay()
        {
            lblAccountNumber.Text = "Account No: None";
            lblBalanceValue.Text = "$0.00";
            lblBalanceValue.ForeColor = Color.FromArgb(14, 165, 233);
            lblAccountRules.Text = "No account selected.";
            lblStatusMessage.Text = "No active account.";
        }

        // Task 3: Launch Dynamic Account Creation Form (Form Inheritance)
        private void btnAddAccount_Click(object sender, EventArgs e)
        {
            if (_selectedCustomer == null)
            {
                ShowWarning("Please select an active customer first before creating an account.", "No Customer Selected");
                return;
            }

            using (var form = new AddAccountForm(_controller, _selectedCustomer, () =>
            {
                PopulateAccountDropdown();
                UpdateAccountDisplay();
                AddHistoryLog($"[ACCOUNT CREATION] Successfully added account to {_selectedCustomer.Name}. Total: {_selectedCustomer.Accounts.Count}.");
            }))
            {
                form.ShowDialog(this);
            }
        }

        // =========================================================================
        // FINANCIAL TRANSACTIONS & INTRA-ACCOUNT MONEY TRANSFERS
        // =========================================================================

        private void btnDeposit_Click(object sender, EventArgs e)
        {
            Account? acc = GetSelectedAccount();
            if (acc == null)
            {
                ShowWarning("Please select an account first.", "No Account Selected");
                return;
            }

            if (!decimal.TryParse(txtAmount.Text, out decimal amount))
            {
                ShowError("Please enter a valid numeric transaction amount.", "Invalid Input");
                return;
            }

            try
            {
                _controller.Deposit(acc, amount);

                UpdateAccountDisplay();
                lblStatusMessage.ForeColor = Color.FromArgb(21, 128, 61);
                AddHistoryLog($"[DEPOSIT] {acc.AccountNumber}: +${amount:F2} | Balance: ${acc.Balance:F2}");
            }
            catch (BankingException ex)
            {
                lblStatusMessage.ForeColor = Color.FromArgb(185, 28, 28);
                lblStatusMessage.Text = ex.Message;
                AddHistoryLog($"[DEPOSIT ERROR] {acc.AccountNumber}: {ex.Message}");
                ShowWarning(ex.Message, "Deposit Rejected");
            }
            catch (Exception ex)
            {
                ShowError("Unexpected error during deposit: " + ex.Message, "Error");
            }
        }

        private void btnWithdraw_Click(object sender, EventArgs e)
        {
            Account? acc = GetSelectedAccount();
            if (acc == null)
            {
                ShowWarning("Please select an account first.", "No Account Selected");
                return;
            }

            if (!decimal.TryParse(txtAmount.Text, out decimal amount))
            {
                ShowError("Please enter a valid numeric transaction amount.", "Invalid Input");
                return;
            }

            try
            {
                _controller.Withdraw(acc, amount);

                UpdateAccountDisplay();
                lblStatusMessage.ForeColor = Color.FromArgb(21, 128, 61);
                AddHistoryLog($"[WITHDRAWAL] {acc.AccountNumber}: -${amount:F2} | Balance: ${acc.Balance:F2}");
            }
            catch (InsufficientFundsException ex)
            {
                UpdateAccountDisplay();
                lblStatusMessage.ForeColor = Color.FromArgb(185, 28, 28);
                lblStatusMessage.Text = ex.Message;
                AddHistoryLog($"[INSUFFICIENT FUNDS] {acc.AccountNumber}: {ex.Message}");
                ShowError(ex.Message, "Transaction Declined — Insufficient Funds");
            }
            catch (BankingException ex)
            {
                lblStatusMessage.ForeColor = Color.FromArgb(185, 28, 28);
                lblStatusMessage.Text = ex.Message;
                AddHistoryLog($"[BANKING EXCEPTION] {acc.AccountNumber}: {ex.Message}");
                ShowWarning(ex.Message, "Transaction Error");
            }
            catch (Exception ex)
            {
                ShowError("Unexpected error during withdrawal: " + ex.Message, "Error");
            }
        }

        private void btnInterest_Click(object sender, EventArgs e)
        {
            Account? acc = GetSelectedAccount();
            if (acc == null)
            {
                ShowWarning("Please select an account first.", "No Account Selected");
                return;
            }

            try
            {
                decimal interest = _controller.CalculateInterest(acc);
                UpdateAccountDisplay();

                if (interest > 0)
                {
                    lblStatusMessage.ForeColor = Color.FromArgb(21, 128, 61);
                    AddHistoryLog($"[INTEREST APPLIED] {acc.AccountNumber}: +${interest:F2} | New Balance: ${acc.Balance:F2}");
                }
                else
                {
                    lblStatusMessage.ForeColor = Color.FromArgb(71, 85, 105);
                    AddHistoryLog($"[INTEREST CHECK] {acc.AccountNumber}: $0.00 applied | Status: {acc.LastTransactionStatus}");
                }
            }
            catch (Exception ex)
            {
                ShowError("Error calculating interest: " + ex.Message, "Error");
            }
        }

        // Task 4: Launch Dedicated Intra-Account Transfer Dialog (Form Inheritance)
        private void btnTransfer_Click(object sender, EventArgs e)
        {
            if (_selectedCustomer == null)
            {
                ShowWarning("Please select an active customer first.", "No Customer Selected");
                return;
            }

            if (_selectedCustomer.Accounts.Count < 2)
            {
                ShowWarning("This customer needs at least 2 accounts to perform an intra-account transfer.", "Insufficient Accounts");
                return;
            }

            using (var transferForm = new TransferForm(_controller, _selectedCustomer, () =>
            {
                PopulateAccountDropdown();
                UpdateAccountDisplay();
                AddHistoryLog($"[INTRA-ACCOUNT TRANSFER] Completed money transfer for {_selectedCustomer.Name}.");
            }))
            {
                transferForm.ShowDialog(this);
            }
        }

        // =========================================================================
        // AUDIT LOGGING & HELPERS
        // =========================================================================

        private void AddHistoryLog(string message)
        {
            string timeStamp = DateTime.Now.ToString("HH:mm:ss");
            lstHistory.Items.Add($"[{timeStamp}] {message}");
            lstHistory.TopIndex = lstHistory.Items.Count - 1;
        }

        private void btnClearHistory_Click(object sender, EventArgs e)
        {
            lstHistory.Items.Clear();
            AddHistoryLog("Transaction audit history cleared.");
        }
    }
}
