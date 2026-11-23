using System;
using System.IO;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using _20240040_MuskanPanwar_BankSprint1;

namespace BankSystem.Tests
{
    [TestClass]
    public class Sprint3TransferAndPersistenceTests
    {
        private CustomerController _controller = null!;
        private Customer _regularCust = null!;
        private Customer _staffCust = null!;
        private string _testJsonFile = null!;

        [TestInitialize]
        public void Setup()
        {
            _controller = new CustomerController();
            _regularCust = _controller.GetCustomerByNumber("20240040")!;
            _staffCust = _controller.GetCustomerByNumber("STAFF01")!;
            _testJsonFile = Path.Combine(Path.GetTempPath(), $"bank_test_{Guid.NewGuid():N}.json");
            _controller.PersistenceFilePath = _testJsonFile;
        }

        [TestCleanup]
        public void Cleanup()
        {
            if (File.Exists(_testJsonFile))
            {
                try { File.Delete(_testJsonFile); } catch { }
            }
        }

        // =========================================================================
        // TASK 1 & SPRINT 3: SUCCESSFUL INTRA-ACCOUNT TRANSFERS
        // =========================================================================

        [TestMethod]
        public void Transfer_EverydayToInvestment_TransfersFundsCorrectly()
        {
            // Arrange
            Account everyday = _regularCust.Accounts.First(a => a is EverydayAccount); // $500
            Account investment = _regularCust.Accounts.First(a => a is InvestmentAccount); // $2000

            // Act
            bool result = _controller.Transfer(_regularCust, everyday, investment, 200.00m);

            // Assert
            Assert.IsTrue(result);
            Assert.AreEqual(300.00m, everyday.Balance);
            Assert.AreEqual(2200.00m, investment.Balance);
            Assert.IsTrue(everyday.LastTransactionStatus.Contains("Transfer Out Successful"));
            Assert.IsTrue(investment.LastTransactionStatus.Contains("Transfer In Successful"));
        }

        [TestMethod]
        public void Transfer_InvestmentToOmni_TransfersFundsCorrectly()
        {
            Account investment = _regularCust.Accounts.First(a => a is InvestmentAccount); // $2000
            Account omni = _regularCust.Accounts.First(a => a is OmniAccount); // $1500

            bool result = _controller.Transfer(_regularCust.CustomerNumber, investment.AccountNumber, omni.AccountNumber, 500.00m);

            Assert.IsTrue(result);
            Assert.AreEqual(1500.00m, investment.Balance);
            Assert.AreEqual(2000.00m, omni.Balance);
        }

        [TestMethod]
        public void Transfer_OmniToEveryday_UsingApprovedOverdraft_Succeeds()
        {
            Account omni = _regularCust.Accounts.First(a => a is OmniAccount); // $1500 balance, $500 overdraft
            Account everyday = _regularCust.Accounts.First(a => a is EverydayAccount); // $500 balance

            // Transfer $1800 from Omni (exceeds balance $1500 by $300, but within $500 overdraft limit)
            bool result = _controller.Transfer(_regularCust, omni, everyday, 1800.00m);

            Assert.IsTrue(result);
            Assert.AreEqual(-300.00m, omni.Balance); // Entered overdraft safely
            Assert.AreEqual(2300.00m, everyday.Balance);
        }

        // =========================================================================
        // TASK 1 & SPRINT 3: FAILURE STATES & OVERDRAFT BREACHES
        // =========================================================================

        [TestMethod]
        [ExpectedException(typeof(InsufficientFundsException))]
        public void Transfer_EverydayExceedingBalance_ThrowsExceptionAndAppliesNoFee()
        {
            Account everyday = _regularCust.Accounts.First(a => a is EverydayAccount); // $500
            Account investment = _regularCust.Accounts.First(a => a is InvestmentAccount);

            try
            {
                _controller.Transfer(_regularCust, everyday, investment, 600.00m);
            }
            finally
            {
                // Everyday accounts never charge failed fees
                Assert.AreEqual(500.00m, everyday.Balance);
            }
        }

        [TestMethod]
        public void Transfer_InvestmentInsufficientFunds_RegularCustomer_ChargesStandard10DollarFee()
        {
            Account investment = _regularCust.Accounts.First(a => a is InvestmentAccount); // $2000
            Account everyday = _regularCust.Accounts.First(a => a is EverydayAccount);

            try
            {
                _controller.Transfer(_regularCust, investment, everyday, 3000.00m);
                Assert.Fail("Expected InsufficientFundsException was not thrown.");
            }
            catch (InsufficientFundsException ex)
            {
                // Regular customer incurs standard $10.00 fee
                Assert.AreEqual(1990.00m, investment.Balance);
                Assert.AreEqual(500.00m, everyday.Balance); // Destination unaffected
                StringAssert.Contains(ex.Message, "fee of $10.00 has been charged");
            }
        }

