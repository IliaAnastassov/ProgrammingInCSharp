namespace Chapter4_Objective3
{
    using System;
    using System.Linq;

    public class Program
    {
        public static void Main(string[] args)
        {
            UseLinqQuery();
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
