namespace Chapter1
{
    using System;
    using System.Threading;

    public class Program
    {
        public static void ThreadMethod()
        {
            for (int i = 0; i < 10; i++)
            {
                Console.WriteLine($"ThreadProc: {i}");
                Thread.Sleep(0);
            }
        }

        public static void ThreadMethod(object count)
        {
            for (int i = 0; i < (int)count; i++)
            {
                Console.WriteLine($"Parameterized ThreadProc: {i}");
                Thread.Sleep(0);
            }
        }

        [ThreadStatic]
        public static int counter;

        public static void Main(string[] args)
        {
            new Thread(() =>
            {
                for (int i = 0; i < 10; i++)
                {
                    counter++;
                    Console.WriteLine($"Thread A: {counter}");
                    Thread.Sleep(100);
                }
            }).Start();

            new Thread(() =>
            {
                for (int i = 0; i < 10; i++)
                {
                    counter++;
                    Console.WriteLine($"Thread B: {counter}");
                    Thread.Sleep(100);
                }
            }).Start();
        }
    }
}
