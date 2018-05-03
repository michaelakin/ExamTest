using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exam
{
    public class OrderCalculator : IOrderCalculator
    {
        public decimal CalculateTotal(Order order, decimal taxRate)
        {
            decimal total = 0;
            foreach (var orderitem in order.OrderItems)
            {
                var netTotal = orderitem.Quantity * orderitem.Item.GetPrice();
                if (orderitem is Interfaces.ITaxable)
                {
                    var totalWithTax = Math.Round(netTotal + (netTotal * taxRate), 2, MidpointRounding.AwayFromZero);
                    total += totalWithTax;
                }
                else
                    total += netTotal;
            }
            return total;
        }
    }
}
