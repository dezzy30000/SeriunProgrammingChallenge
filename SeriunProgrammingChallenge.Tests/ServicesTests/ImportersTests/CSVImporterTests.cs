using Moq;
using NUnit.Framework;
using SeriunProgrammingChallenge.Console.Models;
using SeriunProgrammingChallenge.Console.Services.Exporters.Interfaces;
using SeriunProgrammingChallenge.Console.Services.Importers;
using System;
using System.Linq;

namespace SeriunProgrammingChallenge.Tests.ServicesTests.ImportersTests
{
    public class CSVImporterTests
    {
        [Test]
        public void GivenANumberOfSalesTransactionsForASingleOrderWhenImportedThenAListOfOrdersAndProductsShouldBeCorrectlyBuilt()
        {
            var mockExporter = new Mock<IExporter>();
            var exportedOrders = new Order[] { };
            var exportedProducts = new Product[] { };

            mockExporter
                .Setup(exporter => exporter.Export(It.IsNotNull<Order[]>(), It.IsNotNull<Product[]>()))
                .Returns(true)
                .Callback<Order[], Product[]>((orders, products) =>
                {
                    exportedOrders = orders;
                    exportedProducts = products;
                });

            var csvImporter = new CSVImporter(new[]
            {
                "2469,29/05/2016,MS55999,Zig Memory System Wink of Stella Brush Glitter Marker Clear,4.99,1",
                "2469,29/05/2016,MS55000,Zig Memory System Wink of Stella Brush Glitter Marker White,4.99,1",
                "2469,29/05/2016,MS55102,Zig Memory System Wink of Stella Brush Glitter Marker Silver,4.99,1",
                "2469,29/05/2016,MS55101,Zig Memory System Wink of Stella Brush Glitter Marker Gold,4.99,1"
            },
            new[] { mockExporter.Object });

            var wasImportSuccessful = csvImporter.Import();

            var order = exportedOrders.Single();
            var transactions = order.Transactions;

            Assert.IsTrue(wasImportSuccessful);

            Assert.AreEqual("2469", order.Id);
            Assert.AreEqual(4, order.Transactions.Length);

            Assert.AreEqual(new DateTime(2016, 05, 29), transactions[0].Timestamp);
            Assert.AreEqual(1, transactions[0].Quantity);
            Assert.AreEqual("MS55999", transactions[0].Product.ItemCode);
            Assert.AreEqual(4.99m, transactions[0].Product.Price);
            Assert.AreEqual("Zig Memory System Wink of Stella Brush Glitter Marker Clear", transactions[0].Product.Description);

            Assert.AreEqual(new DateTime(2016, 05, 29), transactions[1].Timestamp);
            Assert.AreEqual(1, transactions[1].Quantity);
            Assert.AreEqual("MS55000", transactions[1].Product.ItemCode);
            Assert.AreEqual(4.99m, transactions[1].Product.Price);
            Assert.AreEqual("Zig Memory System Wink of Stella Brush Glitter Marker White", transactions[1].Product.Description);

            Assert.AreEqual(new DateTime(2016, 05, 29), transactions[2].Timestamp);
            Assert.AreEqual(1, transactions[2].Quantity);
            Assert.AreEqual("MS55102", transactions[2].Product.ItemCode);
            Assert.AreEqual(4.99m, transactions[2].Product.Price);
            Assert.AreEqual("Zig Memory System Wink of Stella Brush Glitter Marker Silver", transactions[2].Product.Description);

            Assert.AreEqual(new DateTime(2016, 05, 29), transactions[3].Timestamp);
            Assert.AreEqual(1, transactions[3].Quantity);
            Assert.AreEqual("MS55101", transactions[3].Product.ItemCode);
            Assert.AreEqual(4.99m, transactions[3].Product.Price);
            Assert.AreEqual("Zig Memory System Wink of Stella Brush Glitter Marker Gold", transactions[3].Product.Description);

            Assert.AreEqual(4, exportedProducts.Length);

            Assert.AreEqual("MS55999", exportedProducts[0].ItemCode);
            Assert.AreEqual(4.99m, exportedProducts[0].Price);
            Assert.AreEqual("Zig Memory System Wink of Stella Brush Glitter Marker Clear", exportedProducts[0].Description);

            Assert.AreEqual("MS55000", exportedProducts[1].ItemCode);
            Assert.AreEqual(4.99m, exportedProducts[1].Price);
            Assert.AreEqual("Zig Memory System Wink of Stella Brush Glitter Marker White", exportedProducts[1].Description);

            Assert.AreEqual("MS55102", exportedProducts[2].ItemCode);
            Assert.AreEqual(4.99m, exportedProducts[2].Price);
            Assert.AreEqual("Zig Memory System Wink of Stella Brush Glitter Marker Silver", exportedProducts[2].Description);

            Assert.AreEqual("MS55101", exportedProducts[3].ItemCode);
            Assert.AreEqual(4.99m, exportedProducts[3].Price);
            Assert.AreEqual("Zig Memory System Wink of Stella Brush Glitter Marker Gold", exportedProducts[3].Description);

            mockExporter.VerifyAll();
        }

