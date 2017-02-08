using log4net;
using log4net.Core;
using SeriunProgrammingChallenge.Console.Models;
using SeriunProgrammingChallenge.Console.Services.Exporters.Interfaces;
using SeriunProgrammingChallenge.Console.Services.Importers.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace SeriunProgrammingChallenge.Console.Services.Importers
{
    public class CSVImporter : IImporter
    {
        private const int OrderId = 0;
        private const int TransactionDate = 1;
        private const int ItemCode = 2;
        private const int Description = 3;
        private const int Price = 4;
        private const int Quantity = 5;

        private readonly IExporter[] _exporters;
        private readonly IEnumerable<string> _datalines;
        private readonly ILog _logger = LogManager.GetLogger(typeof(CSVImporter));

        /// <summary>
        /// http://stackoverflow.com/questions/1757065/java-splitting-a-comma-separated-string-but-ignoring-commas-in-quotes
        /// </summary>
        private readonly Regex RegexToSplitACommaSeparatedStringButIgnoringCommasInQuotes = new Regex(",(?=(?:[^\"]*\"[^\"]*\")*[^\"]*$)", RegexOptions.Compiled);

        public CSVImporter(IEnumerable<string> datalines, IExporter[] exporters)
        {
            _datalines = datalines;
            _exporters = exporters;
        }

        public bool Import()
        {
            var orders = new HashSet<Order>();
            var products = new HashSet<Product>();

            foreach (var line in _datalines)
            {
                var segments = RegexToSplitACommaSeparatedStringButIgnoringCommasInQuotes.Split(line);

                var product = new Product(segments[ItemCode], segments[Description], decimal.Parse(segments[Price]));
                var transaction = new Transaction(DateTime.Parse(segments[TransactionDate]), product, int.Parse(segments[Quantity]));

                var order = new Order(segments[OrderId]);

                if (orders.Contains(order))
                {
                    order = orders
                        .Single(storedOrder => storedOrder == order);
                }
                else
                {
                    orders.Add(order);
                }

                order.AppendTransaction(transaction);

                if (!products.Add(product))
                {
                    _logger.Warn($"Duplicate product found. Item code - {product.ItemCode} | Description - {product.Description}");
                }
            }

            return _exporters
                .Any(exporter => exporter.Export(orders.ToArray(), products.ToArray()));
        }
    }
}
