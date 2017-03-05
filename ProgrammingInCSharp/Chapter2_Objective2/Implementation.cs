namespace Chapter2_Objective2
{
    using System;

    public class Implementation : IInterfaceA
    {
        void IInterfaceA.InterfaceMethod()
        {
            Console.WriteLine("Explicitly implemented method");
        }
    }
}
