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
            if (amount <= balance)
            {
                balance -= amount;
                lastTransactionStatus = "Withdrawal Successful: $" + amount;
                return true;
            }
            else
            {
                lastTransactionStatus = "Withdrawal Failed: Insufficient Funds";
                return false;
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
