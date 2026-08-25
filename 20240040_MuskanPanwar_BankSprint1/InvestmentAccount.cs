namespace _20240040_MuskanPanwar_BankSprint1
{
    // Investment Account: Variable interest, fee for failed transactions, no overdraft
    public class InvestmentAccount : Account
    {
        private decimal interestRate;
        private decimal failedFee = 10.00m; // Fixed fee for failed transaction

        public decimal InterestRate
        {
            get => interestRate;
            set => interestRate = value;
        }

        public decimal FailedFee
        {
            get => failedFee;
            set => failedFee = value;
        }

        // Constructor using base keyword
        public InvestmentAccount(string accountNumber, decimal balance, decimal interestRate, Customer owner)
            : base(accountNumber, balance, owner)
        {
            this.interestRate = interestRate;
        }

        // Withdrawal method (charges fee on insufficient funds)
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
                // Charge fee with 50% discount for bank staff
                decimal fee = GetFee(failedFee);
                balance -= fee;
                lastTransactionStatus = "Withdrawal Failed: Insufficient Funds. Fee charged: $" + fee;
                return false;
            }
        }

        // Calculate interest on total balance
        public override decimal CalculateInterest()
        {
            decimal interest = balance * (interestRate / 100);
            balance += interest;
            lastTransactionStatus = "Interest added: $" + interest;
            return interest;
        }
    }
}
