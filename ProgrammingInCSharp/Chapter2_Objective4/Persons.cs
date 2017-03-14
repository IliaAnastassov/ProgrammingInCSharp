namespace Chapter2_Objective4
{
    using System.Collections;
    using System.Collections.Generic;

    public class Persons : IEnumerable<Person>
    {
        private Person[] persons;

        public IEnumerator<Person> GetEnumerator()
        {
            for (int i = 0; i < persons.Length; i++)
            {
                yield return persons[i];
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
