using System;
using DAL;
using NUnit.Framework;

namespace Tests
{
    public class CategoryTest
    {
        [SetUp]
        public void Setup() { }

        [Test]
        public void TestCategoryValidation()
        {
            Assert.Catch<ArgumentException>(() => new Category("", "Text"));
            Assert.Catch<ArgumentException>(() => new Category(null, "Text"));
            Assert.Catch<ArgumentException>(() => new Category("University", ""));
            Assert.Catch<ArgumentException>(() => new Category("University", null));
        }
    }
}