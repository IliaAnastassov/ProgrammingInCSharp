namespace Chapter2_Objective1
{
    using System;

    public class Program
    {
        public static void Main(string[] args)
        {

        }

        private static void UseCustomNullable()
        {
            var myNullable = new Nullable<int>(66);
            Console.WriteLine(myNullable);
        }

        private static void UseNamedParameters()
        {
            MethodWithOptionalParameters(66, optionalParTwo: false);
        }

        private static void MethodWithOptionalParameters(int number, string optionalParOne = "Some text", bool optionalParTwo = true)
        {
            // implementation is not relevant in this case
        }

        private static void UseWorkDays()
        {
            var day = WorkDays.Monday;

            if ((int)day == 1)
            {
                Console.WriteLine("Damn, I gotta go to work...");
            }
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

    public enum WorkDays : byte
    {
        Monday = 1,
        Tuesday = 2,
        Wednesday = 3,
        Thursday = 4,
        Friday = 5
    }
}
