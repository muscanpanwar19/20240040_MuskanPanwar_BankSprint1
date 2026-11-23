using System;
using System.Text.Json.Serialization;

namespace _20240040_MuskanPanwar_BankSprint1
{
    /// <summary>
    /// Abstract base class representing a generic bank account.
    /// Supports polymorphic JSON serialization using [JsonDerivedType] for Sprint 3 data persistence.
    /// </summary>
    [JsonPolymorphic(TypeDiscriminatorPropertyName = "$accountType")]
    [JsonDerivedType(typeof(EverydayAccount), typeDiscriminator: "Everyday")]
    [JsonDerivedType(typeof(InvestmentAccount), typeDiscriminator: "Investment")]
    [JsonDerivedType(typeof(OmniAccount), typeDiscriminator: "Omni")]
    public abstract class Account
    {
        // Protected fields accessible in derived classes
        protected string accountNumber;
        protected decimal balance;
        protected string lastTransactionStatus;
        protected Customer? owner;

        /// <summary>
        /// Unique account identification code (e.g., "ED-20240040", "INV-STAFF01").
        /// </summary>
        public string AccountNumber
        {
            get => accountNumber;
            set => accountNumber = value;
        }

        /// <summary>
        /// Current available monetary balance in the account.
        /// </summary>
        public decimal Balance
        {
            get => balance;
            set => balance = value;
        }

        /// <summary>
        /// Audit status message describing the outcome of the last attempted transaction.
        /// </summary>
        public string LastTransactionStatus
        {
            get => lastTransactionStatus;
            set => lastTransactionStatus = value;
        }

        /// <summary>
        /// The customer who owns this account.
        /// Ignored in JSON serialization to prevent circular references between Customer and Account.
        /// </summary>
        [JsonIgnore]
        public Customer? Owner
        {
            get => owner;
            set => owner = value;
        }

        /// <summary>
        /// Gets the human-readable account category (Everyday, Investment, or Omni).
        /// </summary>
        [JsonIgnore]
        public abstract string AccountType { get; }

        /// <summary>
        /// Base constructor initializing core account fields.
        /// </summary>
        /// <param name="accountNumber">Unique identifier for the account.</param>
        /// <param name="balance">Starting balance.</param>
        /// <param name="owner">Reference to the customer owner.</param>
        public Account(string accountNumber, decimal balance, Customer? owner = null)
        {
            this.accountNumber = accountNumber ?? string.Empty;
            this.balance = balance;
            this.owner = owner;
            this.lastTransactionStatus = "Account opened.";
        }

        /// <summary>
        /// Associates this account with its parent customer.
        /// </summary>
        /// <param name="customer">The owner customer.</param>
        public void SetOwner(Customer customer)
        {
            this.owner = customer;
        }

        /// <summary>
        /// Credits funds to the account.
        /// </summary>
        /// <param name="amount">Positive decimal amount to deposit.</param>
        /// <returns>True if successful.</returns>
        /// <exception cref="BankingException">Thrown when amount is less than or equal to zero.</exception>
        public virtual bool Deposit(decimal amount)
        {
            if (amount <= 0)
            {
                lastTransactionStatus = "Deposit Failed: Amount must be positive.";
                throw new BankingException("Deposit failed: Amount must be positive.", this, amount);
            }

            balance += amount;
            lastTransactionStatus = "Deposit Successful: $" + amount.ToString("F2");
            return true;
        }

        /// <summary>
        /// Debits funds from the account according to specific account overdraft and fee rules.
        /// </summary>
        /// <param name="amount">Positive decimal amount to withdraw.</param>
        /// <returns>True if withdrawal succeeds.</returns>
        /// <exception cref="BankingException">Thrown on invalid inputs or business rule violations.</exception>
        /// <exception cref="InsufficientFundsException">Thrown when balance/overdraft is exceeded.</exception>
        public abstract bool Withdraw(decimal amount);

        /// <summary>
        /// Calculates and applies interest to the account according to account-specific interest policies.
        /// </summary>
        /// <returns>The interest amount credited.</returns>
        public abstract decimal CalculateInterest();

        /// <summary>
        /// Calculates applicable fee with a 50% discount for bank staff members.
        /// </summary>
        /// <param name="standardFee">Standard baseline fee amount.</param>
        /// <returns>Discounted fee for staff ($5.00), or standard fee for regular customers ($10.00).</returns>
        protected decimal GetFee(decimal standardFee)
        {
            if (owner != null && owner.IsStaff)
            {
                return standardFee * 0.5m; // 50% staff benefit discount
            }
            return standardFee;
        }

        /// <summary>
        /// Returns a formatted string summary of account state.
        /// </summary>
        public virtual string GetAccountInfo()
        {
            return $"Account: {accountNumber} ({AccountType}) | Balance: ${balance:F2} | Status: {lastTransactionStatus}";
        }
    }
}
