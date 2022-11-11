using System;
using DAL;
using NUnit.Framework;

namespace Tests
{
    public class ValidatorTest
    {
        [SetUp]
        public void Setup() { }

        [Test]
        public void TestValidator()
        {
            Assert.Catch<ArgumentException>(() => Validator.ValidateInteger(10, 100, 200));
            Assert.Catch<ArgumentException>(() => Validator.ValidateString(""));
            Assert.Catch<ArgumentException>(() => Validator.ValidateString(null));
            Assert.AreEqual(0, Validator.ValidateInteger(0, -1, 1));
        }
    }
}