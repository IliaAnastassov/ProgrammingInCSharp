namespace Chapter1_Objective4
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class Program
    {
        public static void Main(string[] args)
        {

        }

        private static void UseExceptionHandler()
        {
            var handler = new ExceptionHandler();

            handler.OnChange += (sender, e) => Console.WriteLine("1st subscriber called");
            handler.OnChange += (sender, e) => { throw new Exception(); };
            handler.OnChange += (sender, e) => { throw new Exception(); };
            handler.OnChange += (sender, e) => { throw new Exception(); };
            handler.OnChange += (sender, e) => Console.WriteLine("5th subscriber called");

            try
            {
                handler.Raise();
            }
            catch (AggregateException ex)
            {
                Console.WriteLine($"{ex.InnerExceptions.Count} exceptions handled");
            }
        }

        private static void UseExceptionRaiser()
        {
            var raiser = new ExceptionRaiser();

            raiser.OnChange += (sender, e) => Console.WriteLine("1st subscriber called");
            raiser.OnChange += (sender, e) => { throw new Exception(); };
            raiser.OnChange += (sender, e) => Console.WriteLine("3rd subscriber called");

            raiser.Raise();
        }

        private static void UseCustomEventAccessor()
        {
            var user = new CustomEventAccessorUser();
            user.OnChange += (sender, e) => Console.WriteLine(e.Value);

            user.Raise();
        }

        private static void CreateAndRaise()
        {
            var user = new EventHandlerUser();
            user.OnChange += (sender, e) => Console.WriteLine($"OnChange raised with value: {e.Value}");

            user.Raise();
        }

        private static void UseActionToExposeEvent()
        {
            var user = new ActionUser();
            user.OnChange += () => Console.WriteLine("First");
            user.OnChange += () => Console.WriteLine("Second");

            user.Raise();
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

    public class MyArgs : EventArgs
    {
        public int Value { get; set; }

        public MyArgs(int value)
        {
            Value = value;
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

    public class EventHandlerUser
    {
        public event EventHandler<MyArgs> OnChange = delegate { };

        public void Raise()
        {
            OnChange(this, new MyArgs(66));
        }
    }

    public class CustomEventAccessorUser
    {
        private event EventHandler<MyArgs> onChange = delegate { };

        public event EventHandler<MyArgs> OnChange
        {
            add
            {
                lock (onChange)
                {
                    onChange += value;
                }
            }

            remove
            {
                lock (onChange)
                {
                    onChange -= value;
                }
            }
        }

        public void Raise()
        {
            onChange(this, new MyArgs(66));
        }
    }

    public class ExceptionRaiser
    {
        public event EventHandler OnChange = delegate { };

        public void Raise()
        {
            OnChange(this, EventArgs.Empty);
        }
    }

    public class ExceptionHandler
    {
        public event EventHandler OnChange = delegate { };

        public void Raise()
        {
            var exceptions = new List<Exception>();

            foreach (var handler in OnChange.GetInvocationList())
            {
                try
                {
                    handler.DynamicInvoke(this, EventArgs.Empty);
                }
                catch (Exception ex)
                {
                    exceptions.Add(ex);
                }
            }

            if (exceptions.Any())
            {
                throw new AggregateException(exceptions);
            }
        }
    }
}
