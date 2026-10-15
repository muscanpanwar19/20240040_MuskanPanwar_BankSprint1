using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using _20240040_MuskanPanwar_BankSprint1;

namespace BankSystem.Tests
{
    [TestClass]
    public class OmniAccountTests
    {
        private Customer _regularCustomer;
        private Customer _staffCustomer;
        private OmniAccount _regOmni;
        private OmniAccount _staffOmni;

        [TestInitialize]
        public void Setup()
        {
            _regularCustomer = new Customer("CUST01", "Regular Customer", "reg@bank.com", false);
            _staffCustomer = new Customer("STAFF01", "Staff Member", "staff@bank.com", true);

            // Starting balance: $1500.00, InterestRate: 5.0%, OverdraftLimit: $500.00
            _regOmni = new OmniAccount("OMN-REG01", 1500.00m, 5.0m, 500.00m, _regularCustomer);
            _staffOmni = new OmniAccount("OMN-STF01", 1500.00m, 5.0m, 500.00m, _staffCustomer);
        }

        // ==========================================
        // DEPOSIT TESTS
        // ==========================================

        [TestMethod]
        public void Deposit_PositiveAmount_IncreasesBalance()
        {
            // Act
            bool result = _regOmni.Deposit(500.00m);

            // Assert
            Assert.IsTrue(result);
            Assert.AreEqual(2000.00m, _regOmni.Balance);
            StringAssert.Contains(_regOmni.LastTransactionStatus, "Deposit Successful");
        }

        [TestMethod]
        [ExpectedException(typeof(BankingException))]
        public void Deposit_ZeroAmount_ThrowsBankingException()
        {
            _regOmni.Deposit(0.00m);
        }

        // ==========================================
        // WITHDRAWAL HAPPY PATH & OVERDRAFT EDGE CASES
        // ==========================================

        [TestMethod]
        public void Withdraw_WithinPositiveBalance_DecreasesBalance()
        {
            // Act: Withdraw $500 from $1500
            bool result = _regOmni.Withdraw(500.00m);

            // Assert
            Assert.IsTrue(result);
            Assert.AreEqual(1000.00m, _regOmni.Balance);
            StringAssert.Contains(_regOmni.LastTransactionStatus, "Withdrawal Successful");
        }

        [TestMethod]
        public void Withdraw_UsingOverdraftWithinLimit_AllowsNegativeBalance()
        {
            // Balance: $1500.00, Overdraft: $500.00 (Total available: $2000.00)
            // Act: Withdraw $1800.00
            bool result = _regOmni.Withdraw(1800.00m);

            // Assert: Balance becomes -$300.00
            Assert.IsTrue(result);
            Assert.AreEqual(-300.00m, _regOmni.Balance);
            StringAssert.Contains(_regOmni.LastTransactionStatus, "Withdrawal Successful");
        }

        [TestMethod]
        public void Withdraw_ExactOverdraftLimit_BalanceReachesNegativeOverdraftLimit()
        {
            // Edge Case: Withdrawing entire available funds (Balance $1500 + Overdraft $500 = $2000.00)
            bool result = _regOmni.Withdraw(2000.00m);

            // Assert
            Assert.IsTrue(result);
            Assert.AreEqual(-500.00m, _regOmni.Balance);
            StringAssert.Contains(_regOmni.LastTransactionStatus, "Withdrawal Successful");
        }

        [TestMethod]
        [ExpectedException(typeof(BankingException))]
        public void Withdraw_ZeroAmount_ThrowsBankingException()
        {
            // Edge Case: $0 withdrawal
            _regOmni.Withdraw(0.00m);
        }

        [TestMethod]
        [ExpectedException(typeof(BankingException))]
        public void Withdraw_NegativeAmount_ThrowsBankingException()
        {
            // Edge Case: Negative withdrawal
            _regOmni.Withdraw(-50.00m);
        }

        // ==========================================
        // MANDATORY EDGE CASE: EXCEEDING OVERDRAFT BY EXACTLY $0.01
        // ==========================================

