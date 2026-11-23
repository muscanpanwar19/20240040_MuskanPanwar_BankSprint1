using System;
using System.Drawing;
using System.Windows.Forms;

namespace GUI
{
    /// <summary>
    /// Base form demonstrating Visual and Structural Form Inheritance for Nova Premier Bank.
    /// Standardizes corporate branding (slate navy palette), Segoe UI typography,
    /// application icons, dialog styles, and common UI helper utilities across all views.
    /// </summary>
    public class BaseBankForm : Form
    {
        // Corporate Brand Colors
        public static readonly Color BrandNavy = Color.FromArgb(15, 23, 42);      // Slate 900
        public static readonly Color BrandHeaderBlue = Color.FromArgb(30, 41, 59); // Slate 800
        public static readonly Color BackgroundColor = Color.FromArgb(241, 245, 249); // Slate 100
        public static readonly Color CardWhite = Color.White;
        public static readonly Color TextPrimary = Color.FromArgb(15, 23, 42);
        public static readonly Color TextSecondary = Color.FromArgb(71, 85, 105);
        public static readonly Color AccentBlue = Color.FromArgb(14, 165, 233);
        public static readonly Color SuccessGreen = Color.FromArgb(21, 128, 61);
        public static readonly Color DangerRed = Color.FromArgb(185, 28, 28);
        public static readonly Color WarningAmber = Color.FromArgb(217, 119, 6);

        public BaseBankForm()
        {
            // Apply standard design defaults to all inherited forms
            this.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
            this.BackColor = BackgroundColor;
            this.ForeColor = TextPrimary;
            this.StartPosition = FormStartPosition.CenterParent;
        }

        /// <summary>
        /// Creates a branded Nova Premier Bank header banner panel for any inherited window.
        /// </summary>
        protected Panel CreateBrandedHeader(string screenTitle, string subtitle, int width = 600, int height = 65)
        {
            Panel panel = new Panel
            {
                Dock = DockStyle.Top,
                Height = height,
                BackColor = BrandNavy,
                Padding = new Padding(16, 10, 16, 10)
            };

            Label lblLogo = new Label
            {
                Text = "NOVA PREMIER BANK",
                Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point),
                ForeColor = Color.White,
                Location = new Point(16, 10),
                AutoSize = true
            };

            Label lblSub = new Label
            {
                Text = $"{screenTitle} — {subtitle}",
                Font = new Font("Segoe UI", 8.5F, FontStyle.Regular, GraphicsUnit.Point),
                ForeColor = Color.FromArgb(148, 163, 184),
                Location = new Point(18, 36),
                AutoSize = true
            };

            panel.Controls.Add(lblLogo);
            panel.Controls.Add(lblSub);
            return panel;
        }

        /// <summary>
        /// Displays a standardized success notification alert.
        /// </summary>
        public void ShowSuccess(string message, string title = "Transaction Successful")
        {
            MessageBox.Show(message, title, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        /// <summary>
        /// Displays a standardized warning notification alert.
        /// </summary>
        public void ShowWarning(string message, string title = "Validation Warning")
        {
            MessageBox.Show(message, title, MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        /// <summary>
        /// Displays a standardized error notification alert.
        /// </summary>
        public void ShowError(string message, string title = "Banking Operation Error")
        {
            MessageBox.Show(message, title, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        /// <summary>
        /// Styles a button with the corporate primary theme.
        /// </summary>
        public void StylePrimaryButton(Button btn)
        {
            btn.BackColor = Color.FromArgb(240, 249, 255);
            btn.ForeColor = Color.FromArgb(3, 105, 161);
            btn.FlatStyle = FlatStyle.Flat;
            btn.FlatAppearance.BorderColor = Color.FromArgb(186, 230, 253);
            btn.Font = new Font("Segoe UI", 8.5F, FontStyle.Bold);
            btn.Cursor = Cursors.Hand;
        }

        /// <summary>
        /// Styles a button with the corporate success green theme.
        /// </summary>
        public void StyleSuccessButton(Button btn)
        {
            btn.BackColor = Color.FromArgb(240, 253, 244);
            btn.ForeColor = SuccessGreen;
            btn.FlatStyle = FlatStyle.Flat;
            btn.FlatAppearance.BorderColor = Color.FromArgb(187, 247, 208);
            btn.Font = new Font("Segoe UI", 8.5F, FontStyle.Bold);
            btn.Cursor = Cursors.Hand;
        }

        /// <summary>
        /// Styles a button with the danger red theme.
        /// </summary>
        public void StyleDangerButton(Button btn)
        {
            btn.BackColor = Color.FromArgb(254, 242, 242);
            btn.ForeColor = DangerRed;
            btn.FlatStyle = FlatStyle.Flat;
            btn.FlatAppearance.BorderColor = Color.FromArgb(252, 165, 165);
            btn.Font = new Font("Segoe UI", 8.5F, FontStyle.Bold);
            btn.Cursor = Cursors.Hand;
        }
    }
}
