using SeriunProgrammingChallenge.Console.Models;
using SeriunProgrammingChallenge.Console.Services.Exporters.Interfaces;

namespace SeriunProgrammingChallenge.Console.Services.Exporters
{
    public class ReportsExporter : IExporter
    {
        private Order[] _orders;
        private Product[] _products;

        public ReportsExporter()
        {
            _orders = new Order[] { };
            _products = new Product[] { };
        }

        public bool Export(Order[] orders, Product[] products)
        {
            _orders = orders;
            _products = products;

            return true;
        }

        public IReportsService GetReportService()
        {
            return new ReportsService(_orders, _products);
        }
    }
}
