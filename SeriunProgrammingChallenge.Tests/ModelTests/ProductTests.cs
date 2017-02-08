using NUnit.Framework;
using SeriunProgrammingChallenge.Console.Models;

namespace SeriunProgrammingChallenge.Tests.ModelTests
{
    public class ProductTests
    {
        [Test]
        public void ProductSuccessfullyConstructed()
        {
            var product = new Product("1234", "a nice product", 50.0m);

            Assert.AreEqual("1234", product.ItemCode);
            Assert.AreEqual("a nice product", product.Description);
            Assert.AreEqual(50.0m, product.Price);
        }

        [Test]
        public void ProductNotEqualOperatorTest()
        {
            var left = new Product(string.Empty, "Groovi Plate Set Frilly Circles ", 2.5m);
            var right = new Product("GROFL40081", "Groovi Plate Set Frilly Circles ", 14.99m);

            Assert.IsTrue(left != right);
            Assert.IsTrue(right != left);
            Assert.IsTrue(left != null);
            Assert.IsTrue(null != right);
            Assert.IsFalse(null != null);
        }

        [Test]
        public void ProductEqualTest()
        {
            var left = new Product(string.Empty, "Craft Consortium Decoupage Papers (Spring / Summer 2016)", 2.5m);
            var right = new Product(string.Empty, "Craft Consortium Decoupage Papers (Spring / Summer 2016)", 2.5m);

            Assert.AreEqual(left, right);
            Assert.AreEqual(right, left);
        }

        [Test]
        public void ProductNotEqualTest()
        {
            var left = new Product(string.Empty, "Groovi Plate Set Frilly Circles ", 2.5m);
            var right = new Product("GROFL40081", "Groovi Plate Set Frilly Circles ", 14.99m);

            Assert.AreNotEqual(left, right);
            Assert.AreNotEqual(right, left);
        }

        [Test]
        public void ProductEqualsTest()
        {
            var left = new Product(string.Empty, "Craft Consortium Decoupage Papers (Spring / Summer 2016)", 2.5m);
            var right = new Product(string.Empty, "Craft Consortium Decoupage Papers (Spring / Summer 2016)", 2.5m);

            Assert.True(left.Equals(right));
            Assert.True(right.Equals(left));
        }

        [Test]
        public void ProductNotEqualsTest()
        {
            var left = new Product(string.Empty, "Groovi Plate Set Frilly Circles ", 2.5m);
            var right = new Product("GROFL40081", "Groovi Plate Set Frilly Circles ", 14.99m);

            Assert.False(left.Equals(right));
            Assert.False(right.Equals(left));
        }

        [Test]
        public void ProductEqualityTest()
        {
            var left = new Product(string.Empty, "Craft Consortium Decoupage Papers (Spring / Summer 2016)", 2.5m);
            var right = new Product(string.Empty, "Craft Consortium Decoupage Papers (Spring / Summer 2016)", 2.5m);

            Assert.True(left == right);
            Assert.True(right == left);
        }

        [Test]
        public void ProductEqualsAsObjectTest()
        {
            var left = (object)new Product(string.Empty, "Craft Consortium Decoupage Papers (Spring / Summer 2016)", 2.5m);
            var right = (object)new Product(string.Empty, "Craft Consortium Decoupage Papers (Spring / Summer 2016)", 2.5m);

            Assert.False(left == right);
            Assert.False(right == left);
        }

        [Test]
        public void ProductNotEqualityTest()
        {
            var left = new Product(string.Empty, "Groovi Plate Set Frilly Circles ", 2.5m);
            var right = new Product("GROFL40081", "Groovi Plate Set Frilly Circles ", 14.99m);

            Assert.True(left != right);
            Assert.True(right != left);
        }
    }
}
