using System;
using BLL;
using DAL;

namespace PL
{
    public class AccountView
    {
        private readonly AccountManager _accountManager;
        private readonly Account _account;
        
        public AccountView(AccountManager accountManager, Account account)
        {
            _accountManager = accountManager;
            _account = account;
        }
        
        public void ShowAccountView()
        {
            while (true)
            {
                Console.WriteLine("Chose your action code: \n" +
                                  "[0] Show all accounts\n" +
                                  "[1] Show categories\n" +
                                  "[2] Show account info\n" +
                                  "[3] Change account info\n" +
                                  "[4] Add money to balance\n" +
                                  "[5] Remove money from balance\n" +
                                  "[6] Transfer money to another account\n" +
                                  "[7] Add category\n" +
                                  "[8] Remove category\n" +
                                  "[9] Add earnings\n" +
                                  "[A] Add expenses\n" +
                                  "[B] Show earnings and expenses\n" +
                                  "[C] Navigate to category\n" +
                                  "[D] Income by dates\n" +
                                  "[E] Exit\n");
                Console.Write("Your action code: ");
                var key = Console.ReadKey();
                Console.WriteLine();
                
                switch (key.KeyChar)
                {
                    case '0':
                        foreach (var accountEntry in _accountManager.GetAllAccounts())
                        {
                            Console.WriteLine(accountEntry.AccountName);
                        }
                        break;
                    case '1':
                        foreach (var category in _account.Categories)
                        {
                            Console.WriteLine(category.CategoryName);
                        }
                        break;
                    case '2':
                        Console.WriteLine($"Name: {_account.AccountName}");
                        Console.WriteLine($"Age: {_account.Age}");
                        Console.WriteLine($"Description: {_account.Description}");
                        Console.WriteLine($"Balance: ${_account.Balance}");
                        break;
                    case '3':
                        _accountManager.UpdateAccount(Input.InputName(), Input.InputAge(),
                                                      Input.InputDescription(), Input.InputMoney());
                        _accountManager.SaveAccount();
                        Console.WriteLine("Changed account info successfully!");
                        break;
                    case '4':
                        var sumToAdd = Input.InputMoney();
                        var result4 = _accountManager.ChangeBalance(sumToAdd);
                        _accountManager.SaveAccount();
                        Console.WriteLine(result4);
                        break;
                    case '5':
                        var sumToRemove = Input.InputMoney();
                        var result5 = _accountManager.ChangeBalance(-sumToRemove);
                        _accountManager.SaveAccount();
                        Console.WriteLine(result5);
                        break;
                    case '6':
                        Console.WriteLine("Transfer to: ");
                        var receiverAccountName = AccountManager.GetAccount(Input.InputName());
                        var moneyToTransfer = Input.InputMoney();
                        var result6 = _accountManager.TransferMoney(receiverAccountName, moneyToTransfer);
                        _accountManager.SaveAccount();
                        Console.WriteLine(result6);
                        break;
                    case '7':
                        var newCategoryName = Input.InputName();
                        var newCategoryDescription = Input.InputDescription();
                        var result7 =_accountManager.AddCategoryToAccount(newCategoryName, newCategoryDescription);
                        _accountManager.SaveAccount();
                        Console.WriteLine(result7);
                        break;
                    case '8':
                        var removeCategoryName = Input.InputName();
                        var result8 = _accountManager.RemoveCategoryFromAccount(removeCategoryName);
                        _accountManager.SaveAccount();
                        Console.WriteLine(result8);
                        break;
                    case '9':
                        var earningSum = Input.InputMoney();
                        var beginEarningPeriod = Input.InputDate();
                        var endEarningPeriod = Input.InputDate();
                        var result9 = _accountManager.AddRegularMoneyChange(earningSum, beginEarningPeriod, endEarningPeriod);
                        _accountManager.SaveAccount();
                        Console.WriteLine(result9);
                        break;
                    case 'a':
                    case 'A':
                        var expenseSum = Input.InputMoney();
                        var beginExpensePeriod = Input.InputDate();
                        var endExpensePeriod = Input.InputDate();
                        var resultA = _accountManager.AddRegularMoneyChange(expenseSum, beginExpensePeriod, endExpensePeriod);
                        _accountManager.SaveAccount();
                        Console.WriteLine(resultA);
                        break;
                    case 'b':
                    case 'B':
                        foreach (var earning in _account.Earnings)
                        {
                            Console.WriteLine($"+${earning.Sum} per month from {earning.BeginPeriod:dd:MM:yyyy} " +
                                              $"to {earning.EndPeriod:dd:MM:yyyy}");
                        }
                        
                        foreach (var expense in _account.Expenses)
                        {
                            Console.WriteLine($"-${expense.Sum} per month from {expense.BeginPeriod:dd:MM:yyyy} " +
                                              $"to {expense.EndPeriod:dd:MM:yyyy}");
                        }
                        
                        break;
                    case 'c':
                    case 'C':
                        var categoryName = Input.InputName();
                        if (!_accountManager.CategoryExists(categoryName))
                        {
                            Console.WriteLine("No such category exists!");
                            break;
                        }
                        
                        var cat = _accountManager.GetCategory(categoryName);
                        var categoryView = new CategoryView(_accountManager, cat);
                        categoryView.ShowCategoryView();
                        break;
                    case 'd':
                    case 'D':
                        var beginDate = Input.InputDate();
                        var endDate = Input.InputDate();
                        var income = _accountManager.GetIncomeByDates(beginDate, endDate);
                        Console.WriteLine($"{(income > 0 ? "+" : "-")}${Math.Abs(income)} " +
                                          $"from {beginDate:dd:MM:yyyy} to {endDate:dd:MM:yyyy}");
                        break;
                    case 'e':
                    case 'E':
                        _accountManager.SaveAccount();
                        return;
                    default:
                        Console.WriteLine("Unknown command. Try again.");
                        break;
                }
            }
        }
    }
}