namespace SeriunProgrammingChallenge.Console.Models
{
    public class Product
    {
        private readonly string _itemCode;
        private readonly string _description;
        private readonly decimal _price;

        public Product(string itemCode, string description, decimal price)
        {
            _price = price;
            _itemCode = itemCode;
            _description = description;
        }

        public string ItemCode { get { return _itemCode; } }
        public decimal Price { get { return _price; } }
        public string Description { get { return _description; } }

        #region Equality overrides

        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }

            var p = obj as Product;

            return p != null && p._itemCode.Equals(_itemCode) && p._description.Equals(_description);
        }

        public bool Equals(Product product)
        {
            return (object)product != null && product._itemCode.Equals(_itemCode) && product._description.Equals(_description);
        }

        public override int GetHashCode()
        {
            return ToString().GetHashCode();
        }

        public override string ToString()
        {
            return $"{_itemCode}, {_description}";
        }

        public static bool operator ==(Product a, Product b)
        {
            if (ReferenceEquals(a, b))
            {
                return true;
            }

            if (((object)a == null) || ((object)b == null))
            {
                return false;
            }

            return a._itemCode.Equals(b._itemCode) && a._description.Equals(b._description);
        }

        public static bool operator !=(Product a, Product b)
        {
            return !(a == b);
        }

        #endregion
    }
}
