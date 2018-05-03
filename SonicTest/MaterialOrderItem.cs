using Exam.Interfaces;

namespace Exam
{
    public class MaterialOrderItem : OrderItem, ITaxable
    {

        public MaterialOrderItem(Item item, int quantity, IOrderItemCalc orderItemCalc = null) : base(item, quantity)
        {
            if (orderItemCalc == null)
                orderItemCalc = new MaterialOrderItemCalc();
            else
                this.orderItemCalc = orderItemCalc;
        }

        private MaterialOrderItem() : base()
        {
        }
    }
}
