using System;
using System.Collections.Generic;

namespace _20240040_MuskanPanwar_BankSprint1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=================================================");
            Console.WriteLine("    NOVA PREMIER BANK - SPRINT 2 (MVC & TESTS)   ");
            Console.WriteLine("=================================================\n");

            // 1. Initialize CustomerController (MVC Controller Layer)
            CustomerController controller = new CustomerController();
            Console.WriteLine("[MVC CONTROLLER] Initialized CustomerController with default records.");
            Console.WriteLine($"Total Customers Loaded: {controller.CustomerCount}\n");

            // Display Seeded Customers
            Console.WriteLine("--- Initial Customer List (Read) ---");
            foreach (Customer c in controller.GetAllCustomers())
            {
                Console.WriteLine("  • " + c.GetCustomerDetails());
            }
            Console.WriteLine();

            // 2. Demonstrate CRUD - CREATE
            Console.WriteLine("--- 1. Demonstrating CRUD: CREATE ---");
            Customer newCustomer = controller.CreateCustomer("20240099", "Sarah Connor", "sarah.connor@email.com", false);
            Console.WriteLine($"Created: {newCustomer.GetCustomerDetails()}");
            Console.WriteLine($"Accounts Created: {newCustomer.Accounts.Count} (Everyday, Investment, Omni)");
            Console.WriteLine($"Total Customers Now: {controller.CustomerCount}\n");

            // 3. Demonstrate CRUD - UPDATE
            Console.WriteLine("--- 2. Demonstrating CRUD: UPDATE ---");
            controller.UpdateCustomer("20240099", "Sarah Connor-Reese", "sarah.reese@email.com", true);
            Customer updatedCustomer = controller.GetCustomerByNumber("20240099");
            Console.WriteLine($"Updated: {updatedCustomer.GetCustomerDetails()}\n");

            // 4. Test Everyday Account & InsufficientFundsException
            Console.WriteLine("--- 3. Testing Everyday Account & Exception Handling ---");
            Customer muskan = controller.GetCustomerByNumber("20240040");
            Account everyday = muskan.Accounts[0];

            controller.Deposit(everyday, 100.00m);
            Console.WriteLine("Deposit $100:       " + everyday.GetAccountInfo());

            controller.Withdraw(everyday, 200.00m);
            Console.WriteLine("Withdraw $200:      " + everyday.GetAccountInfo());

            // Edge Case: Attempt to withdraw exact balance ($400)
            controller.Withdraw(everyday, everyday.Balance);
            Console.WriteLine("Withdraw exact bal: " + everyday.GetAccountInfo());

            // Failure Path: Attempt to withdraw with $0 balance -> triggers tailored InsufficientFundsException
            try
            {
                Console.WriteLine("Attempting withdrawal of $50 from empty Everyday account...");
                controller.Withdraw(everyday, 50.00m);
            }
            catch (InsufficientFundsException ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"[CAUGHT EXCEPTION] {ex.GetType().Name}: {ex.Message}");
                Console.ResetColor();
            }
            Console.WriteLine();

            // 5. Test Investment Account, Fees & Staff Discount
            Console.WriteLine("--- 4. Testing Investment Account & Fee Application ---");
            Customer staffMember = controller.GetCustomerByNumber("STAFF01");
            Account regInvestment = muskan.Accounts[1];
            Account staffInvestment = staffMember.Accounts[1];

            // Deposit to Muskan's investment account
            controller.Deposit(regInvestment, 500.00m);
            Console.WriteLine("Muskan Investment:  " + regInvestment.GetAccountInfo());

            // Regular customer overdraws -> $10 fee charged & InsufficientFundsException thrown
            try
            {
                Console.WriteLine("Muskan overdrawing $10,000 (Standard $10 Fee)...");
                controller.Withdraw(regInvestment, 10000.00m);
            }
            catch (InsufficientFundsException ex)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"[REGULAR FEE APPLIED] {ex.Message}");
                Console.ResetColor();
            }

            // Staff member overdraws -> $5 fee charged (50% discount) & InsufficientFundsException thrown
            try
            {
                Console.WriteLine("Rakshika (Staff) overdrawing $10,000 (50% Discount -> $5 Fee)...");
                controller.Withdraw(staffInvestment, 10000.00m);
            }
            catch (InsufficientFundsException ex)
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine($"[STAFF DISCOUNT FEE]  {ex.Message}");
                Console.ResetColor();
            }
            Console.WriteLine();

            // 6. Test Omni Account & Overdraft Edge Cases
            Console.WriteLine("--- 5. Testing Omni Account & Overdraft Edge Cases ---");
            Account omni = muskan.Accounts[2]; // Initial balance: $1500, Overdraft: $500
            Console.WriteLine("Initial Omni:       " + omni.GetAccountInfo());

            // Interest on balance exceeding $1,000 (5% of $500 = $25)
            controller.CalculateInterest(omni);
            Console.WriteLine("Interest applied:   " + omni.GetAccountInfo());

            // Withdraw using overdraft successfully (Balance $1525 - $1800 = -$275)
            controller.Withdraw(omni, 1800.00m);
            Console.WriteLine("Overdraft used:     " + omni.GetAccountInfo());

            // Edge Case: Exceeding overdraft limit by $0.01
            // Available is overdraft limit ($500) - current overdraft ($275) = $225
            decimal remainingOverdraft = 500.00m - Math.Abs(omni.Balance);
            decimal overdrawByOneCent = remainingOverdraft + 0.01m;

            try
            {
                Console.WriteLine($"Attempting withdrawal exceeding overdraft by exactly $0.01 (${overdrawByOneCent:F2})...");
                controller.Withdraw(omni, overdrawByOneCent);
            }
            catch (InsufficientFundsException ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"[OVERDRAFT EXCEEDED] {ex.Message}");
                Console.ResetColor();
            }

            // Edge Case: Zero / Negative withdrawal
            try
            {
                Console.WriteLine("Attempting withdrawal of $0.00...");
                controller.Withdraw(omni, 0.00m);
            }
            catch (BankingException ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"[ZERO AMOUNT BLOCKED] {ex.Message}");
                Console.ResetColor();
            }
            Console.WriteLine();

            // 7. Demonstrate CRUD - DELETE
            Console.WriteLine("--- 6. Demonstrating CRUD: DELETE ---");
            bool deleted = controller.DeleteCustomer("20240099");
            Console.WriteLine($"Customer '20240099' deleted successfully: {deleted}");
            Console.WriteLine($"Total Customers Remaining: {controller.CustomerCount}\n");

            Console.WriteLine("=================================================");
            Console.WriteLine(" All Sprint 2 MVC & Exception tests executed!    ");
            Console.WriteLine("=================================================");
        }
    }
}
