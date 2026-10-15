using System;

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
                // Charge fee with 50% discount for bank staff
                decimal fee = GetFee(failedFee);
                balance -= fee;
                lastTransactionStatus = "Withdrawal Failed: Insufficient Funds. Fee charged: $" + fee.ToString("F2");
                throw new InsufficientFundsException(
                    "Investment Account withdrawal failed: Insufficient funds. Requested $" + 
                    amount.ToString("F2") + ", but available balance was $" + (balance + fee).ToString("F2") + 
                    ". A failed transaction fee of $" + fee.ToString("F2") + 
                    " has been charged. Remaining balance is $" + balance.ToString("F2") + ".", this, amount);
            }
        }

        // Calculate interest on total balance
        public override decimal CalculateInterest()
        {
            if (balance > 0)
            {
                decimal interest = Math.Round(balance * (interestRate / 100), 2);
                balance += interest;
                lastTransactionStatus = "Interest added: $" + interest.ToString("F2");
                return interest;
            }
            else
            {
                lastTransactionStatus = "No interest added: Balance is not positive.";
                return 0;
            }
        }
    }
}
