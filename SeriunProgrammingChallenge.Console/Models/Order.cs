using System.Collections.Generic;
using System.Linq;

namespace SeriunProgrammingChallenge.Console.Models
{
    public class Order
    {
        private readonly string _id;
        private readonly List<Transaction> _transactions;
        public Order(string id)
        {
            _id = id;
            _transactions = new List<Transaction>();
        }

        public string Id { get { return _id; } }

        public decimal Total()
        {
            return _transactions
                    .Sum(transaction => transaction.Product.Price * transaction.Quantity);
        }

        public decimal Total(string itemCode, decimal discount)
        {
            return _transactions
                    .Sum(transaction =>
                    {
                        if (transaction.Product.ItemCode == itemCode)
                        {
                            return (transaction.Product.Price - (transaction.Product.Price * (discount / 100))) * transaction.Quantity;
                        }

                        return transaction.Product.Price * transaction.Quantity;
                    });
        }

        public Transaction[] Transactions { get { return _transactions.ToArray(); } }

        public void AppendTransaction(Transaction transaction)
        {
            _transactions.Add(transaction);
        }

        #region Equality overrides

        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }

            var p = obj as Order;

            return p != null && p._id.Equals(_id);
        }

        public bool Equals(Order order)
        {
            return order != null && order._id.Equals(_id);
        }

        public override int GetHashCode()
        {
            return ToString().GetHashCode();
        }

        public override string ToString()
        {
            return $"{_id}";
        }

        public static bool operator ==(Order a, Order b)
        {
            if (ReferenceEquals(a, b))
            {
                return true;
            }

            if (((object)a == null) || ((object)b == null))
            {
                return false;
            }

            return a._id.Equals(b._id);
        }

        public static bool operator !=(Order a, Order b)
        {
            return !(a == b);
        }

        #endregion
    }
}
