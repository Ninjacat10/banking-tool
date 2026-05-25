// File: Models/Account.cs
using System;

namespace BankingSystem.Models
{
    public class Account
    {
        // Public properties with getters for JSON serialization.
        // Setters are kept private or restricted to protect the data from outside tampering.
        public string AccountNumber { get; private set; }
        public string AccountHolder { get; private set; }
        public decimal Balance { get; private set; }

        // This constructor is used when creating a brand new account
        public Account(string accountNumber, string accountHolder, decimal initialDeposit)
        {
            if (initialDeposit < 0)
                throw new ArgumentException("Initial deposit cannot be negative.");

            AccountNumber = accountNumber;
            AccountHolder = accountHolder;
            Balance = initialDeposit;
        }

        // Parameterless constructor strictly required for JSON Deserialization
        public Account() { }

        // Core business mutations happen safely inside the object itself
        public void Deposit(decimal amount)
        {
            if (amount <= 0)
                throw new ArgumentException("Deposit amount must be positive.");

            Balance += amount;
        }

        public void Withdraw(decimal amount)
        {
            if (amount <= 0)
                throw new ArgumentException("Withdrawal amount must be positive.");

            if (amount > Balance)
                throw new InvalidOperationException("Insufficient funds for this withdrawal.");

            Balance -= amount;
        }
    }
}