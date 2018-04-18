using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Exam
{
    [Serializable]
    [XmlInclude(typeof(MaterialOrderItem))]
    [XmlInclude(typeof(ServiceOrderItem))]
    [KnownType(typeof(MaterialOrderItem))]
    [KnownType(typeof(ServiceOrderItem))]
    [DataContract()]
    public class OrderItem {

        [DataMember]
        [XmlElement]
        public Item Item { get; }

        [DataMember]
        [XmlElement]
        public int Quantity { get; }

        [DataMember]
        [XmlElement]
        public bool IsTaxable { get; }

        public OrderItem(Item item, int quantity)
        {
            Item = item;
            Quantity = quantity;
            if (this is Interfaces.ITaxable) IsTaxable = true;
        }

        // Had to add this for serialzation to XML
        public OrderItem() { }
    }
}
