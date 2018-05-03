using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Exam.Interfaces;

namespace Exam
{
    public class ServiceOrderItemCalc : IOrderItemCalc
    {
        public decimal Calc(OrderItem orderItem, decimal taxRate)
        {
            var netTotal = orderItem.Quantity * orderItem.Item.GetPrice();
            var totalWithTax = Math.Round(netTotal, 2, MidpointRounding.AwayFromZero);
            return totalWithTax;
        }
    }
}
