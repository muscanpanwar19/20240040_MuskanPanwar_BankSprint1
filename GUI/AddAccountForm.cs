using System;
using System.Drawing;
using System.Windows.Forms;
using _20240040_MuskanPanwar_BankSprint1;

namespace GUI
{
    /// <summary>
    /// Dynamic Account Creation window demonstrating Form Inheritance (inherits BaseBankForm).
    /// Allows bank operators to add new Everyday, Investment, or Omni accounts to a customer at runtime (Task 3).
    /// </summary>
    public class AddAccountForm : BaseBankForm
    {
        private readonly CustomerController _controller;
        private readonly Customer _customer;
        private readonly Action _onAccountCreated;

        // UI Controls
        private ComboBox cboAccountType = null!;
        private TextBox txtAccountNumber = null!;
        private TextBox txtInitialDeposit = null!;
        private TextBox txtInterestRate = null!;
        private TextBox txtOverdraftLimit = null!;
        private Label lblInterestRate = null!;
        private Label lblOverdraftLimit = null!;
        private Label lblTypeInfo = null!;
        private Button btnCreate = null!;
        private Button btnCancel = null!;

        public AddAccountForm(CustomerController controller, Customer customer, Action onAccountCreated)
        {
            _controller = controller ?? throw new ArgumentNullException(nameof(controller));
            _customer = customer ?? throw new ArgumentNullException(nameof(customer));
            _onAccountCreated = onAccountCreated;

            InitializeCustomComponents();
        }

        private void InitializeCustomComponents()
        {
            this.Text = "Nova Premier Bank — Add New Account";
            this.ClientSize = new Size(540, 510);
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.StartPosition = FormStartPosition.CenterParent;

            // Branded Top Header
            Panel header = CreateBrandedHeader("Add New Account", $"Assign an additional account to {_customer.Name}", 540, 65);
            this.Controls.Add(header);

            // Group Container
            GroupBox grpDetails = new GroupBox
            {
                Text = " Account Specification ",
                Location = new Point(16, 78),
                Size = new Size(508, 360),
                Font = new Font("Segoe UI", 9F, FontStyle.Bold),
                ForeColor = BrandNavy,
                BackColor = Color.White
            };

            // Account Type
            Label lblType = new Label
            {
                Text = "Account Type:",
                Location = new Point(20, 28),
                Size = new Size(130, 18),
                Font = new Font("Segoe UI", 8.5F, FontStyle.Bold),
                ForeColor = TextSecondary
            };
            cboAccountType = new ComboBox
            {
                Location = new Point(20, 48),
                Size = new Size(220, 24),
                DropDownStyle = ComboBoxStyle.DropDownList,
                Font = new Font("Segoe UI", 9F)
            };
            cboAccountType.Items.AddRange(new object[] { "Everyday Account", "Investment Account", "Omni Account" });
            cboAccountType.SelectedIndex = 0;
            cboAccountType.SelectedIndexChanged += CboAccountType_SelectedIndexChanged;

            // Account Number
            Label lblNum = new Label
            {
                Text = "Account Number:",
                Location = new Point(260, 28),
                Size = new Size(140, 18),
                Font = new Font("Segoe UI", 8.5F, FontStyle.Bold),
                ForeColor = TextSecondary
            };
            txtAccountNumber = new TextBox
            {
                Location = new Point(260, 48),
                Size = new Size(225, 24),
                Font = new Font("Segoe UI", 9F)
            };

            // Opening Balance
            Label lblDep = new Label
            {
                Text = "Initial Deposit ($):",
                Location = new Point(20, 88),
                Size = new Size(150, 18),
                Font = new Font("Segoe UI", 8.5F, FontStyle.Bold),
                ForeColor = TextSecondary
            };
            txtInitialDeposit = new TextBox
            {
                Location = new Point(20, 108),
                Size = new Size(220, 24),
                Font = new Font("Segoe UI", 9F),
                Text = "250.00"
            };

            // Interest Rate
            lblInterestRate = new Label
            {
                Text = "Interest Rate (%):",
                Location = new Point(260, 88),
                Size = new Size(150, 18),
                Font = new Font("Segoe UI", 8.5F, FontStyle.Bold),
                ForeColor = TextSecondary
            };
            txtInterestRate = new TextBox
            {
                Location = new Point(260, 108),
                Size = new Size(225, 24),
                Font = new Font("Segoe UI", 9F),
                Text = "4.50",
                Enabled = false
            };

            // Overdraft Limit
            lblOverdraftLimit = new Label
            {
                Text = "Overdraft Limit ($):",
                Location = new Point(20, 148),
                Size = new Size(150, 18),
                Font = new Font("Segoe UI", 8.5F, FontStyle.Bold),
                ForeColor = TextSecondary
            };
            txtOverdraftLimit = new TextBox
            {
                Location = new Point(20, 168),
                Size = new Size(220, 24),
                Font = new Font("Segoe UI", 9F),
                Text = "500.00",
                Enabled = false
            };

            // Information Box
            lblTypeInfo = new Label
            {
                Text = "Everyday Account: Standard checking with zero interest, zero overdraft, and zero failed transaction fees.",
                Location = new Point(20, 210),
                Size = new Size(465, 60),
                Font = new Font("Segoe UI", 8.5F, FontStyle.Regular),
                ForeColor = TextSecondary,
                BackColor = Color.FromArgb(248, 250, 252),
                BorderStyle = BorderStyle.FixedSingle,
                Padding = new Padding(6)
            };

            grpDetails.Controls.Add(lblType);
            grpDetails.Controls.Add(cboAccountType);
            grpDetails.Controls.Add(lblNum);
            grpDetails.Controls.Add(txtAccountNumber);
            grpDetails.Controls.Add(lblDep);
            grpDetails.Controls.Add(txtInitialDeposit);
            grpDetails.Controls.Add(lblInterestRate);
            grpDetails.Controls.Add(txtInterestRate);
            grpDetails.Controls.Add(lblOverdraftLimit);
            grpDetails.Controls.Add(txtOverdraftLimit);
            grpDetails.Controls.Add(lblTypeInfo);
            this.Controls.Add(grpDetails);

            // Action Buttons
            btnCreate = new Button
            {
                Text = "+ Open Account",
                Location = new Point(276, 452),
                Size = new Size(135, 36)
            };
            StyleSuccessButton(btnCreate);
            btnCreate.Click += BtnCreate_Click;

            btnCancel = new Button
            {
                Text = "Cancel",
                Location = new Point(418, 452),
                Size = new Size(106, 36)
            };
            btnCancel.BackColor = Color.FromArgb(241, 245, 249);
            btnCancel.ForeColor = TextSecondary;
            btnCancel.FlatStyle = FlatStyle.Flat;
            btnCancel.FlatAppearance.BorderColor = Color.FromArgb(203, 213, 225);
            btnCancel.Font = new Font("Segoe UI", 8.5F, FontStyle.Bold);
            btnCancel.Click += (s, e) => this.Close();

            this.Controls.Add(btnCreate);
            this.Controls.Add(btnCancel);

            SuggestAccountNumber();
        }

