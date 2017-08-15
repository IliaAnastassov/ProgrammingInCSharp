using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization.Json;
using System.Xml.Serialization;

namespace Chapter4_Objective4
{
    class Program
    {
        static void Main(string[] args)
        {

        }

        private static void UseJsonSerializer()
        {
            var person = new PersonJson
            {
                Id = 66,
                FirstName = "Shmuley",
                LastName = "Boteach"
            };

            using (var stream = new MemoryStream())
            {
                var serializer = new DataContractJsonSerializer(typeof(PersonJson));
                serializer.WriteObject(stream, person);

                stream.Position = 0;
                var reader = new StreamReader(stream);
                Console.WriteLine(reader.ReadToEnd());

                stream.Position = 0;
                var result = serializer.ReadObject(stream);
            }
        }

        private static void UseDataContract()
        {
            var person = new PersonDataContract
            {
                FirstName = "Shmuley",
                LastName = "Boteach",
                Age = 66
            };

            using (var stream = new FileStream("data.xml", FileMode.Create))
            {
                var serializer = new DataContractSerializer(typeof(PersonDataContract));
                serializer.WriteObject(stream, person);
            }

            using (var stream = new FileStream("data.xml", FileMode.Open))
            {
                var serializer = new DataContractSerializer(typeof(PersonDataContract));
                var shmuley = serializer.ReadObject(stream) as PersonDataContract;

                Console.WriteLine($"{shmuley.FirstName} {shmuley.LastName} {shmuley.Age}");
            }
        }

        private static void UseBinarySerialization()
        {
            var randomGuy = new Person
            {
                FirstName = "Shmuley",
                LastName = "Boteach",
                Age = 66
            };

            var formatter = new BinaryFormatter();

            using (var stream = new FileStream("shmuley.bin", FileMode.Create))
            {
                formatter.Serialize(stream, randomGuy);
            }

            using (var stream = new FileStream("shmuley.bin", FileMode.Open))
            {
                var person = formatter.Deserialize(stream);
                Console.WriteLine(person);
            }
        }

        private static void UseXmlSerializationAttributes()
        {
            var serializer = new XmlSerializer(typeof(Order), new Type[] { typeof(VIPOrder) });
            string xml;

            using (var writer = new StringWriter())
            {
                var order = CreateOrder();
                serializer.Serialize(writer, order);
                xml = writer.ToString();
            }

            Console.WriteLine(xml);
            Console.WriteLine();

            using (var reader = new StringReader(xml))
            {
                var order = serializer.Deserialize(reader) as Order;
                Console.WriteLine($"{order} ID={order.ID}");
            }
        }

        private static Order CreateOrder()
        {
            var p1 = new Product { ID = 1, Description = "p1", Price = 6.99M };
            var p2 = new Product { ID = 2, Description = "p2", Price = 2.49M };

            Order order = new VIPOrder
            {
                ID = 4,
                Description = "Order for Shmuley Boteach. Don't you try fucking with that guy.",
                OrderLines = new List<OrderLine>
                {
                    new OrderLine { ID = 5, Product = p1, Amount = 6 },
                    new OrderLine { ID = 6, Product = p2, Amount = 4 }
                }
            };

            return order;
        }

        private static void UseXmlSerialization()
        {
            var serializer = new XmlSerializer(typeof(Person));
            string xml;

            using (var writer = new StringWriter())
            {
                var person = new Person
                {
                    FirstName = "Shmuley",
                    LastName = "Boteach",
                    Age = 66
                };

                serializer.Serialize(writer, person);
                xml = writer.ToString();
            }

            Console.WriteLine(xml);
            Console.WriteLine();

            using (StringReader reader = new StringReader(xml))
            {
                var person = serializer.Deserialize(reader) as Person;
                Console.WriteLine($"{person.FirstName} {person.LastName} is {person.Age} old");
            }
        }
    }
}
