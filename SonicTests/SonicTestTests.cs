using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Exam;
using Exam.Interfaces;
using System.Windows;
using 

namespace ExamTests
{
    [TestClass]
    public class SonicTests
    {
        [TestMethod]
        public void CreateServiceOrderItem()
        {
            var item = new ServiceOrderItem(new Item(1, "One", 10.00F), 1);
            Assert.IsNotNull(item);
        }

        [TestMethod]
        public void CreateMaterialOrderItem()
        {
            var item = new MaterialOrderItem(new Item(2, "Two" , 10.02F), 1);
            Assert.IsNotNull(item);
            Assert.IsTrue(item is ITaxable);
        }

        [TestMethod]
        public void IsMaterialOrderItemTaxable()
        {
            var item = new MaterialOrderItem(new Item(3, "Three", 10.03F), 1);
            Assert.IsTrue(item is ITaxable);
        }

        public void AreItemsSorted()
        {
            var item1 = new ServiceOrderItem(new Item(1, "One", 10.00F), 1);
            var item2 = new MaterialOrderItem(new Item(2, "Two", 10.02F), 1);
            var item3 = new MaterialOrderItem(new Item(3, "Three", 10.03F), 1);
            var item4 = new MaterialOrderItem(new Item(4, "four", 10.04F), 2);

            OrderItem[] items = { item1, item2, (item3 };

            var order = new Order(items);
            var itemsList = order.GetItems();
            var itemArray = itemsList.
            Assert.IsTrue(itemsList[0].)

        }

        public void IsRounding()
        {

        }

        public void SerializeOrder()
        {
            var item1 = new ServiceOrderItem(new Item(1, "One", 10.00F), 1);
            var item2 = new MaterialOrderItem(new Item(2, "Two", 10.02F), 1);
            OrderItem[] items = { item1, item2, (item3 };
            var order = new Order(items);
            var orderJson = 

            clipboard.SetText()
        }

    }
}
