// File: Repositories/JsonAccountRepository.cs
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using BankingSystem.Models;

namespace BankingSystem.Repositories
{
    public class JsonAccountRepository : IAccountRepository
    {
        private readonly string _filePath;

        public JsonAccountRepository(string filePath = "accounts.json")
        {
            _filePath = filePath;
        }

        public List<Account> LoadAll()
        {
            if (!File.Exists(_filePath))
            {
                return new List<Account>(); // Return an empty list if no data file exists yet
            }

            string jsonString = File.ReadAllText(_filePath);
            
            // Handle empty files safely
            if (string.IsNullOrWhiteSpace(jsonString)) 
                return new List<Account>();

            return JsonSerializer.Deserialize<List<Account>>(jsonString);
        }

        public void SaveAll(List<Account> accounts)
        {
            // WriteIndented makes the JSON file human-readable/pretty
            var options = new JsonSerializerOptions { WriteIndented = true };
            string jsonString = JsonSerializer.Serialize(accounts, options);
            File.WriteAllText(_filePath, jsonString);
        }
    }
}