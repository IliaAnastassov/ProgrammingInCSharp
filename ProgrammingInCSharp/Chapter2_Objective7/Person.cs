using System;

namespace Chapter2_Objective7
{
    public class Person
    {
        public Person(string firstName, string lastName)
        {
            this.FirstName = firstName;
            this.LastName = lastName;
        }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string ToString(string format)
        {
            string result;

            if (string.IsNullOrWhiteSpace(format) || format == "G")
            {
                format = "FL";
            }

            format = format.Trim()
                           .ToUpperInvariant();

            switch (format)
            {
                case "FL":
                    result = $"{FirstName} {LastName}";
                    break;
                case "LF":
                    result = $"{LastName} {FirstName}";
                    break;
                case "FSL":
                    result = $"{FirstName}, {LastName}";
                    break;
                case "LSF":
                    result = $"{LastName}, {FirstName}";
                    break;
                default:
                    throw new FormatException($"The {format} format string is not supported");
            }

            return result;
        }
    }
}
