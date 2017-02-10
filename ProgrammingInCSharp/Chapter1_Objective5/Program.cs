namespace Chapter1_Objective5
{
    using System;

    class Program
    {
        static void Main(string[] args)
        {

        }

        private static void UseEnvironmentFailFast()
        {
            var input = Console.ReadLine();

            try
            {
                var number = int.Parse(input);
                if (number == 66)
                {
                    Environment.FailFast("Executing Order 66");
                }
            }
            catch (FormatException)
            {
                Console.WriteLine("NAN");
            }
            finally
            {
                Console.WriteLine("End");
            }
        }

        private static void UseFinallyBlock()
        {
            var input = Console.ReadLine();

            try
            {
                int.Parse(input);
            }
            catch (ArgumentNullException)
            {
                Console.WriteLine("You need to enter a value.");
            }
            catch (FormatException)
            {
                Console.WriteLine($"{input} is not a valid number. Please try again.");
            }
            finally
            {
                Console.WriteLine("Process completed");
            }
        }

        private static void CatchDifferentExceptionTypes()
        {
            while (true)
            {
                var input = Console.ReadLine();

                try
                {
                    int.Parse(input);
                    break;
                }
                catch (ArgumentNullException)
                {
                    Console.WriteLine("You need to enter a value.");
                }
                catch (FormatException)
                {
                    Console.WriteLine($"{input} is not a valid number. Please try again.");
                }
            }
        }

        private static void CatchFormatException()
        {
            while (true)
            {
                var input = Console.ReadLine();

                if (string.IsNullOrEmpty(input))
                {
                    break;
                }

                try
                {
                    int.Parse(input);
                    break;
                }
                catch (FormatException)
                {
                    Console.WriteLine($"{input} is not a valid number. Please try again.");
                }
            }
        }
    }
}