        [TestMethod]
        public void Transfer_InvestmentInsufficientFunds_StaffMember_Applies50PercentDiscountFee()
        {
            Account staffInv = _staffCust.Accounts.First(a => a is InvestmentAccount); // $2000
            Account staffEd = _staffCust.Accounts.First(a => a is EverydayAccount);

            try
            {
                _controller.Transfer(_staffCust, staffInv, staffEd, 5000.00m);
                Assert.Fail("Expected InsufficientFundsException was not thrown.");
            }
            catch (InsufficientFundsException ex)
            {
                // Staff member gets 50% discount: $5.00 fee instead of $10.00
                Assert.AreEqual(1995.00m, staffInv.Balance);
                StringAssert.Contains(ex.Message, "fee of $5.00 has been charged");
            }
        }

        [TestMethod]
        public void Transfer_OmniExceedingOverdraft_RegularCustomer_Charges10DollarFee()
        {
            Account omni = _regularCust.Accounts.First(a => a is OmniAccount); // $1500 balance + $500 overdraft = $2000 max
            Account everyday = _regularCust.Accounts.First(a => a is EverydayAccount);

            try
            {
                _controller.Transfer(_regularCust, omni, everyday, 2050.00m);
                Assert.Fail("Expected InsufficientFundsException was not thrown.");
            }
            catch (InsufficientFundsException ex)
            {
                // Regular customer incurs $10.00 failed fee deducted from balance
                Assert.AreEqual(1490.00m, omni.Balance);
                StringAssert.Contains(ex.Message, "fee of $10.00 has been charged");
            }
        }

        [TestMethod]
        public void Transfer_OmniExceedingOverdraft_StaffMember_Applies5DollarDiscountFee()
        {
            Account staffOmni = _staffCust.Accounts.First(a => a is OmniAccount); // $1500 balance + $500 overdraft
            Account staffEd = _staffCust.Accounts.First(a => a is EverydayAccount);

            try
            {
                _controller.Transfer(_staffCust, staffOmni, staffEd, 3000.00m);
                Assert.Fail("Expected InsufficientFundsException was not thrown.");
            }
            catch (InsufficientFundsException ex)
            {
                // Staff member pays $5.00 fee
                Assert.AreEqual(1495.00m, staffOmni.Balance);
                StringAssert.Contains(ex.Message, "fee of $5.00 has been charged");
            }
        }

        [TestMethod]
        [ExpectedException(typeof(BankingException))]
        public void Transfer_IdenticalSourceAndDestination_ThrowsBankingException()
        {
            Account everyday = _regularCust.Accounts.First(a => a is EverydayAccount);
            _controller.Transfer(_regularCust, everyday, everyday, 50.00m);
        }

        [TestMethod]
        [ExpectedException(typeof(BankingException))]
        public void Transfer_NegativeAmount_ThrowsBankingException()
        {
            Account everyday = _regularCust.Accounts.First(a => a is EverydayAccount);
            Account investment = _regularCust.Accounts.First(a => a is InvestmentAccount);

            _controller.Transfer(_regularCust, everyday, investment, -50.00m);
        }

        [TestMethod]
        [ExpectedException(typeof(BankingException))]
        public void Transfer_ZeroAmount_ThrowsBankingException()
        {
            Account everyday = _regularCust.Accounts.First(a => a is EverydayAccount);
            Account investment = _regularCust.Accounts.First(a => a is InvestmentAccount);

            _controller.Transfer(_regularCust, everyday, investment, 0m);
        }

        // =========================================================================
        // TASK 3: DYNAMIC ACCOUNT CREATION (ONE-TO-MANY EXPANSION)
        // =========================================================================

        [TestMethod]
        public void AddAccount_EverydayAccount_SuccessfullyAddsToCustomer()
        {
            int beforeCount = _regularCust.Accounts.Count;

            Account created = _controller.AddAccount("20240040", "Everyday", "ED-NEW-99", 350.00m);

            Assert.IsNotNull(created);
            Assert.IsInstanceOfType(created, typeof(EverydayAccount));
            Assert.AreEqual("ED-NEW-99", created.AccountNumber);
            Assert.AreEqual(350.00m, created.Balance);
            Assert.AreEqual(beforeCount + 1, _regularCust.Accounts.Count);
            Assert.AreSame(_regularCust, created.Owner);
        }

