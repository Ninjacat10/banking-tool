// File: Services/BankingService.cs
using System;
using System.Collections.Generic;
using BankingSystem.Models;
using BankingSystem.Repositories;

namespace BankingSystem.Services
{
    public class BankingService
    {
        private readonly IAccountRepository _repository;
        private readonly List<Account> _accounts;

        // Dependency Inversion: We pass in the interface, not the concrete JSON class
        public BankingService(IAccountRepository repository)
        {
            _repository = repository;
            _accounts = _repository.LoadAll(); 
        }

        public void CreateAccount(string number, string holder, decimal initialBalance)
        {
            if (FindAccount(number) != null)
                throw new InvalidOperationException("An account with this number already exists.");

            Account newAccount = new Account(number, holder, initialBalance);
            _accounts.Add(newAccount);
            _repository.SaveAll(_accounts); // Persist data immediately
        }

        public Account FindAccount(string accountNumber)
        {
            foreach (var account in _accounts)
            {
                if (account.AccountNumber == accountNumber)
                    return account;
            }
            return null;
        }

        public void ExecuteDeposit(string accountNumber, decimal amount)
        {
            Account account = FindAccount(accountNumber);
            if (account == null)
                throw new KeyNotFoundException("Account not found.");

            account.Deposit(amount);
            _repository.SaveAll(_accounts);
        }

        public void ExecuteWithdrawal(string accountNumber, decimal amount)
        {
            Account account = FindAccount(accountNumber);
            if (account == null)
                throw new KeyNotFoundException("Account not found.");

            account.Withdraw(amount);
            _repository.SaveAll(_accounts);
        }
    }
}