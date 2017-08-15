using System;
using System.Runtime.Serialization;

namespace Chapter4_Objective4
{
    [Serializable]
    public class Person
    {
        [OptionalField]
        private int id;

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public int Age { get; set; }

        [OnSerializing]
        internal void OnSerializing(StreamingContext context)
        {
            Console.WriteLine("OnSerializing..");
        }

        [OnSerialized]
        internal void OnSerialized(StreamingContext context)
        {
            Console.WriteLine("OnSerialized..");
        }

        [OnDeserializing]
        internal void OnDeserializing(StreamingContext context)
        {
            Console.WriteLine("OnDeserializing..");
        }

        [OnDeserialized]
        internal void OnDeserialized(StreamingContext context)
        {
            Console.WriteLine("OnDeserialized..");
        }

        public override string ToString()
        {
            return $"{FirstName} {LastName}, {Age}";
        }
    }
}
