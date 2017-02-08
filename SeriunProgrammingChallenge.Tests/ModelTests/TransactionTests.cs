using NUnit.Framework;
using SeriunProgrammingChallenge.Console.Models;
using System;

namespace SeriunProgrammingChallenge.Tests.ModelTests
{
    public class TransactionTests
    {
        [Test]
        public void TransactionSuccessfullyConstructed()
        {
            var timestamp = DateTime.Now;
            var transaction = new Transaction(timestamp, new Product(null, null, 10.0m), 10);

            Assert.AreEqual(timestamp, transaction.Timestamp);
            Assert.AreEqual(10, transaction.Quantity);
            Assert.IsNotNull(transaction.Product);
        }
    }
}
