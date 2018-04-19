namespace Exam
{
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