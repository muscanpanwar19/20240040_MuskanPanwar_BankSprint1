using System;
using System.Text.Json.Serialization;

namespace _20240040_MuskanPanwar_BankSprint1
{
    /// <summary>
    /// Represents an Everyday transactional account.
    /// Characteristics: Zero interest, no overdraft facility, no failed transaction fees.
    /// </summary>
    public class EverydayAccount : Account
    {
        /// <summary>
        /// Account classification type.
        /// </summary>
        [JsonIgnore]
        public override string AccountType => "Everyday";

        /// <summary>
        /// Initializes a new instance of the EverydayAccount class.
        /// </summary>
        /// <param name="accountNumber">Unique identifier for this account.</param>
        /// <param name="balance">Opening balance.</param>
        /// <param name="owner">Optional customer owner reference.</param>
        [JsonConstructor]
        public EverydayAccount(string accountNumber, decimal balance, Customer? owner = null)
            : base(accountNumber, balance, owner)
        {
        }

        /// <summary>
        /// Debits money from the Everyday account if sufficient funds exist.
        /// Overdraft is strictly disallowed and triggers an InsufficientFundsException.
        /// </summary>
        /// <param name="amount">Amount to withdraw.</param>
        /// <returns>True upon successful withdrawal.</returns>
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
                lastTransactionStatus = "Withdrawal Failed: Insufficient Funds";
                throw new InsufficientFundsException(
                    $"Everyday Account withdrawal failed: Insufficient funds. Requested ${amount:F2}, " +
                    $"but available balance is ${balance:F2}. Overdraft is not permitted on Everyday Accounts.",
                    this, amount);
            }
        }

        /// <summary>
        /// Everyday accounts accrue 0% interest by institutional policy.
        /// </summary>
        /// <returns>Always returns 0.00m.</returns>
        public override decimal CalculateInterest()
        {
            lastTransactionStatus = "No interest on Everyday Account.";
            return 0m;
        }
    }
}
