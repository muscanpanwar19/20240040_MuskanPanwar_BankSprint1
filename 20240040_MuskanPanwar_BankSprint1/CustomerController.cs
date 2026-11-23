using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace _20240040_MuskanPanwar_BankSprint1
{
    /// <summary>
    /// Controller in the Model-View-Controller (MVC) architecture that coordinates all business logic,
    /// customer and account collections, intra-account money transfers, and JSON data persistence.
    /// Strictly isolates state management and financial rules from presentation layers.
    /// </summary>
    public class CustomerController
    {
        // Internal data store of customers managed exclusively by the controller
        private readonly List<Customer> _customers;

        // Default storage path for system-wide JSON persistence
        private string _persistenceFilePath = "bank_data.json";

        // JSON options configured for polymorphic serialization and clean formatting
        private static readonly JsonSerializerOptions _jsonOptions = new JsonSerializerOptions
        {
            WriteIndented = true,
            PropertyNameCaseInsensitive = true,
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
        };

        /// <summary>
        /// Total number of customers currently managed by the controller.
        /// </summary>
        public int CustomerCount => _customers.Count;

        /// <summary>
        /// Gets or sets the file path used for JSON state serialization.
        /// </summary>
        public string PersistenceFilePath
        {
            get => _persistenceFilePath;
            set => _persistenceFilePath = string.IsNullOrWhiteSpace(value) ? "bank_data.json" : value;
        }

        /// <summary>
        /// Initializes the CustomerController.
        /// Attempts to restore existing data from the persistence file; otherwise seeds default accounts.
        /// </summary>
        public CustomerController()
        {
            _customers = new List<Customer>();
            InitializeDefaultCustomers();
        }

        /// <summary>
        /// Seeds baseline sample customers (Muskan Panwar and Bank Staff member) for evaluation.
        /// </summary>
        public void InitializeDefaultCustomers()
        {
            _customers.Clear();

            Customer muskan = new Customer("20240040", "Muskan Panwar", "muskan.panwar@email.com", false);
            muskan.PrePopulateAccounts();
            _customers.Add(muskan);

            Customer staffMember = new Customer("STAFF01", "Rakshika", "rakshika@bank.com", true);
            staffMember.PrePopulateAccounts();
            _customers.Add(staffMember);
        }

        // =========================================================================
        // CRUD OPERATIONS: CUSTOMER MANAGEMENT
        // =========================================================================

        /// <summary>
        /// Creates a new customer, pre-populates their standard accounts, and adds them to the system.
        /// </summary>
        /// <param name="customerNumber">Unique alphanumeric customer identifier.</param>
        /// <param name="name">Full legal name of the customer.</param>
        /// <param name="contactDetails">Contact email address or telephone number.</param>
        /// <param name="isStaff">True if customer qualifies for Bank Staff benefits (50% fee discount); otherwise false.</param>
        /// <returns>The newly created and registered Customer instance.</returns>
        /// <exception cref="ArgumentException">Thrown when customer identifier or name is null or whitespace.</exception>
        /// <exception cref="InvalidOperationException">Thrown when a customer with the given ID already exists.</exception>
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

        /// <summary>
        /// Retrieves a read-only snapshot collection of all currently registered customers.
        /// Prevents Views and calling classes from mutating internal collection references directly.
        /// </summary>
        /// <returns>A read-only list of Customer entities.</returns>
        public IReadOnlyList<Customer> GetAllCustomers()
        {
            return _customers.AsReadOnly();
        }

        /// <summary>
        /// Searches for an active customer by their unique customer identification number.
        /// </summary>
        /// <param name="customerNumber">The customer identifier to locate.</param>
        /// <returns>The matching Customer entity, or null if no record matches.</returns>
        public Customer? GetCustomerByNumber(string customerNumber)
        {
            if (string.IsNullOrWhiteSpace(customerNumber)) return null;
            return _customers.Find(c => string.Equals(c.CustomerNumber, customerNumber.Trim(), StringComparison.OrdinalIgnoreCase));
        }

        /// <summary>
        /// Updates the profile information and staff privilege status of an existing customer.
        /// </summary>
        /// <param name="customerNumber">The unique customer number of the target customer record.</param>
        /// <param name="newName">Updated full legal name.</param>
        /// <param name="newContactDetails">Updated contact email or phone number.</param>
        /// <param name="isStaff">Updated staff privilege status.</param>
        /// <returns>True if the customer was successfully located and updated.</returns>
        /// <exception cref="KeyNotFoundException">Thrown when no customer matches customerNumber.</exception>
        /// <exception cref="ArgumentException">Thrown when newName is empty.</exception>
        public bool UpdateCustomer(string customerNumber, string newName, string newContactDetails, bool isStaff)
        {
            Customer? customer = GetCustomerByNumber(customerNumber);
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

        /// <summary>
        /// Removes a customer and all their associated accounts from the system.
        /// </summary>
        /// <param name="customerNumber">The identifier of the customer to delete.</param>
        /// <returns>True if the customer was found and removed; otherwise false.</returns>
        public bool DeleteCustomer(string customerNumber)
        {
            Customer? customer = GetCustomerByNumber(customerNumber);
            if (customer == null)
            {
                return false;
            }

            return _customers.Remove(customer);
        }

        // =========================================================================
        // ARCHITECTURAL EVOLUTION: DYNAMIC ACCOUNT CREATION (TASK 3)
        // =========================================================================

        /// <summary>
        /// Dynamically creates and attaches a new account to an existing customer at runtime.
        /// Supports polymorphic creation of Everyday, Investment, and Omni accounts.
        /// </summary>
        /// <param name="customerNumber">Target customer number.</param>
        /// <param name="accountType">Account type string: "Everyday", "Investment", or "Omni".</param>
        /// <param name="accountNumber">Unique account code.</param>
        /// <param name="initialBalance">Opening deposit balance (must be non-negative).</param>
        /// <param name="interestRate">Interest rate percentage (for Investment or Omni accounts).</param>
        /// <param name="overdraftLimit">Overdraft allowance limit (for Omni accounts).</param>
        /// <returns>The newly created Account instance.</returns>
        /// <exception cref="KeyNotFoundException">Thrown if customer does not exist.</exception>
        /// <exception cref="ArgumentException">Thrown on invalid parameters or duplicate account number.</exception>
        public Account AddAccount(string customerNumber, string accountType, string accountNumber, decimal initialBalance, decimal interestRate = 0m, decimal overdraftLimit = 0m)
        {
            Customer? customer = GetCustomerByNumber(customerNumber);
            if (customer == null)
            {
                throw new KeyNotFoundException($"Customer with ID '{customerNumber}' was not found.");
            }

            if (string.IsNullOrWhiteSpace(accountNumber))
            {
                throw new ArgumentException("Account number cannot be empty.", nameof(accountNumber));
            }

            if (initialBalance < 0m)
            {
                throw new ArgumentException("Initial balance cannot be negative.", nameof(initialBalance));
            }

            // Verify account number uniqueness across all customers in the bank
            bool exists = _customers.Any(c => c.Accounts.Any(a => string.Equals(a.AccountNumber, accountNumber.Trim(), StringComparison.OrdinalIgnoreCase)));
            if (exists)
            {
                throw new ArgumentException($"An account with number '{accountNumber.Trim()}' already exists in the system.");
            }

            Account newAccount;
            string normalizedType = accountType?.Trim().ToLowerInvariant() ?? string.Empty;

            switch (normalizedType)
            {
                case "everyday":
                    newAccount = new EverydayAccount(accountNumber.Trim(), initialBalance, customer);
                    break;

                case "investment":
                    if (interestRate < 0m) throw new ArgumentException("Interest rate cannot be negative.", nameof(interestRate));
                    newAccount = new InvestmentAccount(accountNumber.Trim(), initialBalance, interestRate, customer);
                    break;

                case "omni":
                    if (interestRate < 0m) throw new ArgumentException("Interest rate cannot be negative.", nameof(interestRate));
                    if (overdraftLimit < 0m) throw new ArgumentException("Overdraft limit cannot be negative.", nameof(overdraftLimit));
                    newAccount = new OmniAccount(accountNumber.Trim(), initialBalance, interestRate, overdraftLimit, customer);
                    break;

                default:
                    throw new ArgumentException($"Unsupported account type '{accountType}'. Valid types are Everyday, Investment, or Omni.", nameof(accountType));
            }

            customer.AddAccount(newAccount);
            return newAccount;
        }

        // =========================================================================
        // CORE TRANSACTIONS & INTRA-ACCOUNT MONEY TRANSFERS
        // =========================================================================

        /// <summary>
        /// Credits funds to the specified account.
        /// </summary>
        /// <param name="account">Target account.</param>
        /// <param name="amount">Positive monetary amount.</param>
        /// <returns>True if deposit succeeds.</returns>
        public bool Deposit(Account account, decimal amount)
        {
            if (account == null) throw new ArgumentNullException(nameof(account));
            return account.Deposit(amount);
        }

        /// <summary>
        /// Debits funds from the specified account according to its fee and overdraft rules.
        /// </summary>
        /// <param name="account">Target account.</param>
        /// <param name="amount">Positive monetary amount.</param>
        /// <returns>True if withdrawal succeeds.</returns>
        public bool Withdraw(Account account, decimal amount)
        {
            if (account == null) throw new ArgumentNullException(nameof(account));
            return account.Withdraw(amount);
        }

        /// <summary>
        /// Calculates and credits interest according to the account's formula.
        /// </summary>
        /// <param name="account">Target account.</param>
        /// <returns>The calculated interest amount.</returns>
        public decimal CalculateInterest(Account account)
        {
            if (account == null) throw new ArgumentNullException(nameof(account));
            return account.CalculateInterest();
        }

        /// <summary>
        /// Executes an intra-account money transfer between two accounts owned by the same customer.
        /// Validates ownership, positive amount, non-identical source/destination, and overdraft boundaries.
        /// If insufficient funds occur, account-specific fees ($10 regular, $5 staff) are levied on the source.
        /// </summary>
        /// <param name="customerNumber">The customer identifier who owns both accounts.</param>
        /// <param name="sourceAccountNumber">Account from which funds are debited.</param>
        /// <param name="destinationAccountNumber">Account to which funds are credited.</param>
        /// <param name="amount">Positive amount of money to transfer.</param>
        /// <returns>True if the transfer was successfully completed.</returns>
        /// <exception cref="KeyNotFoundException">Thrown if customer or either account is not found.</exception>
        /// <exception cref="BankingException">Thrown if amount is non-positive or accounts are identical.</exception>
        /// <exception cref="InsufficientFundsException">Thrown when source account overdraft or balance is exceeded.</exception>
        public bool Transfer(string customerNumber, string sourceAccountNumber, string destinationAccountNumber, decimal amount)
        {
            Customer? customer = GetCustomerByNumber(customerNumber);
            if (customer == null)
            {
                throw new KeyNotFoundException($"Customer with ID '{customerNumber}' was not found.");
            }

            Account? sourceAccount = customer.GetAccount(sourceAccountNumber);
            if (sourceAccount == null)
            {
                throw new KeyNotFoundException($"Source account '{sourceAccountNumber}' does not belong to customer '{customerNumber}'.");
            }

            Account? destinationAccount = customer.GetAccount(destinationAccountNumber);
            if (destinationAccount == null)
            {
                throw new KeyNotFoundException($"Destination account '{destinationAccountNumber}' does not belong to customer '{customerNumber}'.");
            }

            return Transfer(customer, sourceAccount, destinationAccount, amount);
        }

        /// <summary>
        /// Executes an intra-account transfer using direct Account object references.
        /// </summary>
        /// <param name="customer">The owner customer.</param>
        /// <param name="sourceAccount">Source account to withdraw from.</param>
        /// <param name="destinationAccount">Destination account to deposit into.</param>
        /// <param name="amount">Amount to transfer.</param>
        /// <returns>True if transfer completed successfully.</returns>
        public bool Transfer(Customer customer, Account sourceAccount, Account destinationAccount, decimal amount)
        {
            if (customer == null) throw new ArgumentNullException(nameof(customer));
            if (sourceAccount == null) throw new ArgumentNullException(nameof(sourceAccount));
            if (destinationAccount == null) throw new ArgumentNullException(nameof(destinationAccount));

            // Intra-account rule: Both accounts must belong to this customer
            if (!customer.Accounts.Contains(sourceAccount) || !customer.Accounts.Contains(destinationAccount))
            {
                throw new BankingException("Security restriction: Transfers are strictly intra-account and must be between accounts owned by the same customer.");
            }

            // Transfer rule: Source and destination accounts cannot be identical
            if (string.Equals(sourceAccount.AccountNumber, destinationAccount.AccountNumber, StringComparison.OrdinalIgnoreCase))
            {
                throw new BankingException("Transfer failed: Source and destination accounts cannot be the same.");
            }

            // Transfer rule: Amount must be strictly positive
            if (amount <= 0m)
            {
                throw new BankingException("Transfer failed: Amount must be positive.", sourceAccount, amount);
            }

            // Execute withdrawal from source account.
            // This triggers Account-specific overdraft checks and levies failed fees if breached.
            bool withdrawn = sourceAccount.Withdraw(amount);

            if (withdrawn)
            {
                // Deposit directly into destination account
                destinationAccount.Deposit(amount);

                // Update audit status descriptions on both accounts
                sourceAccount.LastTransactionStatus = $"Transfer Out Successful: ${amount:F2} to {destinationAccount.AccountNumber}";
                destinationAccount.LastTransactionStatus = $"Transfer In Successful: ${amount:F2} from {sourceAccount.AccountNumber}";
                return true;
            }

            return false;
        }

        // =========================================================================
        // DATA PERSISTENCE: JSON SERIALIZATION (TASK 5)
        // =========================================================================

        /// <summary>
        /// Serializes the entire bank customer and account state into a JSON formatted string.
        /// Uses polymorphic [JsonDerivedType] metadata to safely retain Everyday, Investment, and Omni types.
        /// </summary>
        /// <returns>Indented JSON representation of the bank state.</returns>
        public string ExportToJson()
        {
            return JsonSerializer.Serialize(_customers, _jsonOptions);
        }

        /// <summary>
        /// Deserializes a JSON string into customer and account models, restoring parent references.
        /// </summary>
        /// <param name="json">Valid JSON formatted string.</param>
        /// <exception cref="InvalidOperationException">Thrown if deserialization produces invalid data.</exception>
        public void ImportFromJson(string json)
        {
            if (string.IsNullOrWhiteSpace(json))
            {
                throw new ArgumentException("JSON content cannot be empty.", nameof(json));
            }

            var loaded = JsonSerializer.Deserialize<List<Customer>>(json, _jsonOptions);
            if (loaded == null)
            {
                throw new InvalidOperationException("Failed to deserialize JSON data into customer records.");
            }

            _customers.Clear();
            foreach (var customer in loaded)
            {
                // Ensure bidirectional references between Account.Owner and Customer are reconstructed
                foreach (var account in customer.Accounts)
                {
                    account.SetOwner(customer);
                }
                _customers.Add(customer);
            }
        }

        /// <summary>
        /// Saves all current customer records and accounts to the designated local JSON file.
        /// Automatically called during application exit (FormClosing).
        /// </summary>
        /// <param name="filePath">Target destination path; defaults to PersistenceFilePath ("bank_data.json").</param>
        public void SaveToFile(string? filePath = null)
        {
            string targetPath = string.IsNullOrWhiteSpace(filePath) ? _persistenceFilePath : filePath;
            string json = ExportToJson();
            File.WriteAllText(targetPath, json);
        }

        /// <summary>
        /// Loads customer records and polymorphic accounts from the local JSON file.
        /// Automatically called during application startup (FormLoad).
        /// </summary>
        /// <param name="filePath">Target source path; defaults to PersistenceFilePath ("bank_data.json").</param>
        /// <returns>True if file was loaded successfully; false if file does not exist.</returns>
        public bool LoadFromFile(string? filePath = null)
        {
            string targetPath = string.IsNullOrWhiteSpace(filePath) ? _persistenceFilePath : filePath;
            if (!File.Exists(targetPath))
            {
                return false;
            }

            string json = File.ReadAllText(targetPath);
            ImportFromJson(json);
            return true;
        }
    }
}
