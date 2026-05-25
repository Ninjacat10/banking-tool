// File: Repositories/IAccountRepository.cs
using System.Collections.Generic;
using BankingSystem.Models;

namespace BankingSystem.Repositories
{
    public interface IAccountRepository
    {
        List<Account> LoadAll();
        void SaveAll(List<Account> accounts);
    }
}