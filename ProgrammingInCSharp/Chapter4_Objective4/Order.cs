using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Chapter4_Objective4
{
    [Serializable]
    public class Order
    {
        [NonSerialized]
        private bool isDirty;

        [XmlAttribute]
        public int ID { get; set; }

        [XmlIgnore]
        public bool IsDirty
        {
            get
            {
                return isDirty;
            }
            set
            {
                isDirty = value;
            }
        }

        [XmlArray("Lines")]
        [XmlArrayItem("OrderLine")]
        public List<OrderLine> OrderLines { get; set; }
    }
}
