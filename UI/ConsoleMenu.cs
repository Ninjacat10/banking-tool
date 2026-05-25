// File: UI/ConsoleMenu.cs
using System;
using System.Collections.Generic;
using BankingSystem.Services;
using BankingSystem.Models;

namespace BankingSystem.UI
{
    public class ConsoleMenu
    {
        private readonly BankingService _bankingService;

        public ConsoleMenu(BankingService bankingService)
        {
            _bankingService = bankingService;
        }

        public void Run()
        {
            bool keepRunning = true;
            while (keepRunning)
            {
                Console.WriteLine("\n=== CORE BANKING SYSTEM ===");
                Console.WriteLine("1. Create New Account");
                Console.WriteLine("2. Deposit Funds");
                Console.WriteLine("3. Withdraw Funds");
                Console.WriteLine("4. Check Account Balance");
                Console.WriteLine("5. Exit");
                Console.Write("Choose an option (1-5): ");

                string choice = Console.ReadLine();

                try
                {
                    switch (choice)
                    {
                        case "1": RunCreateAccount(); break;
                        case "2": RunDeposit(); break;
                        case "3": RunWithdraw(); break;
                        case "4": RunCheckBalance(); break;
                        case "5": 
                            Console.WriteLine("Exiting application safely...");
                            keepRunning = false; 
                            break;
                        default: 
                            Console.WriteLine("Invalid selection. Try again."); 
                            break;
                    }
                }
                catch (Exception ex)
                {
                    // Global try-catch handles our business logic violations gracefully without crashing the app
                    Console.WriteLine($"\n[ERROR] {ex.Message}");
                }
            }
        }

        private void RunCreateAccount()
        {
            Console.Write("Enter Account Number: ");
            string num = Console.ReadLine();
            Console.Write("Enter Account Holder Name: ");
            string holder = Console.ReadLine();
            Console.Write("Enter Initial Deposit Amount: ");
            decimal initialDeposit = decimal.Parse(Console.ReadLine());

            _bankingService.CreateAccount(num, holder, initialDeposit);
            Console.WriteLine("Account successfully registered!");
        }

        private void RunDeposit()
        {
            Console.Write("Enter Account Number: ");
            string num = Console.ReadLine();
            Console.Write("Enter Deposit Amount: ");
            decimal amount = decimal.Parse(Console.ReadLine());

            _bankingService.ExecuteDeposit(num, amount);
            Console.WriteLine("Deposit processed successfully.");
        }

        private void RunWithdraw()
        {
            Console.Write("Enter Account Number: ");
            string num = Console.ReadLine();
            Console.Write("Enter Withdrawal Amount: ");
            decimal amount = decimal.Parse(Console.ReadLine());

            _bankingService.ExecuteWithdrawal(num, amount);
            Console.WriteLine("Withdrawal processed successfully.");
        }

        private void RunCheckBalance()
        {
            Console.Write("Enter Account Number: ");
            string num = Console.ReadLine();

            Account acc = _bankingService.FindAccount(num);
            if (acc == null)
            {
                Console.WriteLine("Account not found.");
            }
            else
            {
                Console.WriteLine($"\nAccount Holder: {acc.AccountHolder}");
                Console.WriteLine($"Current Balance: {acc.Balance:C}"); // :C formats as currency (e.g., $100.00)
            }
        }
    }
}