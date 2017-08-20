using System;

namespace Chapter4_Objective5
{
    class Program
    {
        static void Main(string[] args)
        {

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
