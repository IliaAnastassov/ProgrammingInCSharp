namespace Chapter4_Objective3
{
    public class Person
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public int Age { get; set; }

        public Cart Cart { get; set; }

        public override string ToString()
        {
            return $"{FirstName} {LastName}, {Age}";
        }
    }
}
