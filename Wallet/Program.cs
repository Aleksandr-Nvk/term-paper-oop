using System;
using System.Linq;
using BLL;
using DAL;
using PL;

namespace Wallet
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            if (args.Length < 1)
            {
                Console.WriteLine("You need to enter the account name as the first argument of the executable!");
                Console.WriteLine("NOTE: if no account is registered with the specified name, a new one will be created!");
                return;
            }

            var inputAccountName = args.First();
            AccountManager accountManager;
            Account account;
            if (AccountManager.AccountExists(inputAccountName))
            {
                account = AccountManager.GetAccount(inputAccountName);
                accountManager = new AccountManager(account);
            }
            else
            {
                Console.WriteLine("Account with such a name doesn't exist. Let's create a new one.");

                var accountName = Input.InputName();
                var age = Input.InputAge();
                var description = Input.InputDescription();

                account = AccountManager.CreateAccount(accountName, age, description);
                accountManager = new AccountManager(account);
                accountManager.SaveAccount();
            }

            Console.WriteLine($"Hello, {account.AccountName}!");

            var accountView = new AccountView(accountManager, account);
            accountView.ShowAccountView();
        }
    }
}