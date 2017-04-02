namespace Chapter3_Objective4
{
    using System;
    using System.Reflection;
    using System.Threading;

    public class Program
    {
        public static void Main(string[] args)
        {

        }

        private static void DisableWarnings()
        {
#pragma warning disable
            if (false)
            {
                Console.WriteLine("Unreachable code");
            }
#pragma warning restore
        }

        private static Assembly LoadAssembly<T>()
        {
#if !WINRT
            var assembly = typeof(T).Assembly;
#else
            var assembly = typeof(T).GetTypeInfo().Assembly;
#endif
            return assembly;
        }

        private static void UsePreprocessorCompilerDirectives()
        {
#if DEBUG
            Console.WriteLine("Debugging...");
#endif
        }

        private static void UseTimerCallback()
        {
            var t = new Timer(TimerCallback, null, 0, 2000);
            Console.ReadLine();
        }

        private static void TimerCallback(object state)
        {
            Console.WriteLine($"In TimerCallback: {DateTime.Now}");
            GC.Collect();
        }
    }
}