        private void CboAccountType_SelectedIndexChanged(object? sender, EventArgs e)
        {
            SuggestAccountNumber();
            switch (cboAccountType.SelectedIndex)
            {
                case 0: // Everyday
                    txtInterestRate.Enabled = false;
                    txtOverdraftLimit.Enabled = false;
                    lblTypeInfo.Text = "Everyday Account: Standard checking with zero interest, zero overdraft, and zero failed transaction fees.";
                    break;

                case 1: // Investment
                    txtInterestRate.Enabled = true;
                    txtOverdraftLimit.Enabled = false;
                    lblTypeInfo.Text = "Investment Account: Accrues compound interest. Failed transactions incur a $10 fee ($5 for bank staff). No overdraft.";
                    break;

                case 2: // Omni
                    txtInterestRate.Enabled = true;
                    txtOverdraftLimit.Enabled = true;
                    lblTypeInfo.Text = "Omni Account: Permits overdraft up to approved limit. Failed breaches incur a $10 fee ($5 staff). Interest applies on balances over $1,000.";
                    break;
            }
        }

        private void SuggestAccountNumber()
        {
            string prefix = cboAccountType.SelectedIndex switch
            {
                1 => "INV",
                2 => "OMN",
                _ => "ED"
            };
            int count = _customer.Accounts.Count + 1;
            txtAccountNumber.Text = $"{prefix}-{_customer.CustomerNumber}-0{count}";
        }

        private void BtnCreate_Click(object? sender, EventArgs e)
        {
            string accNum = txtAccountNumber.Text.Trim();
            if (string.IsNullOrWhiteSpace(accNum))
            {
                ShowWarning("Please provide a valid account number.", "Missing Account Number");
                txtAccountNumber.Focus();
                return;
            }

            if (!decimal.TryParse(txtInitialDeposit.Text.Trim(), out decimal initialDeposit) || initialDeposit < 0)
            {
                ShowWarning("Initial deposit must be a valid amount of $0.00 or greater.", "Invalid Deposit");
                txtInitialDeposit.Focus();
                return;
            }

            decimal interestRate = 0m;
            decimal overdraftLimit = 0m;

            if (txtInterestRate.Enabled && (!decimal.TryParse(txtInterestRate.Text.Trim(), out interestRate) || interestRate < 0))
            {
                ShowWarning("Interest rate must be a valid percentage (0.00 or higher).", "Invalid Rate");
                txtInterestRate.Focus();
                return;
            }

            if (txtOverdraftLimit.Enabled && (!decimal.TryParse(txtOverdraftLimit.Text.Trim(), out overdraftLimit) || overdraftLimit < 0))
            {
                ShowWarning("Overdraft limit must be a valid amount (0.00 or higher).", "Invalid Overdraft");
                txtOverdraftLimit.Focus();
                return;
            }

            string rawType = cboAccountType.SelectedIndex switch
            {
                1 => "Investment",
                2 => "Omni",
                _ => "Everyday"
            };

            try
            {
                Account newAcc = _controller.AddAccount(_customer.CustomerNumber, rawType, accNum, initialDeposit, interestRate, overdraftLimit);
                ShowSuccess($"Account {newAcc.AccountNumber} ({newAcc.AccountType}) successfully created with opening balance ${newAcc.Balance:F2}!", "Account Created");
                _onAccountCreated?.Invoke();
                this.Close();
            }
            catch (ArgumentException ex)
            {
                ShowWarning(ex.Message, "Validation Error");
            }
            catch (Exception ex)
            {
                ShowError("Failed to create account: " + ex.Message, "Creation Error");
            }
        }
    }
}
