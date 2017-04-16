namespace Chapter3_Objective5
{
    using System;
    using System.Diagnostics;
    using System.IO;
    using System.Text;

    public class Program
    {
        public static void Main(string[] args)
        {

        }

        private static void IncrementCounters()
        {
            if (CreatePerformanceCounters())
            {
                Console.WriteLine("Created performance counters");
                Console.WriteLine("Please restart application");
                return;
            }

            var totalOperationsCounter = new PerformanceCounter("MyCategory", "# operations executed", false);
            var operationsPerSecondCounter = new PerformanceCounter("MyCategory", "# operations / sec", false);

            totalOperationsCounter.Increment();
            operationsPerSecondCounter.Increment();
        }

        private static bool CreatePerformanceCounters()
        {
            if (!PerformanceCounterCategory.Exists("MyCategory"))
            {
                var counters = new CounterCreationDataCollection
                {
                    new CounterCreationData(
                        "# operations executed",
                        "Total number of operations executed",
                        PerformanceCounterType.NumberOfItems32),
                    new CounterCreationData(
                        "# operations / sec",
                        "Number of operations executed per second",
                        PerformanceCounterType.RateOfCountsPerSecond32)
                };

                PerformanceCounterCategory.Create(
                    "MyCategory",
                    "Sample category",
                    PerformanceCounterCategoryType.SingleInstance,
                    counters);

                return true;
            }

            return false;
        }

        private static void ReadDataFromPerformanceCounter()
        {
            using (var counter = new PerformanceCounter("Memory", "Available bytes"))
            {
                var text = "Available memory: ";
                Console.Write(text);

                do
                {
                    while (!Console.KeyAvailable)
                    {
                        Console.Write(counter.RawValue);
                        Console.SetCursorPosition(text.Length, Console.CursorTop);
                    }
                } while (Console.ReadKey(true).Key != ConsoleKey.Escape);

                Console.WriteLine();
            }
        }

        private static void MeasurePerformance()
        {
            var iterations = 200000;
            var timer = new Stopwatch();

            timer.Start();
            StringConcatanation(iterations);
            timer.Stop();

            Console.WriteLine(timer.Elapsed);

            timer.Reset();
            timer.Start();
            StringBuilderAppend(iterations);
            timer.Stop();

            Console.WriteLine(timer.Elapsed);
        }

        private static void StringBuilderAppend(int iterations)
        {
            var sb = new StringBuilder();
            for (int i = 0; i < iterations; i++)
            {
                sb.Append("C");
            }

            var result = sb.ToString();
        }

        private static void StringConcatanation(int iterations)
        {
            var result = string.Empty;

            for (int i = 0; i < iterations; i++)
            {
                result += "C";
            }
        }

        private static void SubscribeToChangesInEventLog()
        {
            var applicationLog = new EventLog("Application", ".", "testEventLogEvent");
            applicationLog.EntryWritten += (sender, e) =>
            {
                Console.WriteLine(e.Entry.Message);
            };

            applicationLog.EnableRaisingEvents = true;
            applicationLog.WriteEntry("Test message", EventLogEntryType.Information);
        }

        private static void ReadFromEventLog()
        {
            var log = new EventLog("MyNewLog");

            Console.WriteLine($"Total entries: {log.Entries.Count}");
            var last = log.Entries[log.Entries.Count - 1];
            Console.WriteLine($"Index: {last.Index}");
            Console.WriteLine($"Source: {last.Source}");
            Console.WriteLine($"Type: {last.EntryType}");
            Console.WriteLine($"Time: {last.TimeWritten}");
            Console.WriteLine($"Message: {last.Message}");
        }

        /// <summary>
        /// This method should only be called when running Visual Studio as Administrator.
        /// </summary>
        private static void WriteToEventLog()
        {
            if (!EventLog.SourceExists("MySource"))
            {
                EventLog.CreateEventSource("MySource", "MyNewLog");
                Console.WriteLine("Event Source created!");
                Console.WriteLine("Please restart the application...");
                return;
            }

            var myLog = new EventLog();
            myLog.Source = "MySource";
            myLog.WriteEntry("Log event!");
            Console.WriteLine("Event log written!");
        }

        private static void ConfigureTraceListener()
        {
            var outputFile = File.Create("tracefile.txt");
            var textListener = new TextWriterTraceListener(outputFile);

            var traceSource = new TraceSource("MyTraceSource", SourceLevels.All);
            traceSource.Listeners.Clear();
            traceSource.Listeners.Add(textListener);

            traceSource.TraceInformation("Trace output");

            traceSource.Flush();
            traceSource.Close();
        }

        private static void UseTraceSourceClass()
        {
            var traceSource = new TraceSource("MyTraceSource", SourceLevels.All);
            traceSource.TraceInformation("Tracing application...");
            traceSource.TraceEvent(TraceEventType.Critical, 0, "Critical trace");
            traceSource.TraceData(TraceEventType.Information, 1, new object[] { "a", "b", "c" });

            traceSource.Flush();
            traceSource.Close();
        }

        private static void UseDebugClass()
        {
            Debug.WriteLine("Starting Application");
            var number = 1;
            number++;
            Debug.Assert(number == 3);
            Debug.WriteLineIf(number > 0, "number is greater than 0");
        }
    }
}
