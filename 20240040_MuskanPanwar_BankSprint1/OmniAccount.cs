using System;
using System.Text.Json.Serialization;

namespace _20240040_MuskanPanwar_BankSprint1
{
    /// <summary>
    /// Represents an Omni multipurpose bank account.
    /// Characteristics: Overdraft limit permitted, failed fee charged on overdraft breach, interest applies on balances exceeding $1,000.
    /// </summary>
    public class OmniAccount : Account
    {
        private decimal interestRate;
        private decimal overdraftLimit;
        private decimal failedFee = 10.00m;

        /// <summary>
        /// Variable interest rate percentage applied to balances over $1,000.
        /// </summary>
        public decimal InterestRate
        {
            get => interestRate;
            set => interestRate = value;
        }

        /// <summary>
        /// Approved credit overdraft buffer allowed below $0.00.
        /// </summary>
        public decimal OverdraftLimit
        {
            get => overdraftLimit;
            set => overdraftLimit = value;
        }

        /// <summary>
        /// Standard penalty fee charged when withdrawal exceeds balance + overdraftLimit ($10.00).
        /// </summary>
        public decimal FailedFee
        {
            get => failedFee;
            set => failedFee = value;
        }

        /// <summary>
        /// Account classification type.
        /// </summary>
        [JsonIgnore]
        public override string AccountType => "Omni";

        /// <summary>
        /// Initializes a new instance of the OmniAccount class.
        /// </summary>
        [JsonConstructor]
        public OmniAccount(string accountNumber, decimal balance, decimal interestRate, decimal overdraftLimit, Customer? owner = null, decimal failedFee = 10.00m)
            : base(accountNumber, balance, owner)
        {
            this.interestRate = interestRate;
            this.overdraftLimit = overdraftLimit;
            this.failedFee = failedFee;
        }

        /// <summary>
        /// Debits funds up to (balance + overdraftLimit).
        /// Breaches beyond limit incur a failed fee ($10 regular, $5 staff) and throw InsufficientFundsException.
        /// </summary>
        public override bool Withdraw(decimal amount)
        {
            if (amount <= 0)
            {
                lastTransactionStatus = "Withdrawal Failed: Amount must be positive.";
                throw new BankingException("Withdrawal failed: Amount must be positive.", this, amount);
            }

            if (amount <= (balance + overdraftLimit))
            {
                balance -= amount;
                lastTransactionStatus = $"Withdrawal Successful: ${amount:F2}";
                return true;
            }
            else
            {
                decimal fee = GetFee(failedFee);
                balance -= fee;
                lastTransactionStatus = $"Withdrawal Failed: Overdraft exceeded. Fee charged: ${fee:F2}";
                throw new InsufficientFundsException(
                    $"Omni Account withdrawal failed: Overdraft limit exceeded. Requested ${amount:F2}, " +
                    $"but maximum available with overdraft limit of ${overdraftLimit:F2} was ${(balance + overdraftLimit + fee):F2}. " +
                    $"A failed transaction fee of ${fee:F2} has been charged. Remaining balance is ${balance:F2}.",
                    this, amount);
            }
        }

        /// <summary>
        /// Applies interest only on the portion of the balance exceeding $1,000.00.
        /// </summary>
        public override decimal CalculateInterest()
        {
            if (balance > 1000m)
            {
                decimal interest = Math.Round((balance - 1000m) * (interestRate / 100m), 2);
                balance += interest;
                lastTransactionStatus = $"Interest added: ${interest:F2}";
                return interest;
            }
            else
            {
                lastTransactionStatus = "No interest: Balance under $1000";
                return 0m;
            }
        }
    }
}
