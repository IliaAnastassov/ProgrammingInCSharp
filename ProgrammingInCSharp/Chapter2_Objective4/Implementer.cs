namespace Chapter2_Objective4
{
    using System;

    public class Implementer : IContractA, IContractB
    {
        public void MethodFromA()
        {
            Console.WriteLine("Method A");
        }

        void IContractB.MethodFromB()
        {
            Console.WriteLine("Method B");
        }
    }
}