        [Test]
        public void GivenANumberOfSalesTransactionsForMultipleOrdersWhenImportedThenAListOfOrdersAndProductsShouldBeCorrectlyBuilt()
        {
            var mockExporter = new Mock<IExporter>();
            var exportedOrders = new Order[] { };
            var exportedProducts = new Product[] { };

            mockExporter
                .Setup(exporter => exporter.Export(It.IsNotNull<Order[]>(), It.IsNotNull<Product[]>()))
                .Returns(true)
                .Callback<Order[], Product[]>((orders, products) =>
                {
                    exportedOrders = orders;
                    exportedProducts = products;
                });

            var csvImporter = new CSVImporter(new[]
            {
                "2470,29/05/2016,GROBI4014501,Groovi Baby Plate A6 Large Bird & Branch,4.79,1",
                "2471,29/05/2016,COLLCEM12,Collall Photoglue,2.99,4"
            },
            new[] { mockExporter.Object });

            var wasImportSuccessful = csvImporter.Import();

            Assert.IsTrue(wasImportSuccessful);

            Assert.AreEqual(2, exportedOrders.Length);

            Assert.AreEqual("2470", exportedOrders[0].Id);
            Assert.AreEqual(1, exportedOrders[0].Transactions.Length);

            Assert.AreEqual(new DateTime(2016, 05, 29), exportedOrders[0].Transactions[0].Timestamp);
            Assert.AreEqual(1, exportedOrders[0].Transactions[0].Quantity);
            Assert.AreEqual("GROBI4014501", exportedOrders[0].Transactions[0].Product.ItemCode);
            Assert.AreEqual(4.79m, exportedOrders[0].Transactions[0].Product.Price);
            Assert.AreEqual("Groovi Baby Plate A6 Large Bird & Branch", exportedOrders[0].Transactions[0].Product.Description);

            Assert.AreEqual("2471", exportedOrders[1].Id);
            Assert.AreEqual(1, exportedOrders[1].Transactions.Length);

            Assert.AreEqual(new DateTime(2016, 05, 29), exportedOrders[1].Transactions[0].Timestamp);
            Assert.AreEqual(4, exportedOrders[1].Transactions[0].Quantity);
            Assert.AreEqual("COLLCEM12", exportedOrders[1].Transactions[0].Product.ItemCode);
            Assert.AreEqual(2.99m, exportedOrders[1].Transactions[0].Product.Price);
            Assert.AreEqual("Collall Photoglue", exportedOrders[1].Transactions[0].Product.Description);

            Assert.AreEqual(2, exportedProducts.Length);

            Assert.AreEqual("GROBI4014501", exportedProducts[0].ItemCode);
            Assert.AreEqual(4.79m, exportedProducts[0].Price);
            Assert.AreEqual("Groovi Baby Plate A6 Large Bird & Branch", exportedProducts[0].Description);

            Assert.AreEqual("COLLCEM12", exportedProducts[1].ItemCode);
            Assert.AreEqual(2.99m, exportedProducts[1].Price);
            Assert.AreEqual("Collall Photoglue", exportedProducts[1].Description);

            mockExporter.VerifyAll();
        }

