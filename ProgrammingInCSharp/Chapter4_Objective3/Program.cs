namespace Chapter4_Objective3
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Text;
    using System.Xml.Linq;

    public class Program
    {
        public static void Main(string[] args)
        {
            // Write code here
        }

        private static void CreateXml()
        {
            var xml = new XElement(
                            "Root",
                            new List<XElement>
                            {
                                new XElement("Child1"),
                                new XElement("Child2"),
                                new XElement("Child3")
                            },
                            new XAttribute("MyAttribute", 66));

            xml.Save("test.xml");
        }

        private static void UseLinqToXml()
        {
            var xml = @"<?xml version=""1.0"" encoding=""utf-8"" ?>
                            <people>
                                <person firstname=""john"" lastname=""doe"">
                                    <contactdetails>
                                        <emailaddress>john@unknown.com</emailaddress>
                                    </contactdetails>
                                </person>
                                <person firstname=""jane"" lastname=""doe"">
                                    <contactdetails>
                                        <emailaddress>jane@unknown.com</emailaddress>
                                        <phonenumber>001122334455</phonenumber>
                                    </contactdetails>
                                </person>
                            </people>";

            var doc = XDocument.Parse(xml);

            var personNames = from p in doc.Descendants("person")
                              select p.Attribute("firstname").Value
                                     + " " + p.Attribute("lastname").Value;

            var namesOfPersons = doc.Descendants("person")
                                    .Select(p => p.Attribute("firstname").Value
                                                + " " + p.Attribute("lastname").Value);

            foreach (var name in personNames)
            {
                Console.WriteLine(name);
            }

            Console.WriteLine();

            var personsWithPhones = from p in doc.Descendants("person")
                                    where p.Descendants("phonenumber").Any()
                                    let name = p.Attribute("firstname").Value
                                               + " " + p.Attribute("lastname").Value
                                    orderby name
                                    select name;

            var personsHavingPhones = doc.Descendants("person")
                                         .Where(p => p.Descendants("phonenumber").Any())
                                         .Select(p => p.Attribute("firstname").Value
                                                      + " " + p.Attribute("lastname").Value)
                                         .OrderBy(n => n);

            foreach (var name in personsHavingPhones)
            {
                Console.WriteLine(name);
            }
        }

        private static void UseMyLinqExtensions()
        {
            var primes = new int[] { 3, 5, 7, 11, 13, 17, 19 };
            var primesGreaterThanTen = primes.MyWhere(p => p > 10).ToArray();
        }

        private static string DecodeWord(string encodedWord)
        {
            var decodedWord = new StringBuilder();
            var charIndexes = encodedWord.Split(new char[] { '-' }, StringSplitOptions.RemoveEmptyEntries).Select(s => int.Parse(s) + 'A' - 1);

            foreach (var index in charIndexes)
            {
                decodedWord.Append(Char.ConvertFromUtf32(index));
            }

            return decodedWord.ToString();
        }

        private static void DisplayPagedOrders()
        {
            var orders = GetCarts().Union(GetCarts())
                                   .SelectMany(c => c.Orders)
                                   .ToList();

            var pageSize = 4;
            var currentPage = 2;

            var pagedOrders = orders.Skip((currentPage - 1) * pageSize)
                                    .Take(pageSize);

            foreach (var order in pagedOrders)
            {
                Console.WriteLine($"{order.Product.Description}: {order.Amount}");
            }
        }

        private static void DisplayItemsSoldCount()
        {
            var carts = GetCarts();
            var methodProductAmounts = from c in carts
                                       from o in c.Orders
                                       group o by o.Product into p
                                       select new
                                       {
                                           Product = p.Key,
                                           Amount = p.Sum(x => x.Amount)
                                       };

            var queryProductAmounts = carts.SelectMany(c => c.Orders.GroupBy(o => o.Product)
                                                                    .Select(p => new
                                                                    {
                                                                        Product = p.Key,
                                                                        Amount = p.Sum(x => x.Amount)
                                                                    }));

            foreach (var productAmount in queryProductAmounts)
            {
                Console.WriteLine($"{productAmount.Product.Description}: {productAmount.Amount}");
            }
        }

        private static IEnumerable<Cart> GetCarts()
        {
            return new List<Cart>
            {
                new Cart
                {
                    Orders = new List<Order>
                    {
                        new Order
                        {
                            Amount = 5,
                            Product = new Product { Description = "Coffee", Price = 5.59M }
                        },
                        new Order
                        {
                            Amount = 12,
                            Product = new Product { Description = "Tea", Price = 4.57M }
                        }
                    }
                },
                new Cart
                {
                    Orders = new List<Order>
                    {
                        new Order
                        {
                            Amount = 26,
                            Product = new Product { Description = "Rice", Price = 1.86M }
                        },
                        new Order
                        {
                            Amount = 47,
                            Product = new Product { Description = "Sugar", Price = 0.67M }
                        },
                        new Order
                        {
                            Amount = 9,
                            Product = new Product { Description = "Butter", Price = 3.29M }
                        }
                    }
                }
            };
        }

        private static void DisplayCartData()
        {
            var carts = GetCarts();

            var avgProductPrice = carts.SelectMany(c => c.Orders.Select(o => o.Product)).Average(p => p.Price);
            var avgProductAmount = carts.SelectMany(c => c.Orders.Select(o => o.Amount)).Average();

            Console.WriteLine($"Average product price: \t\t\t{avgProductPrice:C}");
            Console.WriteLine($"Average amount of products per order: \t{avgProductAmount}");
        }

        private static void LinqSelectWithMultipleSources()
        {
            int[] data1 = { 1, 2, 3 };
            int[] data2 = { 4, 5, 6 };

            var result1 = from d1 in data1
                          from d2 in data2
                          select d1 * d2;

            var result2 = data1.SelectMany(d1 => data2.Select(d2 => d1 * d2));

            Console.WriteLine(string.Join(", ", result1));
            Console.WriteLine(string.Join(", ", result2));
        }

        private static void UseLinqOrderBy()
        {
            int[] numbers = { 1, 1, 2, 3, 5, 8, 13, 21, 34 };

            var result = numbers.Where(n => n > 10)
                                .OrderByDescending(n => n)
                                .ToList();

            Console.WriteLine(string.Join(", ", result));
        }

        private static TimeSpan MeasureMethodSyntaxPerformance(int[] numbers)
        {
            var timer = new Stopwatch();
            var query = numbers.Where(n => n % 2 == 0);

            timer.Start();
            var result = query.ToList();
            timer.Stop();

            return timer.Elapsed;
        }

        private static TimeSpan MeasureQuerySyntaxPerformance(int[] numbers)
        {
            var timer = new Stopwatch();
            var query = from n in numbers
                        where n % 2 == 0
                        select n;

            timer.Start();
            var result = query.ToList();
            timer.Stop();

            return timer.Elapsed;
        }

        private static void UseLinqQuery()
        {
            int[] numbers = { 1, 1, 2, 3, 5, 8, 13, 21, 34 };

            var evenNumbers = from n in numbers
                              where n % 2 == 0
                              select n;

            foreach (var number in evenNumbers)
            {
                Console.WriteLine(number);
            }

            var oddNumbers = numbers.Where(n => n % 2 != 0);

            foreach (var number in oddNumbers)
            {
                Console.WriteLine(number);
            }
        }

        private static void CreateAnonymousType()
        {
            var person = new
            {
                FirstName = "Shmuley",
                LastName = "Boteach",
                Age = 66,
            };

            Console.WriteLine(person.GetType().Name);
        }

        private static void UseExtensionMethod()
        {
            var text = "This is some sample, demo text. It serves the purpose of counting some words; out...";

            Console.WriteLine(text.CountWords());
        }

        private static void UseAnonymousMethod()
        {
            Func<int, int> tripler = delegate (int x)
            {
                return x * 3;
            };

            Console.WriteLine(tripler(66));
        }

        /// <summary>
        /// This method does NOT change the string passed as argument!
        /// </summary>
        /// <param name="text"></param>
        private static void ToUpper(string text)
        {
            text = text.ToUpper();
        }

        /// <summary>
        /// This method does change the instance of the Person class passed as argument
        /// </summary>
        /// <param name="person"></param>
        private static void MakePersonEvil(Person person)
        {
            person.FirstName = "Shmuley";
            person.LastName = "Boteach";
            person.Age = 66;
        }
    }
}
