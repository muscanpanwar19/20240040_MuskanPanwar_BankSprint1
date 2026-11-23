using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using _20240040_MuskanPanwar_BankSprint1;

namespace BankSystem.Tests
{
    [TestClass]
    public class InvestmentAccountTests
    {
        private Customer _regularCustomer;
        private Customer _staffCustomer;
        private InvestmentAccount _regAccount;
        private InvestmentAccount _staffAccount;

        [TestInitialize]
        public void Setup()
        {
            _regularCustomer = new Customer("CUST01", "Regular Customer", "reg@bank.com", false);
            _staffCustomer = new Customer("STAFF01", "Staff Member", "staff@bank.com", true);

            _regAccount = new InvestmentAccount("INV-REG01", 2000.00m, 4.0m, _regularCustomer);
            _staffAccount = new InvestmentAccount("INV-STF01", 2000.00m, 4.0m, _staffCustomer);
        }

        // ==========================================
        // DEPOSIT TESTS
        // ==========================================

        [TestMethod]
        public void Deposit_PositiveAmount_IncreasesBalance()
        {
            // Act
            bool result = _regAccount.Deposit(500.00m);

            // Assert
            Assert.IsTrue(result);
            Assert.AreEqual(2500.00m, _regAccount.Balance);
            StringAssert.Contains(_regAccount.LastTransactionStatus, "Deposit Successful");
        }

        [TestMethod]
        [ExpectedException(typeof(BankingException))]
        public void Deposit_ZeroAmount_ThrowsBankingException()
        {
            _regAccount.Deposit(0.00m);
        }

        // ==========================================
        // WITHDRAWAL HAPPY PATH & EDGE CASES
        // ==========================================

        [TestMethod]
        public void Withdraw_ValidAmount_DecreasesBalance()
        {
            // Act
            bool result = _regAccount.Withdraw(500.00m);

            // Assert
            Assert.IsTrue(result);
            Assert.AreEqual(1500.00m, _regAccount.Balance);
            StringAssert.Contains(_regAccount.LastTransactionStatus, "Withdrawal Successful");
        }

        [TestMethod]
        public void Withdraw_ExactBalance_ResultsInZeroBalance()
        {
            // Edge Case: withdrawing exact balance ($2000.00)
            bool result = _regAccount.Withdraw(2000.00m);

            // Assert
            Assert.IsTrue(result);
            Assert.AreEqual(0.00m, _regAccount.Balance);
            StringAssert.Contains(_regAccount.LastTransactionStatus, "Withdrawal Successful");
        }

        [TestMethod]
        [ExpectedException(typeof(BankingException))]
        public void Withdraw_ZeroAmount_ThrowsBankingException()
        {
            // Edge Case: withdrawing $0
            _regAccount.Withdraw(0.00m);
        }

        [TestMethod]
        [ExpectedException(typeof(BankingException))]
        public void Withdraw_NegativeAmount_ThrowsBankingException()
        {
            // Edge Case: negative withdrawal
            _regAccount.Withdraw(-100.00m);
        }

        // ==========================================
        // FAILURE PATH & FEE APPLICATION (REGULAR VS STAFF)
        // ==========================================

        [TestMethod]
        public void Withdraw_ExceedingBalance_RegularCustomer_ChargesStandardFee_AndThrowsTailoredException()
        {
            // Failure Path: Regular customer overdraws -> standard $10 fee charged
            decimal initialBalance = _regAccount.Balance; // $2000.00
            decimal requestedAmount = 5000.00m;

            try
            {
                _regAccount.Withdraw(requestedAmount);
                Assert.Fail("Expected InsufficientFundsException was not thrown.");
            }
            catch (InsufficientFundsException ex)
            {
                // Assert $10 standard fee was charged
                Assert.AreEqual(initialBalance - 10.00m, _regAccount.Balance);
                // Assert tailored exception message mentions fee and Investment Account
                StringAssert.Contains(ex.Message, "Investment Account");
                StringAssert.Contains(ex.Message, "10.00");
                StringAssert.Contains(_regAccount.LastTransactionStatus, "Fee charged: $10.00");
            }
        }

        [TestMethod]
        public void Withdraw_ExceedingBalance_StaffCustomer_ChargesDiscountedFee_AndThrowsTailoredException()
        {
            // Failure Path: Bank staff gets 50% discount -> $5 fee charged
            decimal initialBalance = _staffAccount.Balance; // $2000.00
            decimal requestedAmount = 5000.00m;

            try
            {
                _staffAccount.Withdraw(requestedAmount);
                Assert.Fail("Expected InsufficientFundsException was not thrown.");
            }
            catch (InsufficientFundsException ex)
            {
                // Assert 50% discounted fee ($5.00) was charged
                Assert.AreEqual(initialBalance - 5.00m, _staffAccount.Balance);
                // Assert tailored exception message mentions $5 fee
                StringAssert.Contains(ex.Message, "Investment Account");
                StringAssert.Contains(ex.Message, "5.00");
                StringAssert.Contains(_staffAccount.LastTransactionStatus, "Fee charged: $5.00");
            }
        }

        // ==========================================
        // INTEREST CALCULATION TESTS
        // ==========================================

        [TestMethod]
        public void CalculateInterest_PositiveBalance_AppliesConfiguredInterestRate()
        {
            // Balance: $2000.00, InterestRate: 4.0% -> Interest = $80.00
            decimal interest = _regAccount.CalculateInterest();

            // Assert
            Assert.AreEqual(80.00m, interest);
            Assert.AreEqual(2080.00m, _regAccount.Balance);
            StringAssert.Contains(_regAccount.LastTransactionStatus, "Interest added: $80.00");
        }

        [TestMethod]
        public void CalculateInterest_ZeroOrNegativeBalance_DoesNotAddInterest()
        {
            // Arrange account with 0 balance
            InvestmentAccount zeroBalAccount = new InvestmentAccount("INV-ZERO", 0.00m, 4.0m, _regularCustomer);

            // Act
            decimal interest = zeroBalAccount.CalculateInterest();

            // Assert
            Assert.AreEqual(0.00m, interest);
            Assert.AreEqual(0.00m, zeroBalAccount.Balance);
            StringAssert.Contains(zeroBalAccount.LastTransactionStatus, "No interest added");
        }
    }
}
