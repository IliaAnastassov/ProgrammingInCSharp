namespace Chapter1
{
    using System;
    using System.Threading;

    public class Program
    {
        [ThreadStatic]
        private static int counter;
        private static ThreadLocal<int> managedThreadId = new ThreadLocal<int>(() =>
        {
            return Thread.CurrentThread.ManagedThreadId;
        });

        public static void Main(string[] args)
        {

        }

        private static void UseThreadLocal()
        {
            new Thread(() =>
            {
                for (int i = 0; i < managedThreadId.Value; i++)
                {
                    Console.WriteLine($"Thread A: {i}");
                }
            }).Start();

            new Thread(() =>
            {
                for (int i = 0; i < managedThreadId.Value; i++)
                {
                    Console.WriteLine($"Thread B: {i}");
                }
            }).Start();
        }

        private static void UseThreadStaticField()
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
    }
}
