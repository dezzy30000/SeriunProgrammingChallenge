using SeriunProgrammingChallenge.Console.Models;

namespace SeriunProgrammingChallenge.Console.Services.Exporters.Interfaces
{
    public interface IReportsService
    {
        decimal AverageOrderValue(int decimals = 2);

        Product[] TopMostPopularProducts(int numberOfItems);

        decimal TotalValueOfDiscountsIssuedIfForProduct(string itemCode, decimal percent, int decimals = 2);
    }
}
