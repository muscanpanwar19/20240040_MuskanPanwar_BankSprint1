using System;

namespace _20240040_MuskanPanwar_BankSprint1
{
    // Everyday Account: No interest, no overdraft, no fees
    public class EverydayAccount : Account
    {
        // Constructor using base keyword
        public EverydayAccount(string accountNumber, decimal balance, Customer owner)
            : base(accountNumber, balance, owner)
        {
        }

        // Withdrawal method (no overdraft, no fees on failure)
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
                lastTransactionStatus = "Withdrawal Successful: $" + amount.ToString("F2");
                return true;
            }
            else
            {
                lastTransactionStatus = "Withdrawal Failed: Insufficient Funds";
                throw new InsufficientFundsException(
                    "Everyday Account withdrawal failed: Insufficient funds. Requested $" + 
                    amount.ToString("F2") + ", but available balance is $" + balance.ToString("F2") + 
                    ". Overdraft is not permitted on Everyday Accounts.", this, amount);
            }
        }

        // No interest for everyday accounts
        public override decimal CalculateInterest()
        {
            lastTransactionStatus = "No interest on Everyday Account.";
            return 0;
        }
    }
}
