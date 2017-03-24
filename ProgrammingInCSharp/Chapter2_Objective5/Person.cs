namespace Chapter2_Objective5
{
    using System;

    [Serializable]
    public class Person
    {
        private int secretValue;

        public Person(string firstName, string lastName)
        {
            FirstName = firstName;
            LastName = lastName;
            secretValue = 66;
        }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        private void SetSecretValue(int value)
        {
            secretValue = value;
        }
    }
}
