using System;
using System.Drawing;
using System.Windows.Forms;
using _20240040_MuskanPanwar_BankSprint1;

namespace GUI
{
    public partial class Form1 : Form
    {
        // MVC Architecture: Form (View) only holds a reference to the Controller.
        // Direct storage or manipulation of List<Customer> is strictly prohibited inside the Form.
        private readonly CustomerController _controller;
        private Customer _selectedCustomer;

        public Form1()
        {
            InitializeComponent();
            _controller = new CustomerController();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            AddHistoryLog("=== Nova Premier Bank System Initialized (Sprint 2 MVC) ===");
            RefreshCustomerDropdown();
        }

        // =========================================================================
        // CUSTOMER DATA BINDING & SELECTION (READ)
        // =========================================================================

        /// <summary>
        /// Refreshes the customer dropdown from the controller.
        /// </summary>
        private void RefreshCustomerDropdown(string selectCustomerNumber = null)
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
            txtCustId.ReadOnly = true; // Primary key cannot be altered during edit
            txtCustName.Text = customer.Name;
            txtCustContact.Text = customer.ContactDetails;
            chkCustStaff.Checked = customer.IsStaff;

            lblCustMessage.ForeColor = Color.FromArgb(71, 85, 105);
            lblCustMessage.Text = $"Viewing record for {customer.Name} ({customer.CustomerNumber}).";
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
        // CUSTOMER CRUD OPERATIONS (Routed through Controller)
        // =========================================================================

        // CREATE: Add new customer
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
                MessageBox.Show(ex.Message, "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                lblCustMessage.ForeColor = Color.FromArgb(185, 28, 28);
                lblCustMessage.Text = "Validation Failed: " + ex.Message;
            }
            catch (InvalidOperationException ex)
            {
                MessageBox.Show(ex.Message, "Duplicate Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                lblCustMessage.ForeColor = Color.FromArgb(185, 28, 28);
                lblCustMessage.Text = ex.Message;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Unexpected error creating customer: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // UPDATE: Modify selected customer
        private void btnCustUpdate_Click(object sender, EventArgs e)
        {
            string id = txtCustId.Text.Trim();
            string name = txtCustName.Text.Trim();
            string contact = txtCustContact.Text.Trim();
            bool isStaff = chkCustStaff.Checked;

            if (string.IsNullOrWhiteSpace(id))
            {
                MessageBox.Show("Please select a customer to update.", "Selection Required", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                MessageBox.Show(ex.Message, "Update Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                lblCustMessage.ForeColor = Color.FromArgb(185, 28, 28);
                lblCustMessage.Text = "Update Failed: " + ex.Message;
            }
        }

        // DELETE: Remove customer
        private void btnCustDelete_Click(object sender, EventArgs e)
        {
            if (_selectedCustomer == null)
            {
                MessageBox.Show("Please select a customer to delete.", "Selection Required", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        // RESET / NEW FORM
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
        // ACCOUNT OVERVIEW & SELECTION
        // =========================================================================

        private void PopulateAccountDropdown()
        {
            cboAccounts.Items.Clear();
            if (_selectedCustomer == null || _selectedCustomer.Accounts == null) return;

            foreach (Account acc in _selectedCustomer.Accounts)
            {
                string typeName = acc.GetType().Name.Replace("Account", "");
                cboAccounts.Items.Add($"{acc.AccountNumber} ({typeName})");
            }

            if (cboAccounts.Items.Count > 0)
            {
                cboAccounts.SelectedIndex = 0;
            }
            else
            {
                ResetAccountDisplay();
            }
        }

        private Account GetSelectedAccount()
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
            Account acc = GetSelectedAccount();
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
                lblAccountRules.Text = "• Type: Everyday Account\n• Interest: 0% | Overdraft: $0.00 | Fee: $0.00\n• Pure checking account for daily expenses.";
            }
            else if (acc is InvestmentAccount inv)
            {
                lblAccountRules.Text = $"• Type: Investment Account\n• Interest: {inv.InterestRate:F1}% on positive balance | Overdraft: $0.00\n• Failed Fee: ${inv.FailedFee:F2} (50% Staff Discount applies).";
            }
            else if (acc is OmniAccount omni)
            {
                lblAccountRules.Text = $"• Type: Omni Account\n• Interest: {omni.InterestRate:F1}% on portion over $1,000 | Overdraft: ${omni.OverdraftLimit:F2}\n• Failed Fee: ${omni.FailedFee:F2} (50% Staff Discount applies).";
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

        // =========================================================================
        // FINANCIAL TRANSACTIONS (Exception-Protected Business Logic)
        // =========================================================================

        private void btnDeposit_Click(object sender, EventArgs e)
        {
            Account acc = GetSelectedAccount();
            if (acc == null)
            {
                MessageBox.Show("Please select an account first.", "No Account Selected", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!decimal.TryParse(txtAmount.Text, out decimal amount))
            {
                MessageBox.Show("Please enter a valid numeric transaction amount.", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                // Deposit routed through controller
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
                MessageBox.Show(ex.Message, "Deposit Rejected", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Unexpected error during deposit: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnWithdraw_Click(object sender, EventArgs e)
        {
            Account acc = GetSelectedAccount();
            if (acc == null)
            {
                MessageBox.Show("Please select an account first.", "No Account Selected", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!decimal.TryParse(txtAmount.Text, out decimal amount))
            {
                MessageBox.Show("Please enter a valid numeric transaction amount.", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                // Withdraw routed through controller; triggers custom exceptions on violation
                _controller.Withdraw(acc, amount);

                UpdateAccountDisplay();
                lblStatusMessage.ForeColor = Color.FromArgb(21, 128, 61);
                AddHistoryLog($"[WITHDRAWAL] {acc.AccountNumber}: -${amount:F2} | Balance: ${acc.Balance:F2}");
            }
            catch (InsufficientFundsException ex)
            {
                UpdateAccountDisplay(); // Update display as fee may have been charged
                lblStatusMessage.ForeColor = Color.FromArgb(185, 28, 28);
                lblStatusMessage.Text = ex.Message;
                AddHistoryLog($"[INSUFFICIENT FUNDS EXCEPTION] {acc.AccountNumber}: {ex.Message}");

                MessageBox.Show(
                    ex.Message,
                    "Transaction Declined - Insufficient Funds",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
            }
            catch (BankingException ex)
            {
                lblStatusMessage.ForeColor = Color.FromArgb(185, 28, 28);
                lblStatusMessage.Text = ex.Message;
                AddHistoryLog($"[BANKING EXCEPTION] {acc.AccountNumber}: {ex.Message}");

                MessageBox.Show(ex.Message, "Transaction Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Unexpected error during withdrawal: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnInterest_Click(object sender, EventArgs e)
        {
            Account acc = GetSelectedAccount();
            if (acc == null)
            {
                MessageBox.Show("Please select an account first.", "No Account Selected", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
                MessageBox.Show("Error calculating interest: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

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
