using System;
using System.Collections.Generic;

namespace DAL
{
    [Serializable]
    public class Account
    {
        private string _accountName;
        public string AccountName
        {
            get => _accountName;
            set => _accountName = Validator.ValidateString(value);
        }

        private int _age;
        public int Age
        {
            get => _age;
            set => _age = Validator.ValidateInteger(value, 0, 120);
        }
        
        private string _description;
        public string Description
        {
            get => _description;
            set => _description = Validator.ValidateString(value);
        }
        
        private int _balance;
        public int Balance
        {
            get => _balance;
            set => _balance = Validator.ValidateInteger(value, -1, 100_000_000);
        }

        private List<Category> _categories = new();
        public List<Category> Categories
        {
            get => _categories;
            set => _categories = value;
        }
        
        private List<RegularMoneyChange> _expenses = new();
        public List<RegularMoneyChange> Expenses
        {
            get => _expenses;
            set => _expenses = value;
        }
        
        private List<RegularMoneyChange> _earnings = new();
        public List<RegularMoneyChange> Earnings
        {
            get => _earnings;
            set => _earnings = value;
        }
        
        public Account() {}

        public Account(string accountName, int age, string description, int balance = 0)
        {
            AccountName = accountName;
            Age = age;
            Description = description;
            Balance = balance;
        }
    }
}