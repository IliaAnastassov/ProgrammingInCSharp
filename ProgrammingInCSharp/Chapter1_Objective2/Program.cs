namespace Chapter1_Objective2
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    public class Program
    {
        public static void Main(string[] args)
        {
        }

        private static void SetTaskTimeout()
        {
            var longRunning = Task.Run(() =>
            {
                Thread.Sleep(20000);
            });

            var index = Task.WaitAny(new[] { longRunning }, 5000);

            if (index == 1)
            {
                Console.WriteLine("Task timed out");
            }
        }

        private static void ThrowOperationCancelledException()
        {
            var tokenSource = new CancellationTokenSource();
            var token = tokenSource.Token;

            var task = Task.Run(() =>
            {
                while (!token.IsCancellationRequested)
                {
                    Console.WriteLine("*");
                    Thread.Sleep(500);
                }

                token.ThrowIfCancellationRequested();
            }, token);

            try
            {
                Console.WriteLine("Press enter to stop the task");
                Console.ReadLine();

                tokenSource.Cancel();
                task.Wait();
            }
            catch (AggregateException e)
            {
                Console.WriteLine($"I like beeeeewbs! {e.InnerExceptions[0].Message}");
            }

            Console.WriteLine("Press enter to stop the application");
            Console.ReadLine();
        }

        private static void UseCancellationToken()
        {
            var cancellationSource = new CancellationTokenSource();
            var token = cancellationSource.Token;

            var task = Task.Run(() =>
            {
                while (!token.IsCancellationRequested)
                {
                    Console.WriteLine("*");
                    Thread.Sleep(500);
                }
            }, token);

            Console.WriteLine("Press enter to stop the task");
            Console.ReadLine();
            cancellationSource.Cancel();

            Console.WriteLine("Press enter to stop the application");
            Console.ReadLine();
        }

        private static void UseInterlocked()
        {
            var n = 0;

            var up = Task.Run(() =>
            {
                for (int i = 0; i < 1000000; i++)
                {
                    Interlocked.Increment(ref n);
                }
            });

            for (int i = 0; i < 1000000; i++)
            {
                Interlocked.Decrement(ref n);
            }

            up.Wait();
            Console.WriteLine(n);
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
