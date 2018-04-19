using Exam.Interfaces;

namespace Exam
{
    public class MaterialOrderItem : OrderItem, ITaxable
    {
        public MaterialOrderItem(Item item, int quantity) : base(item, quantity)
        {
        }

        private MaterialOrderItem() : base()
        {
        }
    }
}
