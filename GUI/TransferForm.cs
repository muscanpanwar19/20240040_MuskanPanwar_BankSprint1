using System;
using System.Drawing;
using System.Windows.Forms;
using _20240040_MuskanPanwar_BankSprint1;

namespace GUI
{
    /// <summary>
    /// Dedicated Intra-Account Transfer window demonstrating Form Inheritance (inherits BaseBankForm).
    /// Provides ComboBox selections for Source and Destination accounts owned by the active customer.
    /// Strictly enforces MVC principles: financial execution delegates exclusively to CustomerController.
    /// </summary>
    public class TransferForm : BaseBankForm
    {
        private readonly CustomerController _controller;
        private readonly Customer _customer;
        private readonly Action _onTransferCompleted;

        // UI Controls
        private ComboBox cboSource = null!;
        private ComboBox cboDestination = null!;
        private TextBox txtAmount = null!;
        private Label lblSourceBalance = null!;
        private Label lblDestBalance = null!;
        private Label lblStatus = null!;
        private Button btnTransfer = null!;
        private Button btnClose = null!;

        public TransferForm(CustomerController controller, Customer customer, Action onTransferCompleted)
        {
            _controller = controller ?? throw new ArgumentNullException(nameof(controller));
            _customer = customer ?? throw new ArgumentNullException(nameof(customer));
            _onTransferCompleted = onTransferCompleted;

            InitializeCustomComponents();
            PopulateAccountDropdowns();
        }

        private void InitializeCustomComponents()
        {
            this.Text = "Nova Premier Bank — Intra-Account Money Transfer";
            this.ClientSize = new Size(620, 520);
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.StartPosition = FormStartPosition.CenterParent;

            // Branded Top Header using BaseBankForm factory
            Panel header = CreateBrandedHeader("Intra-Account Transfer", "Move funds between accounts owned by this customer", 620, 65);
            this.Controls.Add(header);

            // Customer Info Banner
            Panel custPanel = new Panel
            {
                Location = new Point(16, 75),
                Size = new Size(588, 48),
                BackColor = Color.White,
                BorderStyle = BorderStyle.FixedSingle
            };

            Label lblCustInfo = new Label
            {
                Text = $"Active Customer: {_customer.Name} (ID: {_customer.CustomerNumber})",
                Font = new Font("Segoe UI", 9.5F, FontStyle.Bold),
                ForeColor = BrandNavy,
                Location = new Point(12, 14),
                AutoSize = true
            };

            Label lblStaffBadge = new Label
            {
                Text = _customer.IsStaff ? "★ BANK STAFF (50% Fee Discount)" : "Regular Customer",
                Font = new Font("Segoe UI", 8.5F, FontStyle.Bold),
                ForeColor = _customer.IsStaff ? Color.FromArgb(217, 119, 6) : Color.FromArgb(71, 85, 105),
                Location = new Point(340, 15),
                Size = new Size(230, 20),
                TextAlign = ContentAlignment.MiddleRight
            };

            custPanel.Controls.Add(lblCustInfo);
            custPanel.Controls.Add(lblStaffBadge);
            this.Controls.Add(custPanel);

            // Transfer GroupBox
            GroupBox grpTransfer = new GroupBox
            {
                Text = " Transfer Details (Intra-Account Only) ",
                Location = new Point(16, 132),
                Size = new Size(588, 255),
                Font = new Font("Segoe UI", 9F, FontStyle.Bold),
                ForeColor = BrandNavy,
                BackColor = Color.White
            };

            // Source Account
            Label lblSource = new Label
            {
                Text = "Source Account (Debit):",
                Location = new Point(20, 32),
                Size = new Size(180, 18),
                Font = new Font("Segoe UI", 8.5F, FontStyle.Bold),
                ForeColor = TextSecondary
            };
            cboSource = new ComboBox
            {
                Location = new Point(20, 52),
                Size = new Size(350, 24),
                DropDownStyle = ComboBoxStyle.DropDownList,
                Font = new Font("Segoe UI", 9F)
            };
            cboSource.SelectedIndexChanged += (s, e) => UpdateAccountBalances();

            lblSourceBalance = new Label
            {
                Text = "Balance: $0.00",
                Location = new Point(380, 54),
                Size = new Size(185, 20),
                Font = new Font("Segoe UI", 9F, FontStyle.Bold),
                ForeColor = AccentBlue
            };

            // Destination Account
            Label lblDest = new Label
            {
                Text = "Destination Account (Credit):",
                Location = new Point(20, 92),
                Size = new Size(200, 18),
                Font = new Font("Segoe UI", 8.5F, FontStyle.Bold),
                ForeColor = TextSecondary
            };
            cboDestination = new ComboBox
            {
                Location = new Point(20, 112),
                Size = new Size(350, 24),
                DropDownStyle = ComboBoxStyle.DropDownList,
                Font = new Font("Segoe UI", 9F)
            };
            cboDestination.SelectedIndexChanged += (s, e) => UpdateAccountBalances();

            lblDestBalance = new Label
            {
                Text = "Balance: $0.00",
                Location = new Point(380, 114),
                Size = new Size(185, 20),
                Font = new Font("Segoe UI", 9F, FontStyle.Bold),
                ForeColor = SuccessGreen
            };

            // Transfer Amount
            Label lblAmt = new Label
            {
                Text = "Transfer Amount ($):",
                Location = new Point(20, 152),
                Size = new Size(150, 18),
                Font = new Font("Segoe UI", 8.5F, FontStyle.Bold),
                ForeColor = TextSecondary
            };
            txtAmount = new TextBox
            {
                Location = new Point(20, 172),
                Size = new Size(180, 24),
                Font = new Font("Segoe UI", 9.5F, FontStyle.Regular),
                Text = "100.00"
            };

            // Informational status note
            lblStatus = new Label
            {
                Text = "Transfers between different customers are strictly prohibited. Overdraft limits and failed fees apply to source accounts.",
                Location = new Point(20, 206),
                Size = new Size(545, 38),
                Font = new Font("Segoe UI", 8F, FontStyle.Regular),
                ForeColor = TextSecondary,
                BackColor = Color.FromArgb(248, 250, 252),
                BorderStyle = BorderStyle.FixedSingle,
                Padding = new Padding(4)
            };

            grpTransfer.Controls.Add(lblSource);
            grpTransfer.Controls.Add(cboSource);
            grpTransfer.Controls.Add(lblSourceBalance);
            grpTransfer.Controls.Add(lblDest);
            grpTransfer.Controls.Add(cboDestination);
            grpTransfer.Controls.Add(lblDestBalance);
            grpTransfer.Controls.Add(lblAmt);
            grpTransfer.Controls.Add(txtAmount);
            grpTransfer.Controls.Add(lblStatus);
            this.Controls.Add(grpTransfer);

            // Action Buttons
            btnTransfer = new Button
            {
                Text = "⇄ Execute Transfer",
                Location = new Point(320, 420),
                Size = new Size(160, 36)
            };
            StyleSuccessButton(btnTransfer);
            btnTransfer.Click += BtnTransfer_Click;

            btnClose = new Button
            {
                Text = "Cancel / Close",
                Location = new Point(490, 420),
                Size = new Size(114, 36)
            };
            btnClose.BackColor = Color.FromArgb(241, 245, 249);
            btnClose.ForeColor = TextSecondary;
            btnClose.FlatStyle = FlatStyle.Flat;
            btnClose.FlatAppearance.BorderColor = Color.FromArgb(203, 213, 225);
            btnClose.Font = new Font("Segoe UI", 8.5F, FontStyle.Bold);
            btnClose.Click += (s, e) => this.Close();

            this.Controls.Add(btnTransfer);
            this.Controls.Add(btnClose);
        }

