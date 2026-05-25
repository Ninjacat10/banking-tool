// File: Program.cs
using BankingSystem.Repositories;
using BankingSystem.Services;
using BankingSystem.UI;

namespace BankingSystem
{
    class Program
    {
        static void Main(string[] args)
        {
            // Compose our architecture dependencies from bottom up
            IAccountRepository repository = new JsonAccountRepository("accounts.json");
            BankingService bankingService = new BankingService(repository);
            ConsoleMenu menu = new ConsoleMenu(bankingService);

            // Fire up the interface
            menu.Run();
        }
    }
}