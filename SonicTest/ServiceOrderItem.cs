using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Exam.Interfaces;

namespace Exam
{
    //[KnownType(typeof(OrderItem))]
    public class ServiceOrderItem : OrderItem
    {
        public ServiceOrderItem(Item item, int quantity) : base(item, quantity)
        {
        }

        private ServiceOrderItem() : base()
        {
        }
    }
}