        private void PopulateAccountDropdowns()
        {
            cboSource.Items.Clear();
            cboDestination.Items.Clear();

            if (_customer.Accounts.Count == 0)
            {
                ShowWarning("This customer has no active accounts to transfer between.", "No Accounts");
                btnTransfer.Enabled = false;
                return;
            }

            foreach (var acc in _customer.Accounts)
            {
                string display = $"{acc.AccountNumber} ({acc.AccountType}) - ${acc.Balance:F2}";
                cboSource.Items.Add(display);
                cboDestination.Items.Add(display);
            }

            cboSource.SelectedIndex = 0;
            if (cboDestination.Items.Count > 1)
            {
                cboDestination.SelectedIndex = 1;
            }
            else
            {
                cboDestination.SelectedIndex = 0;
            }

            UpdateAccountBalances();
        }

        private void UpdateAccountBalances()
        {
            if (cboSource.SelectedIndex >= 0 && cboSource.SelectedIndex < _customer.Accounts.Count)
            {
                var srcAcc = _customer.Accounts[cboSource.SelectedIndex];
                lblSourceBalance.Text = $"Avail: ${srcAcc.Balance:F2}";
            }

            if (cboDestination.SelectedIndex >= 0 && cboDestination.SelectedIndex < _customer.Accounts.Count)
            {
                var destAcc = _customer.Accounts[cboDestination.SelectedIndex];
                lblDestBalance.Text = $"Avail: ${destAcc.Balance:F2}";
            }
        }

        private void BtnTransfer_Click(object? sender, EventArgs e)
        {
            if (cboSource.SelectedIndex < 0 || cboDestination.SelectedIndex < 0)
            {
                ShowWarning("Please select both a Source and Destination account.", "Missing Selection");
                return;
            }

            if (cboSource.SelectedIndex == cboDestination.SelectedIndex)
            {
                ShowWarning("Source and Destination accounts cannot be the same account.", "Identical Accounts");
                return;
            }

            if (!decimal.TryParse(txtAmount.Text.Trim(), out decimal amount) || amount <= 0)
            {
                ShowWarning("Please enter a valid transfer amount greater than $0.00.", "Invalid Amount");
                txtAmount.Focus();
                return;
            }

            Account sourceAcc = _customer.Accounts[cboSource.SelectedIndex];
            Account destAcc = _customer.Accounts[cboDestination.SelectedIndex];

            try
            {
                // Delegate operation to MVC Controller
                bool success = _controller.Transfer(_customer, sourceAcc, destAcc, amount);
                if (success)
                {
                    ShowSuccess(
                        $"Successfully transferred ${amount:F2} from {sourceAcc.AccountNumber} to {destAcc.AccountNumber}!\n\n" +
                        $"New {sourceAcc.AccountNumber} Balance: ${sourceAcc.Balance:F2}\n" +
                        $"New {destAcc.AccountNumber} Balance: ${destAcc.Balance:F2}",
                        "Transfer Approved");

                    _onTransferCompleted?.Invoke();
                    this.Close();
                }
            }
            catch (InsufficientFundsException ex)
            {
                ShowError(ex.Message, "Transfer Declined — Insufficient Funds");
                UpdateAccountBalances();
                _onTransferCompleted?.Invoke();
            }
            catch (BankingException ex)
            {
                ShowWarning(ex.Message, "Banking Rule Alert");
            }
            catch (Exception ex)
            {
                ShowError("An unexpected error occurred during transfer: " + ex.Message, "System Error");
            }
        }
    }
}
