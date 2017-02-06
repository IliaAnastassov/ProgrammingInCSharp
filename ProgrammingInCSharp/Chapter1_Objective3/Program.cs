namespace Chapter1_Objective3
{
    using System;
    using System.Diagnostics;

    public class Program
    {
        public static void Main(string[] args)
        {
        }

        private static void UseGoto()
        {
            var x = 0;

            if (x == 0)
            {
                goto customLabel;
            }

            Console.WriteLine("Non Custom");

            customLabel:
            Console.WriteLine("Custom");
        }

        private static void UseForeach()
        {
            foreach (var process in Process.GetProcesses())
            {
                Console.WriteLine($"{process.ProcessName}: {process.Threads.Count}");
            }
        }

        private static void UseNullCoalescingOperator()
        {
            int? x = null;
            int? z = null;
            int y = x ??
                    z ??
                    -1;

            Console.WriteLine(y);
        }

        private static void NonShortCircuiting()
        {
            var x = true;

            if (x & IsTrue())
            {
                Console.WriteLine("Entered if statement");
            }
        }

        private static void ShortCiruiting()
        {
            var x = false;

            if (x && IsTrue())
            {
                Console.WriteLine("Entered if statement");
            }
        }

        private static bool IsTrue()
        {
            Console.WriteLine("Side Effect");
            return true;
        }
    }
}