        [TestMethod]
        public void Withdraw_ExceedingOverdraftByOneCent_RegularCustomer_ChargesFeeAndThrowsTailoredException()
        {
            // Edge Case from Assignment Brief:
            // Available is Balance ($1500.00) + Overdraft ($500.00) = $2000.00
            // Attempting to withdraw $2000.01 exceeds limit by exactly $0.01!
            decimal initialBalance = _regOmni.Balance; // $1500.00
            decimal overdrawByOneCent = 2000.01m;

            try
            {
                _regOmni.Withdraw(overdrawByOneCent);
                Assert.Fail("Expected InsufficientFundsException was not thrown for exceeding overdraft by $0.01.");
            }
            catch (InsufficientFundsException ex)
            {
                // Assert standard $10 fee charged to balance
                Assert.AreEqual(initialBalance - 10.00m, _regOmni.Balance);
                // Assert tailored exception message mentions Omni Account and overdraft limit
                StringAssert.Contains(ex.Message, "Omni Account");
                StringAssert.Contains(ex.Message, "Overdraft limit exceeded");
                StringAssert.Contains(ex.Message, "10.00");
                StringAssert.Contains(_regOmni.LastTransactionStatus, "Overdraft exceeded. Fee charged: $10.00");
            }
        }

        [TestMethod]
        public void Withdraw_ExceedingOverdraft_StaffCustomer_ChargesDiscountedFee()
        {
            // Bank staff gets 50% discount on $10 failed fee -> $5 charged
            decimal initialBalance = _staffOmni.Balance; // $1500.00
            decimal overdrawAmount = 2500.00m;

            try
            {
                _staffOmni.Withdraw(overdrawAmount);
                Assert.Fail("Expected InsufficientFundsException was not thrown.");
            }
            catch (InsufficientFundsException ex)
            {
                // Assert 50% discounted fee ($5.00) was charged
                Assert.AreEqual(initialBalance - 5.00m, _staffOmni.Balance);
                StringAssert.Contains(ex.Message, "Omni Account");
                StringAssert.Contains(ex.Message, "5.00");
                StringAssert.Contains(_staffOmni.LastTransactionStatus, "Fee charged: $5.00");
            }
        }

        // ==========================================
        // INTEREST CALCULATION TESTS (> $1,000 RULE)
        // ==========================================

        [TestMethod]
        public void CalculateInterest_BalanceAbove1000_AppliesInterestOnlyToExcessPortion()
        {
            // Balance: $1500.00, Excess over $1,000 is $500.00
            // Interest rate: 5.0% -> Interest = $500 * 0.05 = $25.00
            decimal interest = _regOmni.CalculateInterest();

            // Assert
            Assert.AreEqual(25.00m, interest);
            Assert.AreEqual(1525.00m, _regOmni.Balance);
            StringAssert.Contains(_regOmni.LastTransactionStatus, "Interest added: $25.00");
        }

        [TestMethod]
        public void CalculateInterest_Exact1000Balance_AppliesZeroInterest()
        {
            // Edge Case: Balance is exactly $1,000.00 -> No excess over $1,000
            OmniAccount exactAccount = new OmniAccount("OMN-1K", 1000.00m, 5.0m, 500.00m, _regularCustomer);

            decimal interest = exactAccount.CalculateInterest();

            Assert.AreEqual(0.00m, interest);
            Assert.AreEqual(1000.00m, exactAccount.Balance);
            StringAssert.Contains(exactAccount.LastTransactionStatus, "No interest: Balance under $1000");
        }

        [TestMethod]
        public void CalculateInterest_BalanceUnder1000_AppliesZeroInterest()
        {
            // Balance: $600.00 (< $1000.00) -> 0 interest
            OmniAccount lowAccount = new OmniAccount("OMN-LOW", 600.00m, 5.0m, 500.00m, _regularCustomer);

            decimal interest = lowAccount.CalculateInterest();

            Assert.AreEqual(0.00m, interest);
            Assert.AreEqual(600.00m, lowAccount.Balance);
            StringAssert.Contains(lowAccount.LastTransactionStatus, "No interest: Balance under $1000");
        }
    }
}
