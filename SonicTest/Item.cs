using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace Exam
{
    /// <summary>
    /// Represents a part or service that can be sold.
    ///
    /// Care should be taken to ensure that this class is immutable since it
    /// is sent to other systems for processing that should not be able to
    /// change it in any way.
    ///
    /// </summary>;
    //[DataContract]
    public class Item
    {
        //[DataMember(Name="key")]
        private readonly int key;
        //[DataMember(Name = "name")]
        private readonly string name;
        //[DataMember(Name = "price")]
        private readonly float price;
        public Item(int key, string name, float price)
        {
            this.key = key;
            this.name = name;
            this.price = price;
        }

        public int Key { get { return GetKey(); } }
        public int GetKey()
        {
            return key;
        }

        public string GetKeyForHashTable()
        {
            return Key.ToString();
        }

        public string Name { get { return GetName(); } }
        public string GetName()
        {
            return name;
        }
        public float Price {  get { return GetPrice(); } }
        public float GetPrice()
        {
            return price;
        }
    }
}

