namespace Chapter2_Objective4
{
    using System;

    public class MyImplementation : IMyInterface
    {
        private const int SonstantValue = 12;
        private static readonly int StaticReadonlyValue;
        private static int staticValue;
        private readonly int readOnlyValue = 47;
        private int value;

        static MyImplementation()
        {
            StaticReadonlyValue = 12;
            staticValue = 99;
        }

        public MyImplementation()
        {
            this.value = 66;
        }

        public MyImplementation(int value)
        {
            this.value = value;
        }

        private MyImplementation(int valueOne, int valueTwo)
        {
            this.value = valueOne;
            this.readOnlyValue = valueTwo;
        }

        public event EventHandler ResultRetreived;

        public event EventHandler CalculationPerformed;

        public int Value
        {
            get
            {
                return this.value;
            }

            set
            {
                this.value = value;
            }
        }

        public int this[string index]
        {
            get
            {
                return 66;
            }
        }

        public string GetResult()
        {
            return "My result";
        }
    }
}