        [Test]
        public void GivenANumberOfSalesTransactionsWithMissingItemCodesForTheSameProductForASingleOrderWhenImportedThenAListOfOrdersAndProductsShouldBeCorrectlyBuilt()
        {
            var mockExporter = new Mock<IExporter>();
            var exportedOrders = new Order[] { };
            var exportedProducts = new Product[] { };

            mockExporter
                .Setup(exporter => exporter.Export(It.IsNotNull<Order[]>(), It.IsNotNull<Product[]>()))
                .Returns(true)
                .Callback<Order[], Product[]>((orders, products) =>
                {
                    exportedOrders = orders;
                    exportedProducts = products;
                });

            var csvImporter = new CSVImporter(new[]
            {
                "2506,05/06/2016,,Memento Dew Drop Dye Ink Pads,1.49,1",
                "2506,05/06/2016,,Memento Dew Drop Dye Ink Pads,1.49,1",
                "2506,05/06/2016,,Brilliance Dew Drop Pigment Pads,1.59,1",
                "2506,05/06/2016,,Brilliance Dew Drop Pigment Pads,1.59,1"
            },
            new[] { mockExporter.Object });

            var wasImportSuccessful = csvImporter.Import();

            var order = exportedOrders.Single();
            var transactions = order.Transactions;

            Assert.IsTrue(wasImportSuccessful);

            Assert.AreEqual("2506", order.Id);
            Assert.AreEqual(4, order.Transactions.Length);

            Assert.AreEqual(new DateTime(2016, 06, 05), transactions[0].Timestamp);
            Assert.AreEqual(1, transactions[0].Quantity);
            Assert.AreEqual(string.Empty, transactions[0].Product.ItemCode);
            Assert.AreEqual(1.49m, transactions[0].Product.Price);
            Assert.AreEqual("Memento Dew Drop Dye Ink Pads", transactions[0].Product.Description);

            Assert.AreEqual(new DateTime(2016, 06, 05), transactions[1].Timestamp);
            Assert.AreEqual(1, transactions[1].Quantity);
            Assert.AreEqual(string.Empty, transactions[1].Product.ItemCode);
            Assert.AreEqual(1.49m, transactions[1].Product.Price);
            Assert.AreEqual("Memento Dew Drop Dye Ink Pads", transactions[1].Product.Description);

            Assert.AreEqual(new DateTime(2016, 06, 05), transactions[2].Timestamp);
            Assert.AreEqual(1, transactions[2].Quantity);
            Assert.AreEqual(string.Empty, transactions[2].Product.ItemCode);
            Assert.AreEqual(1.59m, transactions[2].Product.Price);
            Assert.AreEqual("Brilliance Dew Drop Pigment Pads", transactions[2].Product.Description);

            Assert.AreEqual(new DateTime(2016, 06, 05), transactions[3].Timestamp);
            Assert.AreEqual(1, transactions[3].Quantity);
            Assert.AreEqual(string.Empty, transactions[3].Product.ItemCode);
            Assert.AreEqual(1.59m, transactions[3].Product.Price);
            Assert.AreEqual("Brilliance Dew Drop Pigment Pads", transactions[3].Product.Description);

            Assert.AreEqual(2, exportedProducts.Length);

            Assert.AreEqual(string.Empty, exportedProducts[0].ItemCode);
            Assert.AreEqual(1.49m, exportedProducts[0].Price);
            Assert.AreEqual("Memento Dew Drop Dye Ink Pads", exportedProducts[0].Description);

            Assert.AreEqual(string.Empty, exportedProducts[1].ItemCode);
            Assert.AreEqual(1.59m, exportedProducts[1].Price);
            Assert.AreEqual("Brilliance Dew Drop Pigment Pads", exportedProducts[1].Description);

            mockExporter.VerifyAll();
        }

        [Test]
        public void GivenASalesTransactionWithACommaInTheDescriptionWhenImportedThenAListOfOrdersAndProductsShouldBeCorrectlyBuilt()
        {
            var mockExporter = new Mock<IExporter>();
            var exportedOrders = new Order[] { };
            var exportedProducts = new Product[] { };

            mockExporter
                .Setup(exporter => exporter.Export(It.IsNotNull<Order[]>(), It.IsNotNull<Product[]>()))
                .Returns(true)
                .Callback<Order[], Product[]>((orders, products) =>
                {
                    exportedOrders = orders;
                    exportedProducts = products;
                });

            var csvImporter = new CSVImporter(new[]
            {
                "2508,05/06/2016,GROWO4008609,\"Groovi Border Plate Set Calendar, Relatives & Occasions\",15.99,1"
            },
            new[] { mockExporter.Object });

            var wasImportSuccessful = csvImporter.Import();

            var order = exportedOrders.Single();
            var transactions = order.Transactions;

            Assert.IsTrue(wasImportSuccessful);

            Assert.AreEqual("2508", order.Id);
            Assert.AreEqual(1, order.Transactions.Length);

            Assert.AreEqual(new DateTime(2016, 06, 05), transactions[0].Timestamp);
            Assert.AreEqual(1, transactions[0].Quantity);
            Assert.AreEqual("GROWO4008609", transactions[0].Product.ItemCode);
            Assert.AreEqual(15.99m, transactions[0].Product.Price);
            Assert.AreEqual("\"Groovi Border Plate Set Calendar, Relatives & Occasions\"", transactions[0].Product.Description);

            Assert.AreEqual(1, exportedProducts.Length);

            Assert.AreEqual("GROWO4008609", exportedProducts[0].ItemCode);
            Assert.AreEqual(15.99m, exportedProducts[0].Price);
            Assert.AreEqual("\"Groovi Border Plate Set Calendar, Relatives & Occasions\"", exportedProducts[0].Description);

            mockExporter.VerifyAll();
        }

