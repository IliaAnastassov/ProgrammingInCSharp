namespace Chapter1_Objective4
{
    using System;

    public class Program
    {
        public static void Main(string[] args)
        {

        }

        private static void UseActionToExposeEvent()
        {
            var logger = new ActionUser();
            logger.OnChange += () => Console.WriteLine("First");
            logger.OnChange += () => Console.WriteLine("Second");

            logger.Raise();
        }

        private static void UseMulticastDelegate()
        {
            Del d = MethodOne;
            d += MethodTwo;
            d += MethodThree;
            d();

            var invocationCount = d.GetInvocationList().GetLength(0);
            Console.WriteLine(invocationCount);

            d -= MethodOne;
            d -= MethodThree;
            d();

            invocationCount = d.GetInvocationList().GetLength(0);
            Console.WriteLine(invocationCount);
        }

        public delegate void Del();

        private static void MethodOne()
        {
            Console.WriteLine("Method One");
        }

        private static void MethodTwo()
        {
            Console.WriteLine("Method Two");
        }

        private static void MethodThree()
        {
            Console.WriteLine("Method Three");
        }

        private static void UseDelegate()
        {
            Calculate calc = Add;
            Console.WriteLine(calc(5, 4));

            calc = Multiply;
            Console.WriteLine(calc(5, 4));
        }

        public delegate int Calculate(int x, int y);

        public static int Add(int x, int y)
        {
            return x + y;
        }

        public static int Multiply(int x, int y)
        {
            return x * y;
        }
    }

    public class ActionUser
    {
        public Action OnChange { get; set; }

        public void Raise()
        {
            OnChange?.Invoke();
        }
    }

    public class EventActionUser
    {
        public event Action OnChange = delegate { };

        public void Raise()
        {
            OnChange();
        }
    }
}
