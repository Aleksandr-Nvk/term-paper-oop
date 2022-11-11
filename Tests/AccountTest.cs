using System;
using DAL;
using NUnit.Framework;

namespace Tests
{
    public class AccountTest
    {
        [SetUp]
        public void Setup() { }

        [Test]
        public void TestAccountValidation()
        {
            Assert.Catch<ArgumentException>(() => new Account("", 10, "Text"));
            Assert.Catch<ArgumentException>(() => new Account(null, 10, "Text"));
            Assert.Catch<ArgumentException>(() => new Account("Joe", -20, "Text"));
            Assert.Catch<ArgumentException>(() => new Account("Joe", 10, ""));
            Assert.Catch<ArgumentException>(() => new Account("Joe", 10, null));
        }
    }
}