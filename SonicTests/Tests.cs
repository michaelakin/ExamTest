using Exam;
using Exam.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Collections;
using DeepCopy;

namespace ExamTests
{
    [TestClass]
    public class SonicTests
    {
        [TestMethod]
        public void CreateServiceOrderItem()
        {
            var item = new ServiceOrderItem(new Item(1, "One", 10.00m), 1);
            Assert.IsNotNull(item);
        }

        [TestMethod]
        public void CreateMaterialOrderItem()
        {
            var item = new MaterialOrderItem(new Item(2, "Two", 10.02m), 1);
            Assert.IsNotNull(item);
            Assert.IsTrue(item is ITaxable);
        }

        [TestMethod]
        public void IsMaterialOrderItemTaxable()
        {
            var item = new MaterialOrderItem(new Item(3, "Three", 10.03m), 1);
            Assert.IsTrue(item is ITaxable);
        }

        [TestMethod]
        public void CalculateTotal()
        {
            // http://www.softschools.com/math/topics/rounding_to_the_nearest_hundredth/
            var item1 = new ServiceOrderItem(new Item(1, "One", 10.00m), 1);
            var item2 = new MaterialOrderItem(new Item(2, "Two", 10.02m), 2);
            OrderItem[] items = { item1, item2 };
            var order = new Order(items);
            var total = order.GetOrderTotal(0.0825m);
            Assert.IsTrue(total == 31.69m);
        }

        [TestMethod]
        public void CalculateTotalSingleItem()
        {
            var item = new MaterialOrderItem(new Item(2, "Two", 2.00m), 1);
            OrderItem[] items = { item };
            var order = new Order(items);
            var total = order.GetOrderTotal(0.0825m);
            Assert.IsTrue(total == 2.17m);
        }

        [TestMethod]
        public void AreItemsSorted()
        {
            var item1 = new ServiceOrderItem(new Item(1, "One", 10.00m), 1);
            var item2 = new MaterialOrderItem(new Item(2, "Two", 10.02m), 1);
            var item3 = new MaterialOrderItem(new Item(3, "Three", 10.03m), 1);
            var item4 = new MaterialOrderItem(new Item(4, "four", 10.04m), 2);

            OrderItem[] items = { item1, item2, item3, item4 };

            var order = new Order(items);
            var itemsList = (IList<Item>)order.GetItems();
            Assert.IsTrue(itemsList[0].GetName() == "four");
            Assert.IsTrue(itemsList[1].GetName() == "One");
            Assert.IsTrue(itemsList[2].GetName() == "Three");
            Assert.IsTrue(itemsList[3].GetName() == "Two");
        }

        [TestMethod]
        public void IsOrderImmutable()
        {
            var item1 = new ServiceOrderItem(new Item(1, "One", 10.00m), 1);
            var item2 = new MaterialOrderItem(new Item(2, "Two", 10.02m), 1);
            OrderItem[] items = { item1, item2 };

            var order = new Order(items);
            var orderitem = order.OrderItems;
            var test1 = orderitem[0];

            // the follwoing not allowed by compiler.
            //test1.Quantity = 3;
            //test1.Item = new Item(3, "three", 3);
            //order.OrderItems = items;
            order.OrderItems[0] = items[1];
            Assert.AreNotEqual(order.OrderItems[0].Item.Name, order.OrderItems[1].Item.Name);
        }

        [TestMethod]
        public void SerializeOrderToJson()
        {
            var item1 = new ServiceOrderItem(new Item(1, "One", 10.00m), 1);
            var item2 = new MaterialOrderItem(new Item(2, "Two", 10.02m), 4);
            OrderItem[] items = { item1, item2 };
            var order = new Order(items);
            var serializationSettings = new JsonSerializerSettings();
            serializationSettings.TypeNameHandling = TypeNameHandling.Auto;

            var orderJson = JsonConvert.SerializeObject(order, serializationSettings);

            Debug.WriteLine(orderJson);

            var orderDeserialized = JsonConvert.DeserializeObject<Order>(orderJson, serializationSettings);
            Assert.IsTrue(orderDeserialized.OrderItems.Length > 0);
            Assert.IsTrue(orderDeserialized.OrderItems[0] is ServiceOrderItem);
            Assert.IsTrue(orderDeserialized.OrderItems[1] is MaterialOrderItem);
            Assert.IsInstanceOfType(order.OrderItems[1], typeof(MaterialOrderItem));
            Assert.IsTrue(orderDeserialized.OrderItems[1] is ITaxable);
            Assert.IsTrue(orderDeserialized.OrderItems[0].Item.Name == item1.Item.Name);
            Assert.IsTrue(orderDeserialized.OrderItems[0].Item.Price == item1.Item.Price);
            Assert.IsTrue(orderDeserialized.OrderItems[0].Quantity == item1.Quantity);
        }

        [TestMethod]
        public void OrderDataIsRequired()
        {
            Assert.ThrowsException<Exception>(() => { new OrderItem(null, 1); });
            Assert.ThrowsException<Exception>(() => { new OrderItem(new Item(1, "1", 1.0m), 0); });
        }

        [TestMethod]
        public void NewAddItemsToHashTable()
        {
            var item1 = new Item(1, "One", 10.00m);
            var item2 = new Item(2, "Two", 10.02m);
            var hash = new Hashtable();

            hash.Add(item1.ToString(),item1);
            hash.Add(item2.ToString(), item2);

            Assert.IsTrue(hash.Contains("1One10.00"));
            Assert.IsTrue(hash.Contains("2Two10.02"));
        }

        //[TestMethod]
        //public void NewUnderstandRounding()
        //{
        //    var round1 = Math.Truncate((10.044m + 0.005m) * 100) / 100m;
        //    var round2 = Math.Round(10.044m, 2, MidpointRounding.AwayFromZero);
        //    Assert.AreEqual(round1, round2);

        //    var round3 = Math.Truncate((10.045m + 0.005m) * 100) / 100m;
        //    var round4 = Math.Round(10.045m, 2, MidpointRounding.AwayFromZero);
        //    Assert.AreEqual(round3, round4);
            
        //    var round5 = Math.Round(10.044m, 2);
        //    var round6 = Math.Round(10.045m, 2);
        //    Assert.AreEqual(round1, round5);
        //    Assert.AreEqual(round3, round6);
        //}

        [TestMethod]
        public void NewDeepCopyTest()
        {
            var item1 = new ServiceOrderItem(new Item(1, "One", 10.00m), 1);
            var item2 = new MaterialOrderItem(new Item(2, "Two", 10.02m), 2);
            OrderItem[] items = { item1, item2 };
            var order = new Order(items);

            var order2 = DeepCopier.Copy<Order>(order);

            //unsafe
            //{
            //    Item* ptr1 = &order.OrderItems[1].Item;
            //    Item* ptr2 = &order2.OrderItems[1].Item;

            //    //Assert.AreNotEqual(ptr1,ptr2);
            //}
            Assert.AreEqual(order.OrderItems[1].Item.Name, order2.OrderItems[1].Item.Name);
            
        }

    }
}
