namespace Chapter2_Objective1
{
    using System;

    public class Program
    {
        public static void Main(string[] args)
        {

        }

        private static void UseDays()
        {
            var weekend = Days.Saturday | Days.Sunday;
            Console.WriteLine(weekend);
        }
    }

    [Flags]
    public enum Days
    {
        None = 0x0,
        Sunday = 0x1,
        Monday = 0x2,
        Tuesday = 0x4,
        Wednesday = 0x5,
        Thursday = 0x6,
        Friday = 0x7,
        Saturday = 0x8
    }
}
