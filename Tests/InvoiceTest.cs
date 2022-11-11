using System;
using DAL;
using NUnit.Framework;

namespace Tests
{
    public class InvoiceTest
    {
        [SetUp]
        public void Setup() { }

        [Test]
        public void TestInvoiceValidation()
        {
            Assert.DoesNotThrow(() => new Invoice(10, DateTime.Now));
            Assert.Catch<ArgumentException>(() => new Invoice(1_000_000_000, DateTime.Now));
            Assert.Catch<ArgumentException>(() => new Invoice(-1_000_000_000, DateTime.Now));
        }
    }
}