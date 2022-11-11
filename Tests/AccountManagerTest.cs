using System;
using System.Reflection;
using System.Reflection.Emit;
using BLL;
using DAL;
using NUnit.Framework;
using NUnit.Framework.Internal;

namespace Tests
{
    public class AccountManagerTest
    {
        private AccountManager _accountManager;
        private Account _account;
        
        [SetUp]
        public void Setup()
        {
            _account = new Account("Name", 1, "Text");
            _accountManager = new AccountManager(_account);
        }

        [Test]
        public void TestAccountManagerUpdateAccount()
        {
            var expectedName = "Ron";
            var expectedAge = 84;
            var expectedDescription = "Ron's account";
            var expectedBalance = 1000;
            _accountManager.UpdateAccount(expectedName, expectedAge, expectedDescription, expectedBalance);
            
            Assert.AreEqual(_account.AccountName, expectedName);
            Assert.AreEqual(_account.Age, expectedAge);
            Assert.AreEqual(_account.Description, expectedDescription);
            Assert.AreEqual(_account.Balance, expectedBalance);
        }

        [Test]
        public void TestAccountManagerGetDataFileNameByAccountName()
        {
            var method = _accountManager.GetType().GetMethod("GetDataFileNameByAccountName", 
                BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static);
            var result = (string)method.Invoke(_accountManager, new object[] {"Data"});
            Assert.AreEqual(result, "Data.data.json");
        }

        [Test]
        public void TestAccountManagerAddCategoryToAccount()
        {
            var expectedName = "Category";
            var expectedDescription = "Text";
            _accountManager.AddCategoryToAccount(expectedName, expectedDescription);
            Assert.AreEqual(_accountManager.GetCategory(expectedName).CategoryName, expectedName);
            Assert.AreEqual(_accountManager.GetCategory(expectedName).CategoryDescription, expectedDescription);
        }
        
        [Test]
        public void TestAccountManagerRemoveCategoryFromAccount()
        {
            var name = "CategoryName";
            _accountManager.AddCategoryToAccount(name, "Text");
            var categoryCount = _account.Categories.Count;
            _accountManager.RemoveCategoryFromAccount(name);
            Assert.AreEqual(_account.Categories.Count + 1, categoryCount);
        }

        [Test]
        public void TestAccountManagerChangeBalance()
        {
            Assert.AreEqual(_accountManager.ChangeBalance(10), "Success!");
            Assert.AreEqual(_accountManager.ChangeBalance(-1_000_000_000), "Can't process that money");
        }
        
        [Test]
        public void TestAccountManagerAccountExists()
        {
            _accountManager.UpdateAccount("Name", 12, "text", 0);
            _accountManager.SaveAccount();
            Assert.IsTrue(AccountManager.AccountExists("Name"));
        }
        
        [Test]
        public void TestAccountManagerCreateAccount()
        {
            var expectedName = "Alex";
            var expectedAge = 32;
            var expectedDescription = "Text";
            var account = AccountManager.CreateAccount(expectedName, expectedAge, expectedDescription);
            Assert.AreEqual(account.AccountName, expectedName);
            Assert.AreEqual(account.Age, expectedAge);
            Assert.AreEqual(account.Description, expectedDescription);
        }
        
        [Test]
        public void TestAccountManagerTransferMoney()
        {
            var anotherAccount = new Account("Another", 12, "Text");
            Assert.AreEqual(_accountManager.TransferMoney(_account, 100), "Can't send money to yourself!");
            Assert.AreEqual(_accountManager.TransferMoney(anotherAccount, 1_000_000_000), "Can't send that much");
            Assert.AreEqual(_accountManager.TransferMoney(anotherAccount, 0), "Success!");
        }

        [Test]
        public void TestAccountManagerAddRegularMoneyChange()
        {
            var result = _accountManager.AddRegularMoneyChange(100, DateTime.Now, DateTime.Now);
            Assert.AreEqual(result, "Success!");
            result = _accountManager.AddRegularMoneyChange(-1_000_000_000, DateTime.Now, DateTime.Now);
            Assert.AreEqual(result, "Can't have that sum");
        }
        
        [Test]
        public void TestAccountManagerGetIncomeByDates()
        {
            Assert.DoesNotThrow(() => _accountManager.GetIncomeByDates(DateTime.Now, DateTime.Now));
        }
        
        [Test]
        public void TestAccountManagerGetAccount()
        {
            Assert.DoesNotThrow(() => AccountManager.GetAccount("Name"));
        }
    }
}