        [TestMethod]
        public void AddAccount_InvestmentAccount_SetsInterestRateCorrectly()
        {
            Account created = _controller.AddAccount("20240040", "Investment", "INV-HIGH-YIELD", 5000.00m, interestRate: 6.25m);

            Assert.IsNotNull(created);
            Assert.IsInstanceOfType(created, typeof(InvestmentAccount));
            var inv = (InvestmentAccount)created;
            Assert.AreEqual(6.25m, inv.InterestRate);
            Assert.AreEqual(5000.00m, inv.Balance);
        }

        [TestMethod]
        public void AddAccount_OmniAccount_SetsOverdraftAndInterestCorrectly()
        {
            Account created = _controller.AddAccount("STAFF01", "Omni", "OMN-VIP-01", 10000.00m, interestRate: 4.5m, overdraftLimit: 2500.00m);

            Assert.IsNotNull(created);
            Assert.IsInstanceOfType(created, typeof(OmniAccount));
            var omni = (OmniAccount)created;
            Assert.AreEqual(4.5m, omni.InterestRate);
            Assert.AreEqual(2500.00m, omni.OverdraftLimit);
            Assert.AreEqual(10000.00m, omni.Balance);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void AddAccount_DuplicateAccountNumber_ThrowsArgumentException()
        {
            // "ED-20240040" already exists
            _controller.AddAccount("STAFF01", "Everyday", "ED-20240040", 100.00m);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void AddAccount_NegativeBalance_ThrowsArgumentException()
        {
            _controller.AddAccount("20240040", "Everyday", "ED-BAD-BAL", -100.00m);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void AddAccount_InvalidType_ThrowsArgumentException()
        {
            _controller.AddAccount("20240040", "CryptoWallet", "CRYPTO-01", 100.00m);
        }

        // =========================================================================
        // TASK 5: DATA PERSISTENCE & POLYMORPHIC JSON SERIALIZATION
        // =========================================================================

        [TestMethod]
        public void JsonPersistence_ExportContainsPolymorphicDiscriminator()
        {
            string json = _controller.ExportToJson();

            Assert.IsFalse(string.IsNullOrWhiteSpace(json));
            StringAssert.Contains(json, "$accountType");
            StringAssert.Contains(json, "Everyday");
            StringAssert.Contains(json, "Investment");
            StringAssert.Contains(json, "Omni");
        }

        [TestMethod]
        public void JsonPersistence_SaveAndLoad_RestoresCompleteSystemState()
        {
            // Modify state before save
            Account ed = _regularCust.Accounts.First(a => a is EverydayAccount);
            ed.Deposit(777.00m);
            _controller.CreateCustomer("CUST-PERSIST", "Persistence Tester", "test@persist.com", true);

            // Save to disk
            _controller.SaveToFile();
            Assert.IsTrue(File.Exists(_testJsonFile));

            // Create new fresh controller and load
            var freshController = new CustomerController
            {
                PersistenceFilePath = _testJsonFile
            };
            bool loaded = freshController.LoadFromFile();

            // Assert
            Assert.IsTrue(loaded);
            Assert.AreEqual(3, freshController.CustomerCount);

            Customer reloadedRegular = freshController.GetCustomerByNumber("20240040")!;
            Assert.IsNotNull(reloadedRegular);
            Assert.AreEqual("Muskan Panwar", reloadedRegular.Name);
            Assert.IsFalse(reloadedRegular.IsStaff);

            Account reloadedEd = reloadedRegular.Accounts.First(a => a is EverydayAccount);
            Assert.AreEqual(1277.00m, reloadedEd.Balance); // 500 + 777
            Assert.AreSame(reloadedRegular, reloadedEd.Owner); // Back-reference restored!

            Customer reloadedNew = freshController.GetCustomerByNumber("CUST-PERSIST")!;
            Assert.IsNotNull(reloadedNew);
            Assert.IsTrue(reloadedNew.IsStaff);
            Assert.AreEqual(3, reloadedNew.Accounts.Count);

            // Test polymorphism on reloaded accounts
            Assert.IsInstanceOfType(reloadedNew.Accounts[0], typeof(EverydayAccount));
            Assert.IsInstanceOfType(reloadedNew.Accounts[1], typeof(InvestmentAccount));
            Assert.IsInstanceOfType(reloadedNew.Accounts[2], typeof(OmniAccount));
        }
    }
}
