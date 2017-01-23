namespace Chapter1
{
    using System;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

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
            Task<int>[] tasks = new Task<int>[3];

            tasks[0] = Task.Run(() =>
            {
                Thread.Sleep(2000);
                return 1;
            });

            tasks[1] = Task.Run(() =>
            {
                Thread.Sleep(1000);
                return 2;
            });

            tasks[2] = Task.Run(() =>
            {
                Thread.Sleep(3000);
                return 3;
            });

            while (tasks.Length > 0)
            {
                var i = Task.WaitAny(tasks);
                var task = tasks[i];

                Console.WriteLine(task.Result);

                var temp = tasks.ToList();
                temp.RemoveAt(i);
                tasks = temp.ToArray();
            }
        }

        private static void UseTaskWaitAll()
        {
            var tasks = new Task[3];

            tasks[0] = Task.Run(() =>
            {
                Thread.Sleep(1000);
                Console.WriteLine("1");
            });

            tasks[1] = Task.Run(() =>
            {
                Thread.Sleep(1000);
                Console.WriteLine("2");
            });

            tasks[2] = Task.Run(() =>
            {
                Thread.Sleep(1000);
                Console.WriteLine("3");
            });

            Task.WaitAll(tasks);
        }

        private static void UseTaskFactory()
        {
            Task<int[]> parent = Task.Run(() =>
            {
                var result = new int[3];

                var tf = new TaskFactory(TaskCreationOptions.AttachedToParent, TaskContinuationOptions.ExecuteSynchronously);

                tf.StartNew(() => result[0] = 0);
                tf.StartNew(() => result[1] = 1);
                tf.StartNew(() => result[2] = 2);

                return result;
            });

            var finalTask = parent.ContinueWith(p =>
            {
                foreach (var number in p.Result)
                {
                    Console.WriteLine(number);
                }
            });

            finalTask.Wait();
        }

        private static void UseTaskWithChildTasks()
        {
            Task<int[]> parent = Task.Run(() =>
            {
                var results = new int[3];
                new Task(() => results[0] = 0, TaskCreationOptions.AttachedToParent).Start();
                new Task(() => results[1] = 1, TaskCreationOptions.AttachedToParent).Start();
                new Task(() => results[2] = 2, TaskCreationOptions.AttachedToParent).Start();

                return results;
            });

            var finalTask = parent.ContinueWith(parentTask =>
            {
                foreach (var number in parentTask.Result)
                {
                    Console.WriteLine(number);
                }
            });

            finalTask.Wait();
        }

        private static void UseTaskWithContinuation()
        {
            Task<int> task = Task.Run(() =>
            {
                return 66;
            });

            task.ContinueWith((t) =>
            {
                Console.WriteLine("Canceled");
            }, TaskContinuationOptions.OnlyOnCanceled);

            task.ContinueWith((t) =>
            {
                Console.WriteLine("Faulted");
            }, TaskContinuationOptions.OnlyOnFaulted);

            var completedTask = task.ContinueWith((t) =>
            {
                Console.WriteLine("Completed");
            }, TaskContinuationOptions.OnlyOnRanToCompletion);

            completedTask.Wait();
        }

        private static void StartNewTask()
        {
            var t = Task.Run(() =>
            {
                for (int i = 0; i < 50; i++)
                {
                    Console.Write("*");
                }
            });

            var t2 = Task.Run(() =>
            {
                for (int i = 0; i < 50; i++)
                {
                    Console.Write("+");
                }
            });

            t.Wait();
            t2.Wait();

            Console.WriteLine();
        }

        private static void QueueWorkToThreadPool()
        {
            ThreadPool.QueueUserWorkItem((s) =>
            {
                Console.WriteLine("Working on a thread from the thread pool");
            });

            Console.ReadLine();
        }

        private static void UseThreadLocalAttribute()
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
