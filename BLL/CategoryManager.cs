using System;
using DAL;

namespace BLL
{
    public class CategoryManager
    {
        private readonly Category _category;
        public CategoryManager(Category category)
        {
            _category = category;
        }
        
        public void UpdateCategory(string categoryName, string categoryDescription)
        {
            _category.CategoryName = categoryName;
            _category.CategoryDescription = categoryDescription;
        }

        public void AddInvoice(int sum, DateTime date)
        {
            _category.Invoices.Add(new Invoice(sum, date));
        }
    }
}