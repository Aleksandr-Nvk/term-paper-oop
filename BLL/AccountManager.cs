using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using DAL;

namespace BLL
{
    public class AccountManager
    {
        private readonly Account _account;

        public AccountManager(Account account)
        {
            _account = account;
        }
        
        public static Account CreateAccount(string accountName, int age, string description)
        {
            File.Create(GetDataFileNameByAccountName(accountName)).Close();
            return new Account(accountName, age, description);
        }

        public static Account GetAccount(string accountName)
        {
            var json = File.ReadAllText(GetDataFileNameByAccountName(accountName));
            return JsonSerializer.Deserialize<Account>(json);
        }

        public List<Account> GetAllAccounts()
        {
            var accountDataFiles = Directory.GetFiles(".", "*.data.json");
            return accountDataFiles
                .Select(fileName => GetAccount(fileName.Replace(".data.json", "")))
                .ToList();
        }

        public static bool AccountExists(string accountName)
        {
            return File.Exists(GetDataFileNameByAccountName(accountName));
        }

        public void SaveAccount(Account account)
        {
            var json = JsonSerializer.Serialize(account);
            File.WriteAllText(GetDataFileNameByAccountName(account.AccountName), json);
        }
        
        public void SaveAccount()
        {
            var json = JsonSerializer.Serialize(_account);
            File.WriteAllText(GetDataFileNameByAccountName(_account.AccountName), json);
        }

        public string TransferMoney(Account accountTo, int moneyToTransfer)
        {
            if (_account.AccountName == accountTo.AccountName)
            {
                return "Can't send money to yourself!";
            }
            
            if (accountTo.Balance + moneyToTransfer < 100_000_000 && _account.Balance - moneyToTransfer >= 0)
            {
                accountTo.Balance += moneyToTransfer;
                _account.Balance -= moneyToTransfer;
                SaveAccount(accountTo);
                return "Success!";
            }

            return "Can't send that much";
        }

        public string ChangeBalance(int changeSum)
        {
            if (_account.Balance + changeSum < 0 || _account.Balance + changeSum >= 100_000_000)
            {
                return "Can't process that money";
            }

            _account.Balance += changeSum;
            return "Success!";
        }

        public void UpdateAccount(string accountName, int age, string description, int balance)
        {
            _account.AccountName = accountName;
            _account.Age = age;
            _account.Description = description;
            _account.Balance = balance;
        }

        public string AddCategoryToAccount(string categoryName, string categoryDescription)
        {
            if (_account.Categories.All(category => category.CategoryName != categoryName))
            {
                var category = new Category(categoryName, categoryDescription);
                _account.Categories.Add(category);
                return "Success";
            }

            return "Such a category exists already!";
        } 
        
        public string RemoveCategoryFromAccount(string categoryName)
        {
            if (_account.Categories.Any(category => category.CategoryName == categoryName))
            {
                var category = _account.Categories.Find(category => category.CategoryName == categoryName);
                _account.Categories.Remove(category);

                return "Success!";
            }

            return "No such category exists!";
        }

        public string AddRegularMoneyChange(int sum, DateTime beginPeriod, DateTime endPeriod)
        {
            if (_account.Balance + sum < 0 || _account.Balance + sum > 100_000_000)
            {
                return "Can't have that sum";
            }

            if (sum > 0)
            {
                _account.Earnings.Add(new RegularMoneyChange(sum, beginPeriod, endPeriod));
            }
            else if (sum < 0)
            {
                _account.Expenses.Add(new RegularMoneyChange(sum, beginPeriod, endPeriod));
            }

            return "Success!";
        }
        
        public Category GetCategory(string categoryName)
        {
            return _account.Categories.FirstOrDefault(category => category.CategoryName == categoryName);
        }
        
        public bool CategoryExists(string categoryName)
        {
            return _account.Categories.Any(category => category.CategoryName == categoryName);
        }

        public int GetIncomeByDates(DateTime beginDate, DateTime endDate)
        {
            var sum = 0;
            sum += _account.Earnings
                .Where(earning => earning.BeginPeriod >= beginDate && earning.EndPeriod <= endDate)
                .Select(earning => earning.Sum * ((beginDate.Year - endDate.Year) * 12 + beginDate.Month - endDate.Month))
                .Sum();
            sum -= _account.Expenses
                .Where(expense => expense.BeginPeriod >= beginDate && expense.EndPeriod <= endDate)
                .Select(expense => expense.Sum * ((beginDate.Year - endDate.Year) * 12 + beginDate.Month - endDate.Month))
                .Sum();
            foreach (var category in _account.Categories)
            {
                sum += category.Invoices
                    .Select(invoice => invoice.Sum)
                    .Sum();
            }

            return sum;
        }
        
        private static string GetDataFileNameByAccountName(string accountName) => $"{accountName}.data.json";
    }
}