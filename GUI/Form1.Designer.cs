namespace GUI
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.panelHeader = new System.Windows.Forms.Panel();
            this.lblRoleBadge = new System.Windows.Forms.Label();
            this.lblActiveCustomer = new System.Windows.Forms.Label();
            this.lblTagline = new System.Windows.Forms.Label();
            this.lblBrandLogo = new System.Windows.Forms.Label();
            this.grpCustomerManagement = new System.Windows.Forms.GroupBox();
            this.lblCustMessage = new System.Windows.Forms.Label();
            this.btnCustClear = new System.Windows.Forms.Button();
            this.btnCustDelete = new System.Windows.Forms.Button();
            this.btnCustUpdate = new System.Windows.Forms.Button();
            this.btnCustCreate = new System.Windows.Forms.Button();
            this.chkCustStaff = new System.Windows.Forms.CheckBox();
            this.txtCustContact = new System.Windows.Forms.TextBox();
            this.lblCustContact = new System.Windows.Forms.Label();
            this.txtCustName = new System.Windows.Forms.TextBox();
            this.lblCustName = new System.Windows.Forms.Label();
            this.txtCustId = new System.Windows.Forms.TextBox();
            this.lblCustId = new System.Windows.Forms.Label();
            this.cboCustomers = new System.Windows.Forms.ComboBox();
            this.lblSelectCustomer = new System.Windows.Forms.Label();
            this.grpAccount = new System.Windows.Forms.GroupBox();
            this.btnAddAccount = new System.Windows.Forms.Button();
            this.lblAccountRules = new System.Windows.Forms.Label();
            this.lblBalanceValue = new System.Windows.Forms.Label();
            this.lblBalanceHeader = new System.Windows.Forms.Label();
            this.lblAccountNumber = new System.Windows.Forms.Label();
            this.lblSelectAccount = new System.Windows.Forms.Label();
            this.cboAccounts = new System.Windows.Forms.ComboBox();
            this.grpTransactions = new System.Windows.Forms.GroupBox();
            this.btnTransfer = new System.Windows.Forms.Button();
            this.lblStatusMessage = new System.Windows.Forms.Label();
            this.btnInterest = new System.Windows.Forms.Button();
            this.btnWithdraw = new System.Windows.Forms.Button();
            this.btnDeposit = new System.Windows.Forms.Button();
            this.txtAmount = new System.Windows.Forms.TextBox();
            this.lblAmount = new System.Windows.Forms.Label();
            this.grpHistory = new System.Windows.Forms.GroupBox();
            this.btnReloadState = new System.Windows.Forms.Button();
            this.btnSaveState = new System.Windows.Forms.Button();
            this.btnClearHistory = new System.Windows.Forms.Button();
            this.lstHistory = new System.Windows.Forms.ListBox();
            this.panelHeader.SuspendLayout();
            this.grpCustomerManagement.SuspendLayout();
            this.grpAccount.SuspendLayout();
            this.grpTransactions.SuspendLayout();
            this.grpHistory.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelHeader
            // 
            this.panelHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this.panelHeader.Controls.Add(this.lblRoleBadge);
            this.panelHeader.Controls.Add(this.lblActiveCustomer);
            this.panelHeader.Controls.Add(this.lblTagline);
            this.panelHeader.Controls.Add(this.lblBrandLogo);
            this.panelHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelHeader.Location = new System.Drawing.Point(0, 0);
            this.panelHeader.Name = "panelHeader";
            this.panelHeader.Size = new System.Drawing.Size(1115, 65);
            this.panelHeader.TabIndex = 0;
            // 
            // lblRoleBadge
            // 
            this.lblRoleBadge.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblRoleBadge.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            this.lblRoleBadge.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRoleBadge.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(158)))), ((int)(((byte)(11)))));
            this.lblRoleBadge.Location = new System.Drawing.Point(825, 34);
            this.lblRoleBadge.Name = "lblRoleBadge";
            this.lblRoleBadge.Size = new System.Drawing.Size(275, 22);
            this.lblRoleBadge.TabIndex = 3;
            this.lblRoleBadge.Text = "Role: Regular Customer";
            this.lblRoleBadge.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblActiveCustomer
            // 
            this.lblActiveCustomer.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblActiveCustomer.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblActiveCustomer.ForeColor = System.Drawing.Color.White;
            this.lblActiveCustomer.Location = new System.Drawing.Point(700, 10);
            this.lblActiveCustomer.Name = "lblActiveCustomer";
            this.lblActiveCustomer.Size = new System.Drawing.Size(400, 20);
            this.lblActiveCustomer.TabIndex = 2;
            this.lblActiveCustomer.Text = "Active: Muskan Panwar (20240040)";
            this.lblActiveCustomer.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblTagline
            // 
            this.lblTagline.AutoSize = true;
            this.lblTagline.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTagline.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(148)))), ((int)(((byte)(163)))), ((int)(((byte)(184)))));
            this.lblTagline.Location = new System.Drawing.Point(18, 38);
            this.lblTagline.Name = "lblTagline";
            this.lblTagline.Size = new System.Drawing.Size(465, 13);
            this.lblTagline.TabIndex = 1;
            this.lblTagline.Text = "Sprint 3: JSON Persistence | Form Inheritance | Intra-Account Transfers | Dynamic Accounts";
            // 
            // lblBrandLogo
            // 
            this.lblBrandLogo.AutoSize = true;
            this.lblBrandLogo.Font = new System.Drawing.Font("Segoe UI", 13F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBrandLogo.ForeColor = System.Drawing.Color.White;
            this.lblBrandLogo.Location = new System.Drawing.Point(16, 10);
            this.lblBrandLogo.Name = "lblBrandLogo";
            this.lblBrandLogo.Size = new System.Drawing.Size(215, 25);
            this.lblBrandLogo.TabIndex = 0;
            this.lblBrandLogo.Text = "NOVA PREMIER BANK";
            // 
            // grpCustomerManagement
            // 
            this.grpCustomerManagement.BackColor = System.Drawing.Color.White;
            this.grpCustomerManagement.Controls.Add(this.lblCustMessage);
            this.grpCustomerManagement.Controls.Add(this.btnCustClear);
            this.grpCustomerManagement.Controls.Add(this.btnCustDelete);
            this.grpCustomerManagement.Controls.Add(this.btnCustUpdate);
            this.grpCustomerManagement.Controls.Add(this.btnCustCreate);
            this.grpCustomerManagement.Controls.Add(this.chkCustStaff);
            this.grpCustomerManagement.Controls.Add(this.txtCustContact);
            this.grpCustomerManagement.Controls.Add(this.lblCustContact);
            this.grpCustomerManagement.Controls.Add(this.txtCustName);
            this.grpCustomerManagement.Controls.Add(this.lblCustName);
            this.grpCustomerManagement.Controls.Add(this.txtCustId);
            this.grpCustomerManagement.Controls.Add(this.lblCustId);
            this.grpCustomerManagement.Controls.Add(this.cboCustomers);
            this.grpCustomerManagement.Controls.Add(this.lblSelectCustomer);
            this.grpCustomerManagement.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpCustomerManagement.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this.grpCustomerManagement.Location = new System.Drawing.Point(16, 75);
            this.grpCustomerManagement.Name = "grpCustomerManagement";
            this.grpCustomerManagement.Size = new System.Drawing.Size(550, 210);
            this.grpCustomerManagement.TabIndex = 1;
            this.grpCustomerManagement.TabStop = false;
            this.grpCustomerManagement.Text = " Customer Management (MVC Controller) ";
            // 
            // lblCustMessage
            // 
            this.lblCustMessage.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(250)))), ((int)(((byte)(252)))));
            this.lblCustMessage.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblCustMessage.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCustMessage.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            this.lblCustMessage.Location = new System.Drawing.Point(15, 158);
            this.lblCustMessage.Name = "lblCustMessage";
            this.lblCustMessage.Padding = new System.Windows.Forms.Padding(4);
            this.lblCustMessage.Size = new System.Drawing.Size(520, 40);
            this.lblCustMessage.TabIndex = 13;
            this.lblCustMessage.Text = "Select a customer to view or edit, or enter details to create a new record.";
            this.lblCustMessage.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // btnCustClear
            // 
            this.btnCustClear.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(245)))), ((int)(((byte)(249)))));
            this.btnCustClear.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(203)))), ((int)(((byte)(213)))), ((int)(((byte)(225)))));
            this.btnCustClear.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCustClear.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCustClear.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            this.btnCustClear.Location = new System.Drawing.Point(410, 118);
            this.btnCustClear.Name = "btnCustClear";
            this.btnCustClear.Size = new System.Drawing.Size(125, 30);
            this.btnCustClear.TabIndex = 12;
            this.btnCustClear.Text = "Reset Form";
            this.btnCustClear.UseVisualStyleBackColor = false;
            this.btnCustClear.Click += new System.EventHandler(this.btnCustClear_Click);
            // 
            // btnCustDelete
            // 
            this.btnCustDelete.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(242)))), ((int)(((byte)(242)))));
            this.btnCustDelete.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(252)))), ((int)(((byte)(165)))), ((int)(((byte)(165)))));
            this.btnCustDelete.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCustDelete.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCustDelete.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(185)))), ((int)(((byte)(28)))), ((int)(((byte)(28)))));
            this.btnCustDelete.Location = new System.Drawing.Point(275, 118);
            this.btnCustDelete.Name = "btnCustDelete";
            this.btnCustDelete.Size = new System.Drawing.Size(125, 30);
            this.btnCustDelete.TabIndex = 11;
            this.btnCustDelete.Text = "Delete Customer";
            this.btnCustDelete.UseVisualStyleBackColor = false;
            this.btnCustDelete.Click += new System.EventHandler(this.btnCustDelete_Click);
            // 
            // btnCustUpdate
            // 
            this.btnCustUpdate.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(249)))), ((int)(((byte)(255)))));
            this.btnCustUpdate.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(186)))), ((int)(((byte)(230)))), ((int)(((byte)(253)))));
            this.btnCustUpdate.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCustUpdate.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCustUpdate.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(3)))), ((int)(((byte)(105)))), ((int)(((byte)(161)))));
            this.btnCustUpdate.Location = new System.Drawing.Point(145, 118);
            this.btnCustUpdate.Name = "btnCustUpdate";
            this.btnCustUpdate.Size = new System.Drawing.Size(120, 30);
            this.btnCustUpdate.TabIndex = 10;
            this.btnCustUpdate.Text = "Update Details";
            this.btnCustUpdate.UseVisualStyleBackColor = false;
            this.btnCustUpdate.Click += new System.EventHandler(this.btnCustUpdate_Click);
            // 
            // btnCustCreate
            // 
            this.btnCustCreate.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(253)))), ((int)(((byte)(244)))));
            this.btnCustCreate.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(187)))), ((int)(((byte)(247)))), ((int)(((byte)(208)))));
            this.btnCustCreate.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCustCreate.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCustCreate.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(128)))), ((int)(((byte)(61)))));
            this.btnCustCreate.Location = new System.Drawing.Point(15, 118);
            this.btnCustCreate.Name = "btnCustCreate";
            this.btnCustCreate.Size = new System.Drawing.Size(120, 30);
            this.btnCustCreate.TabIndex = 9;
            this.btnCustCreate.Text = "Add Customer";
            this.btnCustCreate.UseVisualStyleBackColor = false;
            this.btnCustCreate.Click += new System.EventHandler(this.btnCustCreate_Click);
            // 
            // chkCustStaff
            // 
            this.chkCustStaff.AutoSize = true;
            this.chkCustStaff.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkCustStaff.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(217)))), ((int)(((byte)(119)))), ((int)(((byte)(6)))));
            this.chkCustStaff.Location = new System.Drawing.Point(405, 54);
            this.chkCustStaff.Name = "chkCustStaff";
            this.chkCustStaff.Size = new System.Drawing.Size(130, 17);
            this.chkCustStaff.TabIndex = 8;
            this.chkCustStaff.Text = "Staff (50% Discount)";
            this.chkCustStaff.UseVisualStyleBackColor = true;
            // 
            // txtCustContact
            // 
            this.txtCustContact.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCustContact.Location = new System.Drawing.Point(100, 84);
            this.txtCustContact.Name = "txtCustContact";
            this.txtCustContact.Size = new System.Drawing.Size(435, 23);
            this.txtCustContact.TabIndex = 7;
            // 
            // lblCustContact
            // 
            this.lblCustContact.AutoSize = true;
            this.lblCustContact.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCustContact.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            this.lblCustContact.Location = new System.Drawing.Point(15, 87);
            this.lblCustContact.Name = "lblCustContact";
            this.lblCustContact.Size = new System.Drawing.Size(50, 13);
            this.lblCustContact.TabIndex = 6;
            this.lblCustContact.Text = "Contact:";
            // 
            // txtCustName
            // 
            this.txtCustName.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCustName.Location = new System.Drawing.Point(270, 52);
            this.txtCustName.Name = "txtCustName";
            this.txtCustName.Size = new System.Drawing.Size(125, 23);
            this.txtCustName.TabIndex = 5;
            // 
            // lblCustName
            // 
            this.lblCustName.AutoSize = true;
            this.lblCustName.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCustName.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            this.lblCustName.Location = new System.Drawing.Point(205, 55);
            this.lblCustName.Name = "lblCustName";
            this.lblCustName.Size = new System.Drawing.Size(61, 13);
            this.lblCustName.TabIndex = 4;
            this.lblCustName.Text = "Full Name:";
            // 
            // txtCustId
            // 
            this.txtCustId.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCustId.Location = new System.Drawing.Point(100, 52);
            this.txtCustId.Name = "txtCustId";
            this.txtCustId.Size = new System.Drawing.Size(95, 23);
            this.txtCustId.TabIndex = 3;
            // 
            // lblCustId
            // 
            this.lblCustId.AutoSize = true;
            this.lblCustId.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCustId.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            this.lblCustId.Location = new System.Drawing.Point(15, 55);
            this.lblCustId.Name = "lblCustId";
            this.lblCustId.Size = new System.Drawing.Size(74, 13);
            this.lblCustId.TabIndex = 2;
            this.lblCustId.Text = "Customer ID:";
            // 
            // cboCustomers
            // 
            this.cboCustomers.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboCustomers.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboCustomers.FormattingEnabled = true;
            this.cboCustomers.Location = new System.Drawing.Point(120, 22);
            this.cboCustomers.Name = "cboCustomers";
            this.cboCustomers.Size = new System.Drawing.Size(415, 21);
            this.cboCustomers.TabIndex = 1;
            this.cboCustomers.SelectedIndexChanged += new System.EventHandler(this.cboCustomers_SelectedIndexChanged);
            // 
            // lblSelectCustomer
            // 
            this.lblSelectCustomer.AutoSize = true;
            this.lblSelectCustomer.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSelectCustomer.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            this.lblSelectCustomer.Location = new System.Drawing.Point(15, 25);
            this.lblSelectCustomer.Name = "lblSelectCustomer";
            this.lblSelectCustomer.Size = new System.Drawing.Size(94, 13);
            this.lblSelectCustomer.TabIndex = 0;
            this.lblSelectCustomer.Text = "Select Customer:";
            // 
            // grpAccount
            // 
            this.grpAccount.BackColor = System.Drawing.Color.White;
            this.grpAccount.Controls.Add(this.btnAddAccount);
            this.grpAccount.Controls.Add(this.lblAccountRules);
            this.grpAccount.Controls.Add(this.lblBalanceValue);
            this.grpAccount.Controls.Add(this.lblBalanceHeader);
            this.grpAccount.Controls.Add(this.lblAccountNumber);
            this.grpAccount.Controls.Add(this.lblSelectAccount);
            this.grpAccount.Controls.Add(this.cboAccounts);
            this.grpAccount.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpAccount.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this.grpAccount.Location = new System.Drawing.Point(16, 292);
            this.grpAccount.Name = "grpAccount";
            this.grpAccount.Size = new System.Drawing.Size(550, 125);
            this.grpAccount.TabIndex = 2;
            this.grpAccount.TabStop = false;
            this.grpAccount.Text = " Account Overview & Allocation ";
            // 
            // btnAddAccount
            // 
            this.btnAddAccount.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(253)))), ((int)(((byte)(244)))));
            this.btnAddAccount.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(187)))), ((int)(((byte)(247)))), ((int)(((byte)(208)))));
            this.btnAddAccount.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAddAccount.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAddAccount.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(128)))), ((int)(((byte)(61)))));
            this.btnAddAccount.Location = new System.Drawing.Point(410, 18);
            this.btnAddAccount.Name = "btnAddAccount";
            this.btnAddAccount.Size = new System.Drawing.Size(125, 26);
            this.btnAddAccount.TabIndex = 6;
            this.btnAddAccount.Text = "+ Add Account";
            this.btnAddAccount.UseVisualStyleBackColor = false;
            this.btnAddAccount.Click += new System.EventHandler(this.btnAddAccount_Click);
            // 
            // lblAccountRules
            // 
            this.lblAccountRules.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(245)))), ((int)(((byte)(249)))));
            this.lblAccountRules.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblAccountRules.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAccountRules.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(65)))), ((int)(((byte)(85)))));
            this.lblAccountRules.Location = new System.Drawing.Point(160, 52);
            this.lblAccountRules.Name = "lblAccountRules";
            this.lblAccountRules.Padding = new System.Windows.Forms.Padding(4);
            this.lblAccountRules.Size = new System.Drawing.Size(375, 60);
            this.lblAccountRules.TabIndex = 5;
            this.lblAccountRules.Text = "• Features: No Interest | No Overdraft | $0 Transaction Fee";
            this.lblAccountRules.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblBalanceValue
            // 
            this.lblBalanceValue.AutoSize = true;
            this.lblBalanceValue.Font = new System.Drawing.Font("Segoe UI", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBalanceValue.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(14)))), ((int)(((byte)(165)))), ((int)(((byte)(233)))));
            this.lblBalanceValue.Location = new System.Drawing.Point(15, 82);
            this.lblBalanceValue.Name = "lblBalanceValue";
            this.lblBalanceValue.Size = new System.Drawing.Size(89, 28);
            this.lblBalanceValue.TabIndex = 4;
            this.lblBalanceValue.Text = "$500.00";
            // 
            // lblBalanceHeader
            // 
            this.lblBalanceHeader.AutoSize = true;
            this.lblBalanceHeader.Font = new System.Drawing.Font("Segoe UI", 7.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBalanceHeader.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this.lblBalanceHeader.Location = new System.Drawing.Point(15, 68);
            this.lblBalanceHeader.Name = "lblBalanceHeader";
            this.lblBalanceHeader.Size = new System.Drawing.Size(99, 12);
            this.lblBalanceHeader.TabIndex = 3;
            this.lblBalanceHeader.Text = "CURRENT BALANCE";
            // 
            // lblAccountNumber
            // 
            this.lblAccountNumber.AutoSize = true;
            this.lblAccountNumber.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAccountNumber.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            this.lblAccountNumber.Location = new System.Drawing.Point(15, 50);
            this.lblAccountNumber.Name = "lblAccountNumber";
            this.lblAccountNumber.Size = new System.Drawing.Size(137, 13);
            this.lblAccountNumber.TabIndex = 2;
            this.lblAccountNumber.Text = "Account No: ED-20240040";
            // 
            // lblSelectAccount
            // 
            this.lblSelectAccount.AutoSize = true;
            this.lblSelectAccount.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSelectAccount.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            this.lblSelectAccount.Location = new System.Drawing.Point(15, 23);
            this.lblSelectAccount.Name = "lblSelectAccount";
            this.lblSelectAccount.Size = new System.Drawing.Size(86, 13);
            this.lblSelectAccount.TabIndex = 1;
            this.lblSelectAccount.Text = "Select Account:";
            // 
            // cboAccounts
            // 
            this.cboAccounts.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboAccounts.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboAccounts.FormattingEnabled = true;
            this.cboAccounts.Location = new System.Drawing.Point(120, 20);
            this.cboAccounts.Name = "cboAccounts";
            this.cboAccounts.Size = new System.Drawing.Size(280, 21);
            this.cboAccounts.TabIndex = 0;
            this.cboAccounts.SelectedIndexChanged += new System.EventHandler(this.cboAccounts_SelectedIndexChanged);
            // 
            // grpTransactions
            // 
            this.grpTransactions.BackColor = System.Drawing.Color.White;
            this.grpTransactions.Controls.Add(this.btnTransfer);
            this.grpTransactions.Controls.Add(this.lblStatusMessage);
            this.grpTransactions.Controls.Add(this.btnInterest);
            this.grpTransactions.Controls.Add(this.btnWithdraw);
            this.grpTransactions.Controls.Add(this.btnDeposit);
            this.grpTransactions.Controls.Add(this.txtAmount);
            this.grpTransactions.Controls.Add(this.lblAmount);
            this.grpTransactions.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpTransactions.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this.grpTransactions.Location = new System.Drawing.Point(16, 424);
            this.grpTransactions.Name = "grpTransactions";
            this.grpTransactions.Size = new System.Drawing.Size(550, 155);
            this.grpTransactions.TabIndex = 3;
            this.grpTransactions.TabStop = false;
            this.grpTransactions.Text = " Financial Operations & Intra-Account Transfers ";
            // 
            // btnTransfer
            // 
            this.btnTransfer.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(249)))), ((int)(((byte)(255)))));
            this.btnTransfer.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(186)))), ((int)(((byte)(230)))), ((int)(((byte)(253)))));
            this.btnTransfer.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnTransfer.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnTransfer.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(3)))), ((int)(((byte)(105)))), ((int)(((byte)(161)))));
            this.btnTransfer.Location = new System.Drawing.Point(280, 53);
            this.btnTransfer.Name = "btnTransfer";
            this.btnTransfer.Size = new System.Drawing.Size(255, 28);
            this.btnTransfer.TabIndex = 6;
            this.btnTransfer.Text = "⇄ Intra-Account Transfer...";
            this.btnTransfer.UseVisualStyleBackColor = false;
            this.btnTransfer.Click += new System.EventHandler(this.btnTransfer_Click);
            // 
            // lblStatusMessage
            // 
            this.lblStatusMessage.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(250)))), ((int)(((byte)(252)))));
            this.lblStatusMessage.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblStatusMessage.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStatusMessage.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this.lblStatusMessage.Location = new System.Drawing.Point(15, 87);
            this.lblStatusMessage.Name = "lblStatusMessage";
            this.lblStatusMessage.Padding = new System.Windows.Forms.Padding(4);
            this.lblStatusMessage.Size = new System.Drawing.Size(520, 55);
            this.lblStatusMessage.TabIndex = 5;
            this.lblStatusMessage.Text = "Ready for transactions.";
            // 
            // btnInterest
            // 
            this.btnInterest.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(250)))), ((int)(((byte)(252)))));
            this.btnInterest.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(203)))), ((int)(((byte)(213)))), ((int)(((byte)(225)))));
            this.btnInterest.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnInterest.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnInterest.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this.btnInterest.Location = new System.Drawing.Point(15, 53);
            this.btnInterest.Name = "btnInterest";
            this.btnInterest.Size = new System.Drawing.Size(255, 28);
            this.btnInterest.TabIndex = 4;
            this.btnInterest.Text = "% Apply Interest";
            this.btnInterest.UseVisualStyleBackColor = false;
            this.btnInterest.Click += new System.EventHandler(this.btnInterest_Click);
            // 
            // btnWithdraw
            // 
            this.btnWithdraw.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(242)))), ((int)(((byte)(242)))));
            this.btnWithdraw.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(252)))), ((int)(((byte)(165)))), ((int)(((byte)(165)))));
            this.btnWithdraw.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnWithdraw.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnWithdraw.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(185)))), ((int)(((byte)(28)))), ((int)(((byte)(28)))));
            this.btnWithdraw.Location = new System.Drawing.Point(405, 19);
            this.btnWithdraw.Name = "btnWithdraw";
            this.btnWithdraw.Size = new System.Drawing.Size(130, 28);
            this.btnWithdraw.TabIndex = 3;
            this.btnWithdraw.Text = "- Withdraw";
            this.btnWithdraw.UseVisualStyleBackColor = false;
            this.btnWithdraw.Click += new System.EventHandler(this.btnWithdraw_Click);
            // 
            // btnDeposit
            // 
            this.btnDeposit.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(253)))), ((int)(((byte)(244)))));
            this.btnDeposit.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(187)))), ((int)(((byte)(247)))), ((int)(((byte)(208)))));
            this.btnDeposit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDeposit.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDeposit.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(128)))), ((int)(((byte)(61)))));
            this.btnDeposit.Location = new System.Drawing.Point(265, 19);
            this.btnDeposit.Name = "btnDeposit";
            this.btnDeposit.Size = new System.Drawing.Size(130, 28);
            this.btnDeposit.TabIndex = 2;
            this.btnDeposit.Text = "+ Deposit";
            this.btnDeposit.UseVisualStyleBackColor = false;
            this.btnDeposit.Click += new System.EventHandler(this.btnDeposit_Click);
            // 
            // txtAmount
            // 
            this.txtAmount.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtAmount.Location = new System.Drawing.Point(160, 22);
            this.txtAmount.Name = "txtAmount";
            this.txtAmount.Size = new System.Drawing.Size(95, 23);
            this.txtAmount.TabIndex = 1;
            this.txtAmount.Text = "100.00";
            // 
            // lblAmount
            // 
            this.lblAmount.AutoSize = true;
            this.lblAmount.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAmount.Location = new System.Drawing.Point(15, 26);
            this.lblAmount.Name = "lblAmount";
            this.lblAmount.Size = new System.Drawing.Size(133, 13);
            this.lblAmount.TabIndex = 0;
            this.lblAmount.Text = "Transaction Amount ($):";
            // 
            // grpHistory
            // 
            this.grpHistory.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grpHistory.BackColor = System.Drawing.Color.White;
            this.grpHistory.Controls.Add(this.btnReloadState);
            this.grpHistory.Controls.Add(this.btnSaveState);
            this.grpHistory.Controls.Add(this.btnClearHistory);
            this.grpHistory.Controls.Add(this.lstHistory);
            this.grpHistory.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpHistory.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this.grpHistory.Location = new System.Drawing.Point(580, 75);
            this.grpHistory.Name = "grpHistory";
            this.grpHistory.Size = new System.Drawing.Size(515, 504);
            this.grpHistory.TabIndex = 4;
            this.grpHistory.TabStop = false;
            this.grpHistory.Text = " Activity & Audit Log (Persistence & MVC Operations) ";
            // 
            // btnReloadState
            // 
            this.btnReloadState.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnReloadState.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(245)))), ((int)(((byte)(249)))));
            this.btnReloadState.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(203)))), ((int)(((byte)(213)))), ((int)(((byte)(225)))));
            this.btnReloadState.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnReloadState.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnReloadState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            this.btnReloadState.Location = new System.Drawing.Point(135, 465);
            this.btnReloadState.Name = "btnReloadState";
            this.btnReloadState.Size = new System.Drawing.Size(110, 28);
            this.btnReloadState.TabIndex = 3;
            this.btnReloadState.Text = "📂 Load JSON";
            this.btnReloadState.UseVisualStyleBackColor = false;
            this.btnReloadState.Click += new System.EventHandler(this.btnReloadState_Click);
            // 
            // btnSaveState
            // 
            this.btnSaveState.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnSaveState.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(249)))), ((int)(((byte)(255)))));
            this.btnSaveState.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(186)))), ((int)(((byte)(230)))), ((int)(((byte)(253)))));
            this.btnSaveState.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSaveState.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSaveState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(3)))), ((int)(((byte)(105)))), ((int)(((byte)(161)))));
            this.btnSaveState.Location = new System.Drawing.Point(15, 465);
            this.btnSaveState.Name = "btnSaveState";
            this.btnSaveState.Size = new System.Drawing.Size(110, 28);
            this.btnSaveState.TabIndex = 2;
            this.btnSaveState.Text = "💾 Save JSON";
            this.btnSaveState.UseVisualStyleBackColor = false;
            this.btnSaveState.Click += new System.EventHandler(this.btnSaveState_Click);
            // 
            // btnClearHistory
            // 
            this.btnClearHistory.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClearHistory.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(245)))), ((int)(((byte)(249)))));
            this.btnClearHistory.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(203)))), ((int)(((byte)(213)))), ((int)(((byte)(225)))));
            this.btnClearHistory.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClearHistory.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClearHistory.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            this.btnClearHistory.Location = new System.Drawing.Point(375, 465);
            this.btnClearHistory.Name = "btnClearHistory";
            this.btnClearHistory.Size = new System.Drawing.Size(125, 28);
            this.btnClearHistory.TabIndex = 1;
            this.btnClearHistory.Text = "Clear History";
            this.btnClearHistory.UseVisualStyleBackColor = false;
            this.btnClearHistory.Click += new System.EventHandler(this.btnClearHistory_Click);
            // 
            // lstHistory
            // 
            this.lstHistory.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lstHistory.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(250)))), ((int)(((byte)(252)))));
            this.lstHistory.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lstHistory.Font = new System.Drawing.Font("Consolas", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lstHistory.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            this.lstHistory.FormattingEnabled = true;
            this.lstHistory.HorizontalScrollbar = true;
            this.lstHistory.ItemHeight = 13;
            this.lstHistory.Location = new System.Drawing.Point(15, 25);
            this.lstHistory.Name = "lstHistory";
            this.lstHistory.Size = new System.Drawing.Size(485, 431);
            this.lstHistory.TabIndex = 0;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(245)))), ((int)(((byte)(249)))));
            this.ClientSize = new System.Drawing.Size(1115, 595);
            this.Controls.Add(this.grpHistory);
            this.Controls.Add(this.grpTransactions);
            this.Controls.Add(this.grpAccount);
            this.Controls.Add(this.grpCustomerManagement);
            this.Controls.Add(this.panelHeader);
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Nova Premier Bank — Sprint 3 (Data Persistence & Intra-Account Transfers)";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.panelHeader.ResumeLayout(false);
            this.panelHeader.PerformLayout();
            this.grpCustomerManagement.ResumeLayout(false);
            this.grpCustomerManagement.PerformLayout();
            this.grpAccount.ResumeLayout(false);
            this.grpAccount.PerformLayout();
            this.grpTransactions.ResumeLayout(false);
            this.grpTransactions.PerformLayout();
            this.grpHistory.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelHeader;
        private System.Windows.Forms.Label lblBrandLogo;
        private System.Windows.Forms.Label lblTagline;
        private System.Windows.Forms.Label lblActiveCustomer;
        private System.Windows.Forms.Label lblRoleBadge;
        private System.Windows.Forms.GroupBox grpCustomerManagement;
        private System.Windows.Forms.Label lblSelectCustomer;
        private System.Windows.Forms.ComboBox cboCustomers;
        private System.Windows.Forms.Label lblCustId;
        private System.Windows.Forms.TextBox txtCustId;
        private System.Windows.Forms.Label lblCustName;
        private System.Windows.Forms.TextBox txtCustName;
        private System.Windows.Forms.Label lblCustContact;
        private System.Windows.Forms.TextBox txtCustContact;
        private System.Windows.Forms.CheckBox chkCustStaff;
        private System.Windows.Forms.Button btnCustCreate;
        private System.Windows.Forms.Button btnCustUpdate;
        private System.Windows.Forms.Button btnCustDelete;
        private System.Windows.Forms.Button btnCustClear;
        private System.Windows.Forms.Label lblCustMessage;
        private System.Windows.Forms.GroupBox grpAccount;
        private System.Windows.Forms.Label lblSelectAccount;
        private System.Windows.Forms.ComboBox cboAccounts;
        private System.Windows.Forms.Label lblAccountNumber;
        private System.Windows.Forms.Label lblBalanceHeader;
        private System.Windows.Forms.Label lblBalanceValue;
        private System.Windows.Forms.Label lblAccountRules;
        private System.Windows.Forms.Button btnAddAccount;
        private System.Windows.Forms.GroupBox grpTransactions;
        private System.Windows.Forms.Label lblAmount;
        private System.Windows.Forms.TextBox txtAmount;
        private System.Windows.Forms.Button btnDeposit;
        private System.Windows.Forms.Button btnWithdraw;
        private System.Windows.Forms.Button btnInterest;
        private System.Windows.Forms.Button btnTransfer;
        private System.Windows.Forms.Label lblStatusMessage;
        private System.Windows.Forms.GroupBox grpHistory;
        private System.Windows.Forms.ListBox lstHistory;
        private System.Windows.Forms.Button btnClearHistory;
        private System.Windows.Forms.Button btnSaveState;
        private System.Windows.Forms.Button btnReloadState;
    }
}
