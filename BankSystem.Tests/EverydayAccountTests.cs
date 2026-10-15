using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using _20240040_MuskanPanwar_BankSprint1;

namespace BankSystem.Tests
{
    [TestClass]
    public class EverydayAccountTests
    {
        private Customer _customer;
        private EverydayAccount _account;

        [TestInitialize]
        public void Setup()
        {
            _customer = new Customer("CUST01", "Test Customer", "test@email.com", false);
            _account = new EverydayAccount("ED-TEST01", 500.00m, _customer);
        }

        // ==========================================
        // DEPOSIT TESTS
        // ==========================================

        [TestMethod]
        public void Deposit_PositiveAmount_IncreasesBalanceAndSetsSuccessStatus()
        {
            // Arrange
            decimal depositAmount = 200.00m;

            // Act
            bool result = _account.Deposit(depositAmount);

            // Assert
            Assert.IsTrue(result);
            Assert.AreEqual(700.00m, _account.Balance);
            StringAssert.Contains(_account.LastTransactionStatus, "Deposit Successful");
        }

        [TestMethod]
        [ExpectedException(typeof(BankingException))]
        public void Deposit_ZeroAmount_ThrowsBankingException()
        {
            // Act
            _account.Deposit(0.00m);
        }

        [TestMethod]
        [ExpectedException(typeof(BankingException))]
        public void Deposit_NegativeAmount_ThrowsBankingException()
        {
            // Act
            _account.Deposit(-50.00m);
        }

        // ==========================================
        // WITHDRAWAL TESTS (HAPPY PATH & EDGE CASES)
        // ==========================================

        [TestMethod]
        public void Withdraw_ValidAmount_DecreasesBalance()
        {
            // Arrange
            decimal withdrawAmount = 150.00m;

            // Act
            bool result = _account.Withdraw(withdrawAmount);

            // Assert
            Assert.IsTrue(result);
            Assert.AreEqual(350.00m, _account.Balance);
            StringAssert.Contains(_account.LastTransactionStatus, "Withdrawal Successful");
        }

        [TestMethod]
        public void Withdraw_ExactBalance_ResultsInZeroBalance()
        {
            // Edge Case: withdrawing the entire exact balance
            decimal exactBalance = _account.Balance; // $500.00

            // Act
            bool result = _account.Withdraw(exactBalance);

            // Assert
            Assert.IsTrue(result);
            Assert.AreEqual(0.00m, _account.Balance);
            StringAssert.Contains(_account.LastTransactionStatus, "Withdrawal Successful");
        }

        [TestMethod]
        [ExpectedException(typeof(BankingException))]
        public void Withdraw_ZeroAmount_ThrowsBankingException()
        {
            // Edge Case: withdrawing $0
            _account.Withdraw(0.00m);
        }

        [TestMethod]
        [ExpectedException(typeof(BankingException))]
        public void Withdraw_NegativeAmount_ThrowsBankingException()
        {
            // Edge Case: withdrawing negative amount
            _account.Withdraw(-100.00m);
        }

        // ==========================================
        // FAILURE PATH & CUSTOM EXCEPTION TESTS
        // ==========================================

        [TestMethod]
        public void Withdraw_ExceedingBalance_ThrowsTailoredInsufficientFundsException()
        {
            // Failure Path: Everyday account does not allow overdraft
            decimal excessAmount = 600.00m; // Balance is $500.00

            try
            {
                _account.Withdraw(excessAmount);
                Assert.Fail("Expected InsufficientFundsException was not thrown.");
            }
            catch (InsufficientFundsException ex)
            {
                // Assert tailored error message
                StringAssert.Contains(ex.Message, "Everyday Account");
                StringAssert.Contains(ex.Message, "Overdraft is not permitted");
                // Balance remains unchanged
                Assert.AreEqual(500.00m, _account.Balance);
                StringAssert.Contains(_account.LastTransactionStatus, "Insufficient Funds");
            }
        }

        // ==========================================
        // INTEREST CALCULATION TESTS
        // ==========================================

        [TestMethod]
        public void CalculateInterest_EverydayAccount_AlwaysReturnsZeroAndDoesNotChangeBalance()
        {
            // Act
            decimal interest = _account.CalculateInterest();

            // Assert
            Assert.AreEqual(0.00m, interest);
            Assert.AreEqual(500.00m, _account.Balance);
            StringAssert.Contains(_account.LastTransactionStatus, "No interest");
        }
    }
}
