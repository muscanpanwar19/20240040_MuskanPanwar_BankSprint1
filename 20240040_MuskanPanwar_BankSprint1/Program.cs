using System;

namespace _20240040_MuskanPanwar_BankSprint1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=================================================");
            Console.WriteLine("     BANK ACCOUNT MANAGEMENT SYSTEM - SPRINT 1   ");
            Console.WriteLine("=================================================\n");

            // 1. Create Regular Customer (Muskan Panwar) & Bank Staff Customer (Rakshika)
            Customer regularCustomer = new Customer("20240040", "Muskan Panwar", "muskan.panwar@email.com", false);
            regularCustomer.PrePopulateAccounts();

            Customer staffCustomer = new Customer("STAFF01", "Rakshika", "rakshika@bank.com", true);
            staffCustomer.PrePopulateAccounts();

            Console.WriteLine(regularCustomer.GetCustomerDetails());
            Console.WriteLine(staffCustomer.GetCustomerDetails());
            Console.WriteLine();

            // 2. Test Everyday Account (No interest, No overdraft, No fee)
            Console.WriteLine("--- 1. Testing Everyday Account (Muskan Panwar) ---");
            Account everyday = regularCustomer.Accounts[0];
            everyday.Deposit(100);
            Console.WriteLine(everyday.GetAccountInfo());

            everyday.Withdraw(200); // Successful withdrawal
            Console.WriteLine(everyday.GetAccountInfo());

            everyday.Withdraw(900); // Fails: Insufficient funds (No fee charged)
            Console.WriteLine(everyday.GetAccountInfo());

            everyday.CalculateInterest(); // 0 interest
            Console.WriteLine(everyday.GetAccountInfo());
            Console.WriteLine();

            // 3. Test Investment Account (Variable interest, Fee for regular vs 50% discount for staff)
            Console.WriteLine("--- 2. Testing Investment Account & Staff Discount ---");
            Account regInvestment = regularCustomer.Accounts[1];
            Account staffInvestment = staffCustomer.Accounts[1];

            // Muskan (Regular Customer) overdraws -> Charges standard $10 fee
            regInvestment.Withdraw(5000); 
            Console.WriteLine("Regular Customer (Muskan): " + regInvestment.GetAccountInfo());

            // Rakshika (Staff Customer) overdraws -> Charges discounted $5 fee (50% off)
            staffInvestment.Withdraw(5000); 
            Console.WriteLine("Staff Member (Rakshika):   " + staffInvestment.GetAccountInfo());

            // Calculate interest on total balance
            regInvestment.CalculateInterest();
            Console.WriteLine("Interest applied:          " + regInvestment.GetAccountInfo());
            Console.WriteLine();

            // 4. Test Omni Account (Overdraft & Interest on balance > $1000)
            Console.WriteLine("--- 3. Testing Omni Account (Muskan Panwar) ---");
            Account omni = regularCustomer.Accounts[2];

            omni.CalculateInterest(); // Interest on balance exceeding $1,000
            Console.WriteLine("Interest applied: " + omni.GetAccountInfo());

            omni.Withdraw(1800); // Successful withdrawal using $500 overdraft limit
            Console.WriteLine("Overdraft used:   " + omni.GetAccountInfo());

            omni.Withdraw(500); // Fails: Exceeds overdraft limit -> Fee charged
            Console.WriteLine("Limit exceeded:   " + omni.GetAccountInfo());
            Console.WriteLine();

            Console.WriteLine("=================================================");
            Console.WriteLine("All tests completed successfully!");
            Console.WriteLine("=================================================");
        }
    }
}
