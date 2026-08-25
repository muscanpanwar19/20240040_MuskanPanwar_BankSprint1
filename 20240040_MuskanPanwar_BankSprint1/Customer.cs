using System;
using System.Collections.Generic;

namespace _20240040_MuskanPanwar_BankSprint1
{
    public class Customer
    {
        // Private fields
        private string customerNumber;
        private string name;
        private string contactDetails;
        private bool isStaff;
        private List<Account> accounts;

        // Public properties
        public string CustomerNumber
        {
            get { return customerNumber; }
            set { customerNumber = value; }
        }

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public string ContactDetails
        {
            get { return contactDetails; }
            set { contactDetails = value; }
        }

        public bool IsStaff
        {
            get { return isStaff; }
            set { isStaff = value; }
        }

        public List<Account> Accounts
        {
            get { return accounts; }
        }

        // Constructor
        public Customer(string customerNumber, string name, string contactDetails, bool isStaff)
        {
            this.customerNumber = customerNumber;
            this.name = name;
            this.contactDetails = contactDetails;
            this.isStaff = isStaff;
            this.accounts = new List<Account>();
        }

        // Add account to customer
        public void AddAccount(Account account)
        {
            accounts.Add(account);
        }

        // Pre-populate with one of each account type
        public void PrePopulateAccounts()
        {
            accounts.Clear();
            accounts.Add(new EverydayAccount("ED-" + customerNumber, 500.00m, this));
            accounts.Add(new InvestmentAccount("INV-" + customerNumber, 2000.00m, 4.0m, this));
            accounts.Add(new OmniAccount("OMN-" + customerNumber, 1500.00m, 5.0m, 500.00m, this));
        }

        // Customer details string
        public string GetCustomerDetails()
        {
            string role = isStaff ? "Bank Staff (50% Fee Discount)" : "Regular Customer";
            return "Customer: " + customerNumber + " | Name: " + name + " | Role: " + role + " | Contact: " + contactDetails;
        }
    }
}
