using NUnit.Framework;
using SeriunProgrammingChallenge.Console.Models;
using System;

namespace SeriunProgrammingChallenge.Tests.ModelTests
{
    public class OrderTests
    {
        [Test]
        public void OrderIdIsCorrectlySet()
        {
            var order = new Order("1234");

            Assert.AreEqual("1234", order.Id);
        }

        [Test]
        public void OrderTransactionInitialised()
        {
            Assert.IsNotNull(new Order(null).Transactions);
        }

        [Test]
        public void OrderTransactionAppendedSuccessfully()
        {
            var order = new Order(null);

            order.AppendTransaction(new Transaction(DateTime.Now, null, 0));

            Assert.AreEqual(1, order.Transactions.Length);
        }

        [Test]
        public void OrderNotEqualOperatorTest()
        {
            var left = new Order("MS55999");
            var right = new Order("MS55000");

            Assert.IsTrue(left != right);
            Assert.IsTrue(right != left);
            Assert.IsTrue(left != null);
            Assert.IsTrue(null != right);
            Assert.IsFalse(null != null);
        }

        [Test]
        public void OrderEqualTest()
        {
            var left = new Order("MS55999");
            var right = new Order("MS55999");

            Assert.AreEqual(left, right);
            Assert.AreEqual(right, left);
        }

        [Test]
        public void OrderNotEqualTest()
        {
            var left = new Order("MS55999");
            var right = new Order("MS55000");

            Assert.AreNotEqual(left, right);
            Assert.AreNotEqual(right, left);
        }

        [Test]
        public void OrderEqualsTest()
        {
            var left = new Order("MS55999");
            var right = new Order("MS55999");

            Assert.True(left.Equals(right));
            Assert.True(right.Equals(left));
        }

        [Test]
        public void OrderNotEqualsTest()
        {
            var left = new Order("MS55999");
            var right = new Order("MS55000");

            Assert.False(left.Equals(right));
            Assert.False(right.Equals(left));
        }

        [Test]
        public void OrderEqualityTest()
        {
            var left = new Order("MS55999");
            var right = new Order("MS55999");

            Assert.True(left == right);
            Assert.True(right == left);
        }

        [Test]
        public void OrderEqualsAsObjectTest()
        {
            var left = (object)new Order("MS55999");
            var right = (object)new Order("MS55999");

            Assert.False(left == right);
            Assert.False(right == left);
        }

        [Test]
        public void OrderNotEqualityTest()
        {
            var left = new Order("MS55999");
            var right = new Order("MS55000");

            Assert.True(left != right);
            Assert.True(right != left);
        }

        [Test]
        public void OrderTotalIsSuccessfullyCalculated()
        {
            var order = new Order(null);

            order.AppendTransaction(new Transaction(DateTime.Now, new Product("1", "1", 43.19m), 13));
            order.AppendTransaction(new Transaction(DateTime.Now, new Product("2", "2", 72.24m), 26));
            order.AppendTransaction(new Transaction(DateTime.Now, new Product("3", "3", 0.89m), 19));

            Assert.AreEqual(2456.62m, order.Total());
        }

        [Test]
        public void OrderTotalIsSuccessfullyCalculatedWhenDiscountForParticularProductIsApplied()
        {
            var order = new Order(null);

            order.AppendTransaction(new Transaction(DateTime.Now, new Product("1", "1", 43.19m), 13));
            order.AppendTransaction(new Transaction(DateTime.Now, new Product("2", "2", 72.24m), 26));
            order.AppendTransaction(new Transaction(DateTime.Now, new Product("3", "3", 0.89m), 19));

            Assert.AreEqual(2080.972m, order.Total("2", 20));
        }
    }
}
