using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Exam.Interfaces;

namespace Exam
{
    public class ServiceOrderItem : OrderItem
    {
        public ServiceOrderItem(Item item, int quantity) : base(item, quantity)
        {
        }
    }
}