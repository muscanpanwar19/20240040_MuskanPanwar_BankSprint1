using System;

namespace _20240040_MuskanPanwar_BankSprint1
{
    // Abstract base class for all bank accounts
    public abstract class Account
    {
        // Protected fields accessible in child classes
        protected string accountNumber;
        protected decimal balance;
        protected string lastTransactionStatus;
        protected Customer owner;

        // Public properties
        public string AccountNumber
        {
            get { return accountNumber; }
        }

        public decimal Balance
        {
            get { return balance; }
        }

        public string LastTransactionStatus
        {
            get { return lastTransactionStatus; }
        }

        public Customer Owner
        {
            get { return owner; }
        }

        // Constructor
        public Account(string accountNumber, decimal balance, Customer owner)
        {
            this.accountNumber = accountNumber;
            this.balance = balance;
            this.owner = owner;
            this.lastTransactionStatus = "Account opened.";
        }

        // Deposit method
        public virtual bool Deposit(decimal amount)
        {
            if (amount <= 0)
            {
                lastTransactionStatus = "Deposit Failed: Amount must be positive.";
                return false;
            }

            balance = balance + amount;
            lastTransactionStatus = "Deposit Successful: $" + amount;
            return true;
        }

       
        public abstract bool Withdraw(decimal amount);
        public abstract decimal CalculateInterest();

        // Helper method: Bank staff gets a 50% discount on transaction fees
        protected decimal GetFee(decimal fee)
        {
            if (owner != null && owner.IsStaff)
            {
                return fee * 0.5m; // 50% discount for bank staff
            }
            return fee;
        }

        
        public virtual string GetAccountInfo()
        {
            return "Account: " + accountNumber + " | Balance: $" + balance + " | Status: " + lastTransactionStatus;
        }
    }
}
