using System;
using System.Collections.Generic;

namespace DAL
{
    [Serializable]
    public class Category
    {
        private string _categoryName;
        public string CategoryName
        {
            get => _categoryName;
            set => _categoryName = Validator.ValidateString(value);
        }
        
        private string _categoryDescription;
        public string CategoryDescription
        {
            get => _categoryDescription;
            set => _categoryDescription = Validator.ValidateString(value);
        }

        private List<Invoice> _invoices = new();
        public List<Invoice> Invoices
        {
            get => _invoices;
            set => _invoices = value;
        }
        
        public Category() {}

        public Category(string categoryName, string categoryDescription)
        {
            CategoryName = categoryName;
            CategoryDescription = categoryDescription;
        }
    }
}