        [Test]
        public void GivenASalesTransactionWithMultilpeQuotesInTheDescriptionWhenImportedThenAListOfOrdersAndProductsShouldBeCorrectlyBuilt()
        {
            var mockExporter = new Mock<IExporter>();
            var exportedOrders = new Order[] { };
            var exportedProducts = new Product[] { };

            mockExporter
                .Setup(exporter => exporter.Export(It.IsNotNull<Order[]>(), It.IsNotNull<Product[]>()))
                .Returns(true)
                .Callback<Order[], Product[]>((orders, products) =>
                {
                    exportedOrders = orders;
                    exportedProducts = products;
                });

            var csvImporter = new CSVImporter(new[]
            {
                "3466,20/11/2016,GROAC40345,\"Groovi Guard 7\"\" x 7\"\"\",3.79,1"
            },
            new[] { mockExporter.Object });

            var wasImportSuccessful = csvImporter.Import();

            var order = exportedOrders.Single();
            var transactions = order.Transactions;

            Assert.IsTrue(wasImportSuccessful);

            Assert.AreEqual("3466", order.Id);
            Assert.AreEqual(1, order.Transactions.Length);

            Assert.AreEqual(new DateTime(2016, 11, 20), transactions[0].Timestamp);
            Assert.AreEqual(1, transactions[0].Quantity);
            Assert.AreEqual("GROAC40345", transactions[0].Product.ItemCode);
            Assert.AreEqual(3.79m, transactions[0].Product.Price);
            Assert.AreEqual("\"Groovi Guard 7\"\" x 7\"\"\"", transactions[0].Product.Description);

            Assert.AreEqual(1, exportedProducts.Length);

            Assert.AreEqual("GROAC40345", exportedProducts[0].ItemCode);
            Assert.AreEqual(3.79m, exportedProducts[0].Price);
            Assert.AreEqual("\"Groovi Guard 7\"\" x 7\"\"\"", exportedProducts[0].Description);

            mockExporter.VerifyAll();
        }

        [Test]
        public void GivenASalesTransactionWithMultilpeQuotesAndCommaInTheDescriptionWhenImportedThenAListOfOrdersAndProductsShouldBeCorrectlyBuilt()
        {
            var mockExporter = new Mock<IExporter>();
            var exportedOrders = new Order[] { };
            var exportedProducts = new Product[] { };

            mockExporter
                .Setup(exporter => exporter.Export(It.IsNotNull<Order[]>(), It.IsNotNull<Product[]>()))
                .Returns(true)
                .Callback<Order[], Product[]>((orders, products) =>
                {
                    exportedOrders = orders;
                    exportedProducts = products;
                });

            var csvImporter = new CSVImporter(new[]
            {
                "3466,20/11/2016,GROAC40345,\"Groovi, Guard 7\"\" x 7\"\"\",3.79,1"
            },
            new[] { mockExporter.Object });

            var wasImportSuccessful = csvImporter.Import();

            var order = exportedOrders.Single();
            var transactions = order.Transactions;

            Assert.IsTrue(wasImportSuccessful);

            Assert.AreEqual("3466", order.Id);
            Assert.AreEqual(1, order.Transactions.Length);

            Assert.AreEqual(new DateTime(2016, 11, 20), transactions[0].Timestamp);
            Assert.AreEqual(1, transactions[0].Quantity);
            Assert.AreEqual("GROAC40345", transactions[0].Product.ItemCode);
            Assert.AreEqual(3.79m, transactions[0].Product.Price);
            Assert.AreEqual("\"Groovi, Guard 7\"\" x 7\"\"\"", transactions[0].Product.Description);

            Assert.AreEqual(1, exportedProducts.Length);

            Assert.AreEqual("GROAC40345", exportedProducts[0].ItemCode);
            Assert.AreEqual(3.79m, exportedProducts[0].Price);
            Assert.AreEqual("\"Groovi, Guard 7\"\" x 7\"\"\"", exportedProducts[0].Description);

            mockExporter.VerifyAll();
        }
    }
}
