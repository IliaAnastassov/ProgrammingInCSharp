namespace Chapter2_Objective7
{
    using System;

    public class Program
    {
        public static void Main(string[] args)
        {
        }

        private static void UsePersonFormatting()
        {
            var man = new Person("Dildo", "Schwaggins");
            Console.WriteLine(man);
            Console.WriteLine(man.ToString());
            Console.WriteLine(man.ToString(null));
            Console.WriteLine(man.ToString("fl"));
            Console.WriteLine(man.ToString("lf"));
            Console.WriteLine(man.ToString("fsl"));
            Console.WriteLine(man.ToString("lsf"));
        }
    }
}
