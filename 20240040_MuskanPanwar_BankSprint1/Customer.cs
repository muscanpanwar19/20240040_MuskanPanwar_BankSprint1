using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace _20240040_MuskanPanwar_BankSprint1
{
    /// <summary>
    /// Represents a Bank Customer holding a 1:N relationship with multiple accounts.
    /// Supports polymorphic JSON serialization and tracks Staff privilege status.
    /// </summary>
    public class Customer
    {
        private string customerNumber;
        private string name;
        private string contactDetails;
        private bool isStaff;
        private List<Account> accounts;

        /// <summary>
        /// Unique customer identifier code (e.g., "20240040", "STAFF01").
        /// </summary>
        public string CustomerNumber
        {
            get => customerNumber;
            set => customerNumber = value;
        }

        /// <summary>
        /// Full legal name of the customer.
        /// </summary>
        public string Name
        {
            get => name;
            set => name = value;
        }

        /// <summary>
        /// Contact details (email address or phone number).
        /// </summary>
        public string ContactDetails
        {
            get => contactDetails;
            set => contactDetails = value;
        }

        /// <summary>
        /// Distinguishes Bank Staff (50% fee discount) from Regular Customers.
        /// </summary>
        public bool IsStaff
        {
            get => isStaff;
            set => isStaff = value;
        }

        /// <summary>
        /// Enumerated representation of customer role (Regular or Staff).
        /// </summary>
        [JsonIgnore]
        public CustomerType Role => isStaff ? CustomerType.Staff : CustomerType.Regular;

        /// <summary>
        /// List of accounts associated with this customer (1:N relationship).
        /// </summary>
        public List<Account> Accounts
        {
            get => accounts;
            set => accounts = value ?? new List<Account>();
        }

        /// <summary>
        /// Primary constructor for Customer, configured for JSON deserialization.
        /// </summary>
        [JsonConstructor]
        public Customer(string customerNumber, string name, string contactDetails, bool isStaff, List<Account>? accounts = null)
        {
            this.customerNumber = customerNumber ?? string.Empty;
            this.name = name ?? string.Empty;
            this.contactDetails = contactDetails ?? string.Empty;
            this.isStaff = isStaff;
            this.accounts = accounts ?? new List<Account>();

            // Ensure back-references are restored
            foreach (var account in this.accounts)
            {
                account.SetOwner(this);
            }
        }

        /// <summary>
        /// Adds an account to the customer's account collection and sets ownership back-reference.
        /// </summary>
        /// <param name="account">The account to associate with this customer.</param>
        public void AddAccount(Account account)
        {
            if (account == null) throw new ArgumentNullException(nameof(account));
            account.SetOwner(this);
            accounts.Add(account);
        }

        /// <summary>
        /// Finds an account belonging to this customer by account number.
        /// </summary>
        public Account? GetAccount(string accountNumber)
        {
            if (string.IsNullOrWhiteSpace(accountNumber)) return null;
            return accounts.Find(a => string.Equals(a.AccountNumber, accountNumber.Trim(), StringComparison.OrdinalIgnoreCase));
        }

        /// <summary>
        /// Removes an account belonging to this customer.
        /// </summary>
        public bool RemoveAccount(string accountNumber)
        {
            var acc = GetAccount(accountNumber);
            if (acc == null) return false;
            return accounts.Remove(acc);
        }

        /// <summary>
        /// Generates standard default accounts for testing / onboarding.
        /// </summary>
        public void PrePopulateAccounts()
        {
            accounts.Clear();
            accounts.Add(new EverydayAccount($"ED-{customerNumber}", 500.00m, this));
            accounts.Add(new InvestmentAccount($"INV-{customerNumber}", 2000.00m, 4.0m, this));
            accounts.Add(new OmniAccount($"OMN-{customerNumber}", 1500.00m, 5.0m, 500.00m, this));
        }

        /// <summary>
        /// Returns customer summary profile.
        /// </summary>
        public string GetCustomerDetails()
        {
            string roleDesc = isStaff ? "Bank Staff (50% Fee Discount)" : "Regular Customer";
            return $"Customer: {customerNumber} | Name: {name} | Role: {roleDesc} | Contact: {contactDetails} | Accounts: {accounts.Count}";
        }
    }
}
