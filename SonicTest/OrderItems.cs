using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Exam
{
    [Serializable]
    [DataContract]
    public class OrderItem {

        [DataMember]
        public Item Item { get; }
        [DataMember]
        public int Quantity { get; }

        public OrderItem(Item item, int quantity)
        {
            Item = item;
            Quantity = quantity;
        }

        // Had to add this for serialzation to XML
        private OrderItem() { }


    }
}
