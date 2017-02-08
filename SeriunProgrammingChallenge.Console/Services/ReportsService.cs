using SeriunProgrammingChallenge.Console.Models;
using SeriunProgrammingChallenge.Console.Services.Exporters.Interfaces;
using System;
using System.Linq;

namespace SeriunProgrammingChallenge.Console.Services
{
    public class ReportsService : IReportsService
    {
        private Order[] _orders;
        private Product[] _products;

        public ReportsService(Order[] orders, Product[] products)
        {
            _orders = orders;
            _products = products;
        }

        public decimal AverageOrderValue(int decimals = 2)
        {
            return Math.Round(_orders
                .Average(order => order.Total()), decimals);
        }

        public Product[] TopMostPopularProducts(int numberOfItems)
        {
            return _orders
               .SelectMany(order => order.Transactions)
               .GroupBy(transaction => transaction.Product)
               .OrderByDescending(group => group.Sum(transaction => transaction.Quantity))
               .Select(group => group.Key)
               .Take(numberOfItems)
               .ToArray();
        }

        public decimal TotalValueOfDiscountsIssuedIfForProduct(string itemCode, decimal percent, int decimals = 2)
        {
            var totalOrderValueWithNoDiscount = _orders.Sum(order => order.Total());
            var totalOrderValueWithDiscount = _orders.Sum(order => order.Total(itemCode, percent));

            return Math.Round(totalOrderValueWithNoDiscount - (totalOrderValueWithNoDiscount - totalOrderValueWithDiscount), decimals);
        }
    }
}
