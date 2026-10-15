using System;

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
            if (amount <= 0)
            {
                lastTransactionStatus = "Withdrawal Failed: Amount must be positive.";
                throw new BankingException("Withdrawal failed: Amount must be positive.", this, amount);
            }

            if (amount <= (balance + overdraftLimit))
            {
                balance -= amount;
                lastTransactionStatus = "Withdrawal Successful: $" + amount.ToString("F2");
                return true;
            }
            else
            {
                // Charge fee with 50% discount for bank staff
                decimal fee = GetFee(failedFee);
                balance -= fee;
                lastTransactionStatus = "Withdrawal Failed: Overdraft exceeded. Fee charged: $" + fee.ToString("F2");
                throw new InsufficientFundsException(
                    "Omni Account withdrawal failed: Overdraft limit exceeded. Requested $" + 
                    amount.ToString("F2") + ", but maximum available with overdraft limit of $" + 
                    overdraftLimit.ToString("F2") + " was $" + (balance + overdraftLimit + fee).ToString("F2") + 
                    ". A failed transaction fee of $" + fee.ToString("F2") + 
                    " has been charged. Remaining balance is $" + balance.ToString("F2") + ".", this, amount);
            }
        }

        public override decimal CalculateInterest()
        {
            if (balance > 1000)
            {
                decimal interest = Math.Round((balance - 1000) * (interestRate / 100), 2);
                balance += interest;
                lastTransactionStatus = "Interest added: $" + interest.ToString("F2");
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
