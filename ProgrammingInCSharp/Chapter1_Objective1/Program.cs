namespace Chapter1_Objective1
{
    using System;
    using System.Collections.Concurrent;
    using System.Linq;
    using System.Net.Http;
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
        }

        private static void UseConcurrentDictionary()
        {
            var dict = new ConcurrentDictionary<string, int>();

            if (dict.TryAdd("sixty six", 66))
            {
                Console.WriteLine("Added");
            }

            if (dict.TryUpdate("sixty six", 69, 66))
            {
                Console.WriteLine("Updated 66 to 69");
            }

            dict["sixty six"] = 66;

            var result = dict.AddOrUpdate("sixty six", 32, (s, i) => i++);
            var resultTwo = dict.GetOrAdd("sixty six", 12);
        }

        private static void UseConcurrentQueue()
        {
            var queue = new ConcurrentQueue<int>();
            queue.Enqueue(47);

            int result;
            if (queue.TryDequeue(out result))
            {
                Console.WriteLine(result);
            }
        }

        private static void UseConcurrentStack()
        {
            var stack = new ConcurrentStack<int>();

            stack.Push(47);

            int result;
            if (stack.TryPop(out result))
            {
                Console.WriteLine(result);
            }

            stack.PushRange(new int[] { 2, 3, 4 });

            var nums = new int[2];
            stack.TryPopRange(nums);

            foreach (var num in nums)
            {
                Console.WriteLine(num);
            }
        }

        private static void UseConcurrentBag()
        {
            var bag = new ConcurrentBag<int>();

            Task.Run(() =>
            {
                bag.Add(5);
                Thread.Sleep(1000);
                bag.Add(9);
            });

            Task.Run(() =>
            {
                foreach (var number in bag)
                {
                    Console.WriteLine(number);
                }
            }).Wait();
        }

        private static void UseBlockingCollection()
        {
            var collection = new BlockingCollection<string>();

            var read = Task.Run(() =>
            {
                foreach (var item in collection.GetConsumingEnumerable())
                {
                    Console.WriteLine(item);
                }
            });

            var write = Task.Run(() =>
            {
                while (true)
                {
                    var input = Console.ReadLine();
                    if (string.IsNullOrWhiteSpace(input))
                    {
                        break;
                    }

                    collection.Add(input);
                }
            });

            write.Wait();
        }

        private static void CatchingAggregateException()
        {
            var numbers = Enumerable.Range(0, 20);

            try
            {
                var evenNumbers = numbers.AsParallel()
                                         .Where(n => IsEven(n));

                evenNumbers.ForAll(n => Console.WriteLine(n));
            }
            catch (AggregateException e)
            {
                Console.WriteLine($"There were {e.InnerExceptions.Count} exceptions");
            }
        }

        private static bool IsEven(int number)
        {
            if (number % 10 == 0)
            {
                throw new ArgumentException("number");
            }

            return number % 2 == 0;
        }

        private static void UseForAll()
        {
            var numbers = Enumerable.Range(0, 20);
            var oddNumbers = numbers.AsParallel()
                                    .Where(n => n % 2 != 0);

            oddNumbers.ForAll(n => Console.WriteLine(n));
        }

        private static void UseAsParallel()
        {
            var numbers = Enumerable.Range(0, 100);

            var evenNumbers = numbers.AsParallel()
                                     .AsOrdered()
                                     .Where(n => n % 2 == 0 && n != 0)
                                     .AsSequential();

            foreach (var number in evenNumbers.Take(10))
            {
                Console.WriteLine($"{number}");
            }
        }

        private static async Task<string> DownloadContent()
        {
            using (var client = new HttpClient())
            {
                var result = await client.GetStringAsync("http://www.microsoft.com");
                return result;
            }
        }

        private static void UseParallelBreak()
        {
            var counter = 0;

            var result = Parallel.For(0, 1000, (int i, ParallelLoopState loopState) =>
            {
                counter++;

                if (i == 500)
                {
                    Console.WriteLine($"Breaking loop after {counter}...");
                    loopState.Break();
                }

                return;
            });

            Console.WriteLine(result.IsCompleted);
            Console.WriteLine(result.LowestBreakIteration);
        }

        private static void UseParallelFor()
        {
            Parallel.For(0, 10, i =>
            {
                Console.WriteLine($"A:{i}");
                Thread.Sleep(1000);
            });

            var numbers = Enumerable.Range(0, 10);

            Parallel.ForEach(numbers, i =>
            {
                Console.WriteLine($"B:{i}");
                Thread.Sleep(1000);
            });
        }

        private static void UseTaskWaitAny()
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

        private static void ThreadMethod()
        {
            for (int i = 0; i < 10; i++)
            {
                Console.WriteLine($"ThreadProc: {i}");
                Thread.Sleep(0);
            }
        }

        private static void ThreadMethod(object count)
        {
            for (int i = 0; i < (int)count; i++)
            {
                Console.WriteLine($"Parameterized ThreadProc: {i}");
                Thread.Sleep(0);
            }
        }
    }
}
