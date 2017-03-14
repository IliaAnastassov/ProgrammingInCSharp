namespace Chapter2_Objective6
{
    using System;

    public class Program
    {
        public static void Main(string[] args)
        {

        }

        /// <summary>
        /// Works only in Release mode. NOTE: calling GC.Collect yourself is not recommended
        /// </summary>
        private static void ForcingGarbageCollection()
        {
            var myTypeOne = new MyCustomType(1);
            GC.Collect();
            GC.WaitForPendingFinalizers();
            var myTypeTwo = new MyCustomType(2);
        }
    }
}
