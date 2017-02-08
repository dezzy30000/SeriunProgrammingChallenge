using SeriunProgrammingChallenge.Console.Services.Exporters;
using SeriunProgrammingChallenge.Console.Services.Importers;
using System.IO;
using System.Linq;

namespace SeriunProgrammingChallenge.Console
{
    class Program
    {
        private const int NumberOfPopularItems = 5;
        private const int DiscountPercentage = 20;
        private const string FileName = "feed.csv";
        private const string DiscountedItem = "ACETHR001";

        static void Main(string[] args)
        {
            var reportsExporter = new ReportsExporter();
            var importer = new CSVImporter(File.ReadLines(FileName).Skip(1), new[] { reportsExporter });

            if (!importer.Import())
            {
                System.Console.WriteLine($"Struggling to import file(s) {FileName}");
                return;
            }

            var reports = reportsExporter.GetReportService();

            System.Console.WriteLine($"Top {NumberOfPopularItems} popular items.");

            foreach (var product in reports.TopMostPopularProducts(NumberOfPopularItems))
            {
                System.Console.WriteLine($"Item Code - {product.ItemCode} | Price - {product.Price:C} | Description - {product.Description}");
            }

            System.Console.WriteLine($"Average order value - {reports.AverageOrderValue()}");
            System.Console.WriteLine();
            System.Console.WriteLine($"Total value of discounts after {DiscountPercentage}% discount for item {DiscountedItem} - {reports.TotalValueOfDiscountsIssuedIfForProduct(DiscountedItem, DiscountPercentage)}");
            System.Console.WriteLine();
            System.Console.WriteLine($"Press any key to exit.");
            System.Console.ReadLine();
        }
    }
}
