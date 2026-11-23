using System;
using System.Text.Json.Serialization;

namespace _20240040_MuskanPanwar_BankSprint1
{
    /// <summary>
    /// Represents an Investment deposit account.
    /// Characteristics: Earns variable compound interest, charges a failed transaction fee ($10 or $5 staff), no overdraft.
    /// </summary>
    public class InvestmentAccount : Account
    {
        private decimal interestRate;
        private decimal failedFee = 10.00m;

        /// <summary>
        /// Annualized interest rate percentage (e.g., 4.0 for 4.0%).
        /// </summary>
        public decimal InterestRate
        {
            get => interestRate;
            set => interestRate = value;
        }

        /// <summary>
        /// Standard baseline fee charged when a transaction fails due to insufficient funds ($10.00).
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
        public override string AccountType => "Investment";

        /// <summary>
        /// Initializes a new instance of the InvestmentAccount class.
        /// </summary>
        [JsonConstructor]
        public InvestmentAccount(string accountNumber, decimal balance, decimal interestRate, Customer? owner = null, decimal failedFee = 10.00m)
            : base(accountNumber, balance, owner)
        {
            this.interestRate = interestRate;
            this.failedFee = failedFee;
        }

        /// <summary>
        /// Debits funds from the Investment account.
        /// If requested amount exceeds balance, a failed fee is applied ($10 regular, $5 staff) and an exception is thrown.
        /// </summary>
        public override bool Withdraw(decimal amount)
        {
            if (amount <= 0)
            {
                lastTransactionStatus = "Withdrawal Failed: Amount must be positive.";
                throw new BankingException("Withdrawal failed: Amount must be positive.", this, amount);
            }

            if (amount <= balance)
            {
                balance -= amount;
                lastTransactionStatus = $"Withdrawal Successful: ${amount:F2}";
                return true;
            }
            else
            {
                decimal fee = GetFee(failedFee);
                balance -= fee;
                lastTransactionStatus = $"Withdrawal Failed: Insufficient Funds. Fee charged: ${fee:F2}";
                throw new InsufficientFundsException(
                    $"Investment Account withdrawal failed: Insufficient funds. Requested ${amount:F2}, " +
                    $"but available balance was ${(balance + fee):F2}. A failed transaction fee of ${fee:F2} " +
                    $"has been charged. Remaining balance is ${balance:F2}.", this, amount);
            }
        }

        /// <summary>
        /// Calculates and credits variable interest onto the entire positive balance.
        /// </summary>
        public override decimal CalculateInterest()
        {
            if (balance > 0)
            {
                decimal interest = Math.Round(balance * (interestRate / 100m), 2);
                balance += interest;
                lastTransactionStatus = $"Interest added: ${interest:F2}";
                return interest;
            }
            else
            {
                lastTransactionStatus = "No interest added: Balance is not positive.";
                return 0m;
            }
        }
    }
}
