using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Exam;
using Exam.Interfaces;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Collections.Generic;
using System.Xml.Serialization;
using System.IO;

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

        [TestMethod]
        public void AreItemsSorted()
        {
            var item1 = new ServiceOrderItem(new Item(1, "One", 10.00F), 1);
            var item2 = new MaterialOrderItem(new Item(2, "Two", 10.02F), 1);
            var item3 = new MaterialOrderItem(new Item(3, "Three", 10.03F), 1);
            var item4 = new MaterialOrderItem(new Item(4, "four", 10.04F), 2);

            OrderItem[] items = { item1, item2, item3, item4 };

            var order = new Order(items);
            var itemsList = (IList<Item>) order.GetItems();
            Assert.IsTrue(itemsList[0].GetName() == "four");
            Assert.IsTrue(itemsList[1].GetName() == "One");
            Assert.IsTrue(itemsList[2].GetName() == "Three");
            Assert.IsTrue(itemsList[3].GetName() == "Two");

        }

        [TestMethod]
        public void IsRounding()
        {

        }

        [TestMethod]
        public void IsOrderImmutable()
        {
            var item1 = new ServiceOrderItem(new Item(1, "One", 10.00F), 1);
            var item2 = new MaterialOrderItem(new Item(2, "Two", 10.02F), 1);
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
            var item1 = new ServiceOrderItem(new Item(1, "One", 10.00F), 1);
            var item2 = new MaterialOrderItem(new Item(2, "Two", 10.02F), 4);
            OrderItem[] items = { item1, item2 };
            var order = new Order(items);
            var serializationSettings = new JsonSerializerSettings();
            serializationSettings.TypeNameHandling = TypeNameHandling.Auto;

            var orderJson = JsonConvert.SerializeObject(order,serializationSettings);
            
            Console.WriteLine(orderJson);

            var orderDeserialized = JsonConvert.DeserializeObject<Order>(orderJson,serializationSettings);
            Assert.IsTrue(orderDeserialized.OrderItems.Length > 0);
            Assert.IsTrue(orderDeserialized.OrderItems[0] is ServiceOrderItem);
            Assert.IsTrue(orderDeserialized.OrderItems[1] is MaterialOrderItem);
            Assert.IsInstanceOfType(order.OrderItems[1], typeof(MaterialOrderItem));
            Assert.IsTrue(orderDeserialized.OrderItems[1] is ITaxable);
            Assert.IsTrue(orderDeserialized.OrderItems[0].IsTaxable == false);
            Assert.IsTrue(orderDeserialized.OrderItems[1].IsTaxable == true);
            Assert.IsTrue(orderDeserialized.OrderItems[0].Item.Name == item1.Item.Name);
            Assert.IsTrue(orderDeserialized.OrderItems[0].Item.Price == item1.Item.Price);
            Assert.IsTrue(orderDeserialized.OrderItems[0].Quantity == item1.Quantity);
        }

        [TestMethod]
        public void SerializeOrderToXml()
        {
            var item1 = new ServiceOrderItem(new Item(1, "One", 10.00F), 1);
            var item2 = new MaterialOrderItem(new Item(2, "Two", 10.02F), 4);
            OrderItem[] items = { item1, item2 };
            var order = new Order(items);

            XmlSerializer ser = new XmlSerializer(typeof(Order));
            TextWriter writer = new StringWriter();
            ser.Serialize(writer, order);
            var orderXml = writer.ToString();
            Console.WriteLine(orderXml);

            TextReader reader = new StringReader(orderXml);
            Order orderDeserialized = (Order) ser.Deserialize(reader);
            Assert.IsTrue(orderDeserialized.OrderItems.Length > 0);
            Assert.IsTrue(orderDeserialized.OrderItems[0].Item.Name == item1.Item.Name);
            Assert.IsTrue(orderDeserialized.OrderItems[0].Item.Price == item1.Item.Price);
            Assert.IsTrue(orderDeserialized.OrderItems[0].Quantity == item1.Quantity);
            Assert.IsTrue(orderDeserialized.OrderItems[0].IsTaxable == false);
            Assert.IsTrue(orderDeserialized.OrderItems[1].IsTaxable == true);
        }
    }
}
