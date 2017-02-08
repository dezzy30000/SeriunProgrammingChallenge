using System;

namespace SeriunProgrammingChallenge.Console.Models
{
    public class Transaction
    {
        private readonly int _quantity;
        private readonly Product _product;
        private readonly DateTime _timestamp;

        public Transaction(DateTime timestamp, Product product, int quantity)
        {
            _product = product;
            _quantity = quantity;
            _timestamp = timestamp;
        }

        public Product Product { get { return _product; } }
        public int Quantity { get { return _quantity; } }
        public DateTime Timestamp { get { return _timestamp; } }
    }
}
