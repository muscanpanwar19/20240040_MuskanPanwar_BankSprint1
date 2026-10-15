using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using _20240040_MuskanPanwar_BankSprint1;

namespace BankSystem.Tests
{
    [TestClass]
    public class CustomerControllerTests
    {
        private CustomerController _controller;

        [TestInitialize]
        public void Setup()
        {
            _controller = new CustomerController();
        }

        // ==========================================
        // INITIALIZATION & READ TESTS
        // ==========================================

        [TestMethod]
        public void Controller_InitializesWithDefaultCustomers()
        {
            // Controller seeds Muskan Panwar and Rakshika (Staff)
            Assert.AreEqual(2, _controller.CustomerCount);
            IReadOnlyList<Customer> customers = _controller.GetAllCustomers();
            Assert.IsNotNull(customers);
            Assert.AreEqual(2, customers.Count);
        }

        [TestMethod]
        public void GetCustomerByNumber_ExistingCustomer_ReturnsCustomer()
        {
            Customer customer = _controller.GetCustomerByNumber("20240040");
            Assert.IsNotNull(customer);
            Assert.AreEqual("Muskan Panwar", customer.Name);
            Assert.IsFalse(customer.IsStaff);
        }

        [TestMethod]
        public void GetCustomerByNumber_NonExistentCustomer_ReturnsNull()
        {
            Customer customer = _controller.GetCustomerByNumber("INVALID999");
            Assert.IsNull(customer);
        }

        // ==========================================
        // CRUD: CREATE TESTS
        // ==========================================

        [TestMethod]
        public void CreateCustomer_ValidData_AddsCustomerAndPrepopulatesAccounts()
        {
            int initialCount = _controller.CustomerCount;

            Customer created = _controller.CreateCustomer("NEW001", "Alice Smith", "alice@email.com", false);

            Assert.IsNotNull(created);
            Assert.AreEqual("NEW001", created.CustomerNumber);
            Assert.AreEqual("Alice Smith", created.Name);
            Assert.AreEqual(initialCount + 1, _controller.CustomerCount);
            // Verify accounts pre-populated
            Assert.AreEqual(3, created.Accounts.Count);
            Assert.IsInstanceOfType(created.Accounts[0], typeof(EverydayAccount));
            Assert.IsInstanceOfType(created.Accounts[1], typeof(InvestmentAccount));
            Assert.IsInstanceOfType(created.Accounts[2], typeof(OmniAccount));
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void CreateCustomer_DuplicateCustomerNumber_ThrowsInvalidOperationException()
        {
            // ID '20240040' already exists
            _controller.CreateCustomer("20240040", "Duplicate User", "dup@email.com", false);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void CreateCustomer_EmptyId_ThrowsArgumentException()
        {
            _controller.CreateCustomer("", "Valid Name", "contact@email.com", false);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void CreateCustomer_EmptyName_ThrowsArgumentException()
        {
            _controller.CreateCustomer("VALID01", "   ", "contact@email.com", false);
        }

        // ==========================================
        // CRUD: UPDATE TESTS
        // ==========================================

        [TestMethod]
        public void UpdateCustomer_ExistingCustomer_ModifiesDetailsSuccessfully()
        {
            bool updated = _controller.UpdateCustomer("20240040", "Muskan Panwar-Updated", "new.email@bank.com", true);

            Assert.IsTrue(updated);
            Customer customer = _controller.GetCustomerByNumber("20240040");
            Assert.AreEqual("Muskan Panwar-Updated", customer.Name);
            Assert.AreEqual("new.email@bank.com", customer.ContactDetails);
            Assert.IsTrue(customer.IsStaff);
        }

        [TestMethod]
        [ExpectedException(typeof(KeyNotFoundException))]
        public void UpdateCustomer_NonExistentCustomer_ThrowsKeyNotFoundException()
        {
            _controller.UpdateCustomer("NONEXISTENT", "New Name", "email@bank.com", false);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void UpdateCustomer_EmptyName_ThrowsArgumentException()
        {
            _controller.UpdateCustomer("20240040", "", "email@bank.com", false);
        }

        // ==========================================
        // CRUD: DELETE TESTS
        // ==========================================

        [TestMethod]
        public void DeleteCustomer_ExistingCustomer_RemovesFromCollectionAndReturnsTrue()
        {
            int initialCount = _controller.CustomerCount;

            bool deleted = _controller.DeleteCustomer("STAFF01");

            Assert.IsTrue(deleted);
            Assert.AreEqual(initialCount - 1, _controller.CustomerCount);
            Assert.IsNull(_controller.GetCustomerByNumber("STAFF01"));
        }

        [TestMethod]
        public void DeleteCustomer_NonExistentCustomer_ReturnsFalse()
        {
            bool deleted = _controller.DeleteCustomer("GHOST_ID");

            Assert.IsFalse(deleted);
        }

        // ==========================================
        // TRANSACTION ROUTING VIA CONTROLLER
        // ==========================================

        [TestMethod]
        public void Controller_Deposit_UpdatesAccountCorrectly()
        {
            Customer customer = _controller.GetCustomerByNumber("20240040");
            Account acc = customer.Accounts[0]; // Everyday account, initial balance $500
            decimal initialBalance = acc.Balance;

            bool success = _controller.Deposit(acc, 150.00m);

            Assert.IsTrue(success);
            Assert.AreEqual(initialBalance + 150.00m, acc.Balance);
        }

        [TestMethod]
        public void Controller_Withdraw_UpdatesAccountCorrectly()
        {
            Customer customer = _controller.GetCustomerByNumber("20240040");
            Account acc = customer.Accounts[0];
            decimal initialBalance = acc.Balance;

            bool success = _controller.Withdraw(acc, 100.00m);

            Assert.IsTrue(success);
            Assert.AreEqual(initialBalance - 100.00m, acc.Balance);
        }

        [TestMethod]
        [ExpectedException(typeof(InsufficientFundsException))]
        public void Controller_Withdraw_PropagatesInsufficientFundsExceptionWhenOverdrawn()
        {
            Customer customer = _controller.GetCustomerByNumber("20240040");
            Account acc = customer.Accounts[0]; // Everyday Account ($500 balance, no overdraft)

            _controller.Withdraw(acc, 99999.00m);
        }
    }
}
