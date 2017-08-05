using System;
using System.Xml.Serialization;

namespace Chapter4_Objective4
{
    [Serializable]
    public class Product
    {
        [XmlAttribute]
        public int ID { get; set; }

        public decimal Price { get; set; }

        public string Description { get; set; }
    }
}