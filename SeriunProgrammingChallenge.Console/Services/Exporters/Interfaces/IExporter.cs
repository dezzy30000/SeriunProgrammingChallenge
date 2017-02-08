using SeriunProgrammingChallenge.Console.Models;

namespace SeriunProgrammingChallenge.Console.Services.Exporters.Interfaces
{
    public interface IExporter
    {
        bool Export(Order[] orders, Product[] products);
    }
}
