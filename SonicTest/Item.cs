using System.Runtime.Serialization;

namespace Exam
{
    /// <summary>
    /// Represents a part or service that can be sold.
    ///
    /// Care should be taken to ensure that this class is immutable since it
    /// is sent to other systems for processing that should not be able to
    /// change it in any way.
    ///
    /// </summary>;
    public class Item
    {
        private readonly int key;
        private readonly string name;
        private readonly decimal price;

        public Item(int key, string name, decimal price)
        {
            this.key = key;
            this.name = name;
            this.price = price;
        }

        public int Key { get { return GetKey(); } }
        public int GetKey()
        {
            return key;
        }

        public string Name { get { return GetName(); } }
        public string GetName()
        {
            return name;
        }

        public decimal Price { get { return GetPrice(); } }
        public decimal GetPrice()
        {
            return price;
        }

        // For the future requirement to use in a hash table.
        public override string ToString()
        {
            return key.ToString() + name.ToString() + price.ToString();
        }
    }
}

