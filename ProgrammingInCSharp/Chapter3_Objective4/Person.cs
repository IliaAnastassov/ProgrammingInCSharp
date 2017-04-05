namespace Chapter3_Objective4
{
    using System.Diagnostics;

    [DebuggerDisplay("Name = {FirstName} {LastName}")]
    public class Person
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }
    }
}
