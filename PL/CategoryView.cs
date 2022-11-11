using System;
using BLL;
using DAL;

namespace PL
{
    public class CategoryView
    {
        private readonly AccountManager _accountManager;
        private readonly Category _category;
        
        public CategoryView(AccountManager accountManager, Category category)
        {
            _accountManager = accountManager;
            _category = category;
        }

        public void ShowCategoryView()
        {
            while (true)
            {
                Console.WriteLine("Chose your action code: \n" +
                                  "[0] Show all invoices\n" +
                                  "[1] Add earning invoice\n" +
                                  "[2] Add expense invoice\n" +
                                  "[3] Edit category info\n" +
                                  "[4] Exit\n");
                Console.Write("Your action code: ");
                var key = Console.ReadKey();
                Console.WriteLine();

                var categoryManager = new CategoryManager(_category);
                switch (key.KeyChar)
                {
                    case '0':
                        foreach (var invoice in _category.Invoices)
                        {
                            Console.WriteLine($"{(invoice.Sum > 0 ? "+" : "-")}${Math.Abs(invoice.Sum)} " +
                                              $"on {invoice.Date:dd:MM:yyyy}");
                        }
                        
                        break;
                    case '1':
                        var earningSum = Input.InputMoney();
                        var earningDate = Input.InputDate();
                        categoryManager.AddInvoice(earningSum, earningDate);
                        _accountManager.SaveAccount();
                        break;
                    case '2':
                        var expenseSum = Input.InputMoney();
                        var expenseDate = Input.InputDate();
                        categoryManager.AddInvoice(-expenseSum, expenseDate);
                        _accountManager.SaveAccount();
                        break;
                    case '3':
                        var newName = Input.InputName();
                        var newDescription = Input.InputDescription();
                        categoryManager.UpdateCategory(newName, newDescription);
                        _accountManager.SaveAccount();
                        break;
                    case '4':
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