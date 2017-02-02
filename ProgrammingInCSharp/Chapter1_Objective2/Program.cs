namespace Chapter1_Objective2
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    public class Program
    {
        static void Main(string[] args)
        {

        }

        private static void CreateDeadlock()
        {
            object lockA = new object();
            object lockB = new object();

            var up = Task.Run(() =>
            {
                lock (lockA)
                {
                    Thread.Sleep(1000);
                    lock (lockB)
                    {
                        Console.WriteLine("Locked A and B");
                    }
                }
            });

            lock (lockB)
            {
                lock (lockA)
                {
                    Console.WriteLine("Locked A and B");
                }
            }

            up.Wait();
        }

        private static void UseLock()
        {
            var n = 0;
            object locker = new object();

            var up = Task.Run(() =>
            {
                for (int i = 0; i < 1000000; i++)
                {
                    lock (locker)
                        n++;
                }
            });

            for (int i = 0; i < 1000000; i++)
            {
                lock (locker)
                    n--;
            }

            up.Wait();
            Console.WriteLine(n);
        }
    }
}
