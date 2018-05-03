using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exam.Interfaces
{
    public interface IOrderItemCalc
    {
        decimal Calc(OrderItem orderItem, decimal taxRate);
    }
}
