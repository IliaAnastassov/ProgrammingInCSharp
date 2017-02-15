namespace Chapter1_Objective5
{
    using System;
    using System.IO;
    using System.Runtime.ExceptionServices;

    public class Program
    {
        public static void Main(string[] args)
        {

        }

        private static void UseExceptionDispatchInfo()
        {
            ExceptionDispatchInfo possibleException = null;

            try
            {
                var number = int.Parse(Console.ReadLine());
            }
            catch (FormatException ex)
            {
                possibleException = ExceptionDispatchInfo.Capture(ex);
            }

            if (possibleException != null)
            {
                possibleException.Throw();
            }
        }

        private static void UsePassInnerException()
        {
            try
            {
                PassInnerException();
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.InnerException.Message);
            }
        }

        private static void PassInnerException()
        {
            var input = Console.ReadLine();

            try
            {
                var number = int.Parse(input);
            }
            catch (FormatException ex)
            {
                throw new ArgumentException("Please use a valid number.", ex);
            }
            catch (OverflowException ex)
            {
                var message = GetOverflowExceptionMessage(input);

                throw new ArgumentException(message, ex);
            }
        }

        private static string GetOverflowExceptionMessage(string input)
        {
            string message;

            if (input.StartsWith("-"))
            {
                message = "Please use a larger number.";
            }
            else
            {
                message = "Please use a smaller number.";
            }

            return message;
        }

        private static string OpenAndParse(string filename)
        {
            if (string.IsNullOrWhiteSpace(filename))
            {
                throw new ArgumentNullException("fileName", "Filename is required");
            }

            return File.ReadAllText(filename);
        }

        private static void InspectException()
        {
            try
            {
                var n = int.Parse(Console.ReadLine());
                Console.WriteLine($"Parsed: {n}");
            }
            catch (FormatException ex)
            {
                Console.WriteLine($"Message: {ex.Message}");
                Console.WriteLine($"Data: {ex.Data}");
                Console.WriteLine($"HResult: {ex.HResult}");
                Console.WriteLine($"StackTrace: {ex.StackTrace}");
                Console.WriteLine($"HelpLink: {ex.HelpLink}");
                Console.WriteLine($"InnerException: {ex.InnerException}");
                Console.WriteLine($"TargetSite: {ex.TargetSite}");
                Console.WriteLine($"Source: {ex.Source}");
            }
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
