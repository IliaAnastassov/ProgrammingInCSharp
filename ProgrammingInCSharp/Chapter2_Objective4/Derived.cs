namespace Chapter2_Objective4
{
    using System;

    public class Derived : Base
    {
        public new void Execute()
        {
            Console.WriteLine("Derived.Execute");
        }
    }
}
