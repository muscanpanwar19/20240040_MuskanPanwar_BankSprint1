using System;
using System.Collections.Generic;

namespace _20240040_MuskanPanwar_BankSprint1
{
    /// <summary>
    /// Controller in the MVC architecture that decouples customer and account data management
    /// from the User Interface (View). Handles all business rules, validations, and CRUD operations.
    /// </summary>
    public class CustomerController
    {
        // Internal data store of customers managed exclusively by the controller
        private readonly List<Customer> _customers;

        /// <summary>
        /// Total number of customers currently managed.
        /// </summary>
        public int CustomerCount => _customers.Count;

        /// <summary>
        /// Initializes the CustomerController with default customers from Sprint 1.
        /// </summary>
        public CustomerController()
        {
            _customers = new List<Customer>();
            InitializeDefaultCustomers();
        }

        /// <summary>
        /// Seeds initial customer records (Muskan Panwar and Bank Staff member).
        /// </summary>
        private void InitializeDefaultCustomers()
        {
            Customer muskan = new Customer("20240040", "Muskan Panwar", "muskan.panwar@email.com", false);
            muskan.PrePopulateAccounts();
            _customers.Add(muskan);

            Customer staffMember = new Customer("STAFF01", "Rakshika", "rakshika@bank.com", true);
            staffMember.PrePopulateAccounts();
            _customers.Add(staffMember);
        }

        // =========================================================================
        // CRUD OPERATION: CREATE
        // =========================================================================

        /// <summary>
        /// Creates a new customer, pre-populates their standard accounts, and adds them to the system.
        /// </summary>
        /// <param name="customerNumber">Unique customer identifier.</param>
        /// <param name="name">Full name of the customer.</param>
        /// <param name="contactDetails">Email or phone contact information.</param>
        /// <param name="isStaff">True if customer is a bank staff member; otherwise false.</param>
        /// <returns>The newly created Customer object.</returns>
        public Customer CreateCustomer(string customerNumber, string name, string contactDetails, bool isStaff)
        {
            if (string.IsNullOrWhiteSpace(customerNumber))
            {
                throw new ArgumentException("Customer number cannot be empty.", nameof(customerNumber));
            }

            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException("Customer name cannot be empty.", nameof(name));
            }

            if (GetCustomerByNumber(customerNumber) != null)
            {
                throw new InvalidOperationException($"A customer with ID '{customerNumber.Trim()}' already exists.");
            }

            Customer newCustomer = new Customer(customerNumber.Trim(), name.Trim(), contactDetails?.Trim() ?? string.Empty, isStaff);
            newCustomer.PrePopulateAccounts();
            _customers.Add(newCustomer);
            return newCustomer;
        }

        // =========================================================================
        // CRUD OPERATION: READ
        // =========================================================================

        /// <summary>
        /// Retrieves a read-only collection of all customers.
        /// Prevents Views from directly modifying the underlying list.
        /// </summary>
        /// <returns>Read-only list of Customer objects.</returns>
        public IReadOnlyList<Customer> GetAllCustomers()
        {
            return _customers.AsReadOnly();
        }

        /// <summary>
        /// Finds and returns a customer by their unique customer number.
        /// </summary>
        /// <param name="customerNumber">The customer ID to search for.</param>
        /// <returns>The Customer if found, or null.</returns>
        public Customer GetCustomerByNumber(string customerNumber)
        {
            if (string.IsNullOrWhiteSpace(customerNumber)) return null;

            return _customers.Find(c => string.Equals(c.CustomerNumber, customerNumber.Trim(), StringComparison.OrdinalIgnoreCase));
        }

        // =========================================================================
        // CRUD OPERATION: UPDATE
        // =========================================================================

        /// <summary>
        /// Updates the personal details and staff status of an existing customer.
        /// </summary>
        /// <param name="customerNumber">The customer number of the record to update.</param>
        /// <param name="newName">Updated full name.</param>
        /// <param name="newContactDetails">Updated contact information.</param>
        /// <param name="isStaff">Updated staff status.</param>
        /// <returns>True if update was successful.</returns>
        public bool UpdateCustomer(string customerNumber, string newName, string newContactDetails, bool isStaff)
        {
            Customer customer = GetCustomerByNumber(customerNumber);
            if (customer == null)
            {
                throw new KeyNotFoundException($"Customer with ID '{customerNumber}' was not found.");
            }

            if (string.IsNullOrWhiteSpace(newName))
            {
                throw new ArgumentException("Customer name cannot be empty.", nameof(newName));
            }

            customer.Name = newName.Trim();
            customer.ContactDetails = newContactDetails?.Trim() ?? string.Empty;
            customer.IsStaff = isStaff;
            return true;
        }

        // =========================================================================
        // CRUD OPERATION: DELETE
        // =========================================================================

        /// <summary>
        /// Removes a customer and their associated accounts from the system.
        /// </summary>
        /// <param name="customerNumber">The customer number of the record to remove.</param>
        /// <returns>True if customer was found and removed; otherwise false.</returns>
        public bool DeleteCustomer(string customerNumber)
        {
            Customer customer = GetCustomerByNumber(customerNumber);
            if (customer == null)
            {
                return false;
            }

            return _customers.Remove(customer);
        }

        // =========================================================================
        // BANKING TRANSACTIONS (Coordinated by Controller)
        // =========================================================================

        /// <summary>
        /// Processes a deposit onto a customer's account.
        /// </summary>
        public bool Deposit(Account account, decimal amount)
        {
            if (account == null) throw new ArgumentNullException(nameof(account));
            return account.Deposit(amount);
        }

        /// <summary>
        /// Processes a withdrawal from a customer's account, enforcing rules and exceptions.
        /// </summary>
        public bool Withdraw(Account account, decimal amount)
        {
            if (account == null) throw new ArgumentNullException(nameof(account));
            return account.Withdraw(amount);
        }

        /// <summary>
        /// Calculates and applies interest to a customer's account.
        /// </summary>
        public decimal CalculateInterest(Account account)
        {
            if (account == null) throw new ArgumentNullException(nameof(account));
            return account.CalculateInterest();
        }
    }
}
