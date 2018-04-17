using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exam
{
    [Serializable()]
    public class OrderItem {

        public Item Item { get; }
        public int Quantity { get; }

        public OrderItem(Item item, int quantity)
        {
            Item = item;
            Quantity = quantity;
        }

    }
}
