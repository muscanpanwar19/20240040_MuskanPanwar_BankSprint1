using System;
using System.Drawing;
using System.Windows.Forms;
using _20240040_MuskanPanwar_BankSprint1;

namespace GUI
{
    public partial class Form1 : Form
    {
        // Customer instance
        private Customer currentCustomer;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // Create customer (Muskan Panwar) and pre-populate Everyday, Investment, and Omni accounts
            currentCustomer = new Customer("20240040", "Muskan Panwar", "muskan.panwar@email.com", false);
            currentCustomer.PrePopulateAccounts();

            PopulateAccountDropdown();
            AddHistoryLog("System initialized for " + currentCustomer.Name);
        }

        // Populate dropdown with all customer accounts
        private void PopulateAccountDropdown()
        {
            cboAccounts.Items.Clear();
            foreach (Account acc in currentCustomer.Accounts)
            {
                cboAccounts.Items.Add(acc.AccountNumber + " (" + acc.GetType().Name.Replace("Account", "") + ")");
            }

            if (cboAccounts.Items.Count > 0)
            {
                cboAccounts.SelectedIndex = 0;
            }
        }

        // Get currently selected account
        private Account GetSelectedAccount()
        {
            if (cboAccounts.SelectedIndex >= 0 && cboAccounts.SelectedIndex < currentCustomer.Accounts.Count)
            {
                return currentCustomer.Accounts[cboAccounts.SelectedIndex];
            }
            return null;
        }

        // Update UI details for selected account
        private void UpdateAccountDisplay()
        {
            Account acc = GetSelectedAccount();
            if (acc == null) return;

            lblAccountNumber.Text = "Account No: " + acc.AccountNumber;
            lblBalanceValue.Text = "$" + acc.Balance.ToString("F2");
            lblStatusMessage.Text = acc.LastTransactionStatus;

            // Display account specific features
            if (acc is EverydayAccount)
            {
                lblAccountRules.Text = "• Features: No Interest | No Overdraft | $0 Transaction Fee";
            }
            else if (acc is InvestmentAccount)
            {
                lblAccountRules.Text = "• Features: 4.0% Interest | No Overdraft | $10 Failed Fee (50% Staff Off)";
            }
            else if (acc is OmniAccount)
            {
                lblAccountRules.Text = "• Features: 5.0% Interest (> $1k) | $500 Overdraft | $10 Failed Fee (50% Off)";
            }
        }

        private void cboAccounts_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateAccountDisplay();
        }

        // Radio button to easily select Regular Customer or Bank Staff
        private void rdoRole_CheckedChanged(object sender, EventArgs e)
        {
            currentCustomer.IsStaff = rdoStaff.Checked;

            if (currentCustomer.IsStaff)
            {
                AddHistoryLog("Role switched: Bank Staff (50% Fee Discount Active)");
            }
            else
            {
                AddHistoryLog("Role switched: Regular Customer");
            }
        }

        // Deposit button click
        private void btnDeposit_Click(object sender, EventArgs e)
        {
            Account acc = GetSelectedAccount();
            if (acc == null) return;

            if (decimal.TryParse(txtAmount.Text, out decimal amount))
            {
                acc.Deposit(amount);
                UpdateAccountDisplay();
                AddHistoryLog(acc.LastTransactionStatus);
            }
            else
            {
                MessageBox.Show("Please enter a valid amount.", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Withdraw button click
        private void btnWithdraw_Click(object sender, EventArgs e)
        {
            Account acc = GetSelectedAccount();
            if (acc == null) return;

            if (decimal.TryParse(txtAmount.Text, out decimal amount))
            {
                acc.Withdraw(amount);
                UpdateAccountDisplay();
                AddHistoryLog(acc.LastTransactionStatus);
            }
            else
            {
                MessageBox.Show("Please enter a valid amount.", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Calculate interest button click
        private void btnInterest_Click(object sender, EventArgs e)
        {
            Account acc = GetSelectedAccount();
            if (acc == null) return;

            acc.CalculateInterest();
            UpdateAccountDisplay();
            AddHistoryLog(acc.LastTransactionStatus);
        }

        // Add message to transaction history listbox
        private void AddHistoryLog(string message)
        {
            lstHistory.Items.Add(message);
            lstHistory.TopIndex = lstHistory.Items.Count - 1; // Auto-scroll
        }

        // Clear history listbox
        private void btnClearHistory_Click(object sender, EventArgs e)
        {
            lstHistory.Items.Clear();
            AddHistoryLog("Transaction history cleared.");
        }
    }
}
