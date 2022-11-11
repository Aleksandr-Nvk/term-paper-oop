using System;
using BLL;
using DAL;
using NUnit.Framework;

namespace Tests
{
    public class CategoryManagerTest
    {
        private CategoryManager _categoryManager;
        private Category _category;
        private Invoice _invoice;
        
        [SetUp]
        public void Setup()
        {
            _category = new Category("Name", "Description");
            _invoice = new Invoice(100, DateTime.Now);
            _categoryManager = new CategoryManager(_category);
        }

        [Test]
        public void TestCategoryManagerUpdateCategory()
        {
            var expectedName = "New name";
            var expectedDescription = "New description";
            _categoryManager.UpdateCategory(expectedName, expectedDescription);
            Assert.AreEqual(_category.CategoryName, expectedName);
            Assert.AreEqual(_category.CategoryDescription, expectedDescription);
        }
        
        [Test]
        public void TestCategoryManagerAddInvoice()
        {
            _categoryManager.AddInvoice(_invoice.Sum, _invoice.Date);
            Assert.AreEqual(_category.Invoices[0].Sum, _invoice.Sum);
            Assert.AreEqual(_category.Invoices[0].Date, _invoice.Date);
        }
    }
}