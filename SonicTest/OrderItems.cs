using System;

namespace Exam
{
    public class OrderItem
    {
        public Item Item { get; set; }

        public int Quantity { get; }

        public OrderItem(Item item, int quantity)
        {
            if (item == null)
                throw new Exception("Item is Required");
            if (quantity == 0)
                throw new Exception("Quantity is Required");

            Item = item;
            Quantity = quantity;
        }

        // Had to add this for serialzation to XML
        public OrderItem() { }
    }
}
