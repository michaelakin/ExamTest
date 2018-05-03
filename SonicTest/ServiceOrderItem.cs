using Exam.Interfaces;

namespace Exam
{
    public class ServiceOrderItem : OrderItem
    {
        public ServiceOrderItem(Item item, int quantity, IOrderItemCalc orderItemCalc = null) : base(item, quantity)
        {
            if (orderItemCalc == null)
                orderItemCalc = new ServiceOrderItemCalc();
            else
                this.orderItemCalc = orderItemCalc;

        }

        private ServiceOrderItem() : base()
        {
        }
    }
}