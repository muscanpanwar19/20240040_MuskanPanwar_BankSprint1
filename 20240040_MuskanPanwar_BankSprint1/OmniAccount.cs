namespace _20240040_MuskanPanwar_BankSprint1
{
    public class OmniAccount : Account
    {
        private decimal interestRate;
        private decimal overdraftLimit;
        private decimal failedFee = 10.00m; 

        public decimal InterestRate
        {
            get => interestRate;
            set => interestRate = value;
        }

        public decimal OverdraftLimit
        {
            get => overdraftLimit;
            set => overdraftLimit = value;
        }

        public decimal FailedFee
        {
            get => failedFee;
            set => failedFee = value;
        }

        // Constructor using base keyword
        public OmniAccount(string accountNumber, decimal balance, decimal interestRate, decimal overdraftLimit, Customer owner)
            : base(accountNumber, balance, owner)
        {
            this.interestRate = interestRate;
            this.overdraftLimit = overdraftLimit;
        }

        // Withdrawal method (allows overdraft up to limit)
        public override bool Withdraw(decimal amount)
        {
            if (amount <= (balance + overdraftLimit))
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
                lastTransactionStatus = "Withdrawal Failed: Overdraft exceeded. Fee charged: $" + fee;
                return false;
            }
        }

        public override decimal CalculateInterest()
        {
            if (balance > 1000)
            {
                decimal interest = (balance - 1000) * (interestRate / 100);
                balance += interest;
                lastTransactionStatus = "Interest added: $" + interest;
                return interest;
            }
            else
            {
                lastTransactionStatus = "No interest: Balance under $1000";
                return 0;
            }
        }
    }
}
