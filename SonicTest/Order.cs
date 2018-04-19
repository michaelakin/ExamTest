using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Newtonsoft.Json;


namespace Exam
{
    /// <summary>
    /// Represents and Order that contains a collection of items.
    ///
    /// Care should be taken to ensure that this class is immutable since it
    /// is sent to other systems for processing that should not be able
    /// to change it in any way.
    /// </summary>
    public class Order
    {

        [XmlArrayItem(Type = typeof(ServiceOrderItem)),XmlArrayItem(Type = typeof(MaterialOrderItem))]
        private readonly OrderItem[] orderItems;

        public OrderItem[] OrderItems
        {
            get
            {
                // To make immutable, return a copy of the array.
                OrderItem[] orderItemsCopy = new OrderItem[orderItems.Length];
                Array.Copy(orderItems, orderItemsCopy, orderItems.Length);
                return orderItemsCopy;
            }
            set { }
        }

        public Order(OrderItem[] orderItems)
        {
            this.orderItems = orderItems;
        }

        // Had to add this for serialzation to XML
        private Order() { }

        // Returns the total order cost after the tax has been applied
        public decimal GetOrderTotal(decimal taxRate)
        {
            decimal total = 0;
            foreach (var orderitem in orderItems)
            {
                if (orderitem is Interfaces.ITaxable)
                {
                    var netTotal = orderitem.Quantity * orderitem.Item.GetPrice();
                    var totalWithTax = Math.Truncate((netTotal + (netTotal * taxRate) + 0.005m) * 100) / 100m;
                    total += totalWithTax;
                }
                else
                    total += orderitem.Quantity * orderitem.Item.GetPrice();
            }
            return total;
        }

        /**
        * Returns a Collection of all items sorted by item name
        * (case-insensitive).
        *
        * @return ICollection<Item>;
        */
        public ICollection<Item> GetItems()
        {
            var items = orderItems.ToList()
                .OrderBy(o => o.Item.GetName().ToLowerInvariant())
                .Select(s => new Item(s.Item.GetKey(), s.Item.GetName(), s.Item.GetPrice()));
            return (ICollection<Item>)items.ToArray();
        }
    }
}

