using System;
using System.Collections.Generic;
using System.Linq;

namespace Chapter4_Objective5
{
    class Program
    {
        static void Main(string[] args)
        {

        }

        private static void UseStack()
        {
            var myStack = new Stack<string>();
            myStack.Push("Shmuley");
            myStack.Push("Fucking");
            myStack.Push("Boteach");
            myStack.Push("Does");
            myStack.Push("!!!");

            while (myStack.Count > 0)
            {
                var item = myStack.Pop();
                Console.Write($"{item} ");
            }

            Console.WriteLine();
        }

        private static void UseQueue()
        {
            var myQueue = new Queue<string>();
            myQueue.Enqueue("Shmuley");
            myQueue.Enqueue("Fucking");
            myQueue.Enqueue("Boteach");
            myQueue.Enqueue("Does");
            myQueue.Enqueue("!!!");

            while (myQueue.Count > 0)
            {
                var item = myQueue.Dequeue();
                Console.Write($"{item} ");
            }

            Console.WriteLine();
        }

        private static void UseHashSet()
        {
            var evenSet = new HashSet<int>();
            var oddSet = new HashSet<int>();

            for (int i = 1; i <= 20; i++)
            {
                if (i % 2 == 0)
                {
                    evenSet.Add(i);
                }
                else
                {
                    oddSet.Add(i);
                }
            }

            DisplayCollection(evenSet);
            DisplayCollection(oddSet);

            evenSet.UnionWith(oddSet);
            var ordered = evenSet.OrderBy(x => x);
            DisplayCollection(ordered);
        }

        private static void DisplayCollection(IEnumerable<int> collection)
        {
            Console.Write("{");
            foreach (var item in collection)
            {
                Console.Write($" {item}");
            }
            Console.WriteLine(" }");
        }

        private static void UseDictionary()
        {
            var shmuley = new Person { Id = 1, Name = "Shmuley Boteach" };
            var dildo = new Person { Id = 2, Name = "Dildo Schwaggins" };
            var pena = new Person { Id = 3, Name = "Pena Gesheva" };

            var persons = new Dictionary<int, Person>();
            persons.Add(shmuley.Id, shmuley);
            persons.Add(dildo.Id, dildo);
            persons.Add(pena.Id, pena);

            foreach (var pair in persons)
            {
                Console.WriteLine($"{pair.Key} : {pair.Value.Name}");
            }

            persons[0] = new Person { Id = 4, Name = "Jane Doe" };

            Console.WriteLine(persons[0].Name);

            Person person;
            if (!persons.TryGetValue(5, out person))
            {
                Console.WriteLine("No person with such id");
            }
        }

        private static void UseJaggedArray()
        {
            int[][] jaggedArray =
                        {
                new int[] { 1, 2, 3, 4 },
                new int[] { 5, 6, 7 },
                new int[] { 8, 9 }
            };

            foreach (var subArray in jaggedArray)
            {
                foreach (var number in subArray)
                {
                    Console.Write($"{number} ");
                }

                Console.WriteLine();
                Console.WriteLine("-------");
            }
        }

        private static void UseTwoDimArray()
        {
            int[,] matrix = new int[3, 2]
            {
                {1 ,2 },
                {3, 4 },
                {5, 6 }
            };

            Console.WriteLine(matrix[0, 0]);
            Console.WriteLine(matrix[0, 1]);
            Console.WriteLine(matrix[1, 0]);
            Console.WriteLine(matrix[1, 1]);
            Console.WriteLine(matrix[2, 0]);
            Console.WriteLine(matrix[2, 1]);
        }

        private static void UseSimpleArray()
        {
            int[] numbers = new int[10];

            for (int i = 0; i < numbers.Length; i++)
            {
                numbers[i] = i;
            }

            foreach (var number in numbers)
            {
                Console.Write($"{number} ");
            }

            Console.WriteLine();
        }
    }
}
