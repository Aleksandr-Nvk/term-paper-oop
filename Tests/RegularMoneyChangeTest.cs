using System;
using DAL;
using NUnit.Framework;

namespace Tests
{
    public class RegularMoneyChangeTest
    {
        [SetUp]
        public void Setup() { }

        [Test]
        public void TestRegularMoneyChangeValidation()
        {
            Assert.DoesNotThrow(() => new RegularMoneyChange(100, DateTime.Now, DateTime.Now));
            Assert.Catch<ArgumentException>(() => new RegularMoneyChange(1_000_000_000, DateTime.Now, DateTime.Now));
            Assert.Catch<ArgumentException>(() => new RegularMoneyChange(-1_000_000_000, DateTime.Now, DateTime.Now));
        }
    }
}