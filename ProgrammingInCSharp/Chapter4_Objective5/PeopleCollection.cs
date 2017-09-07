using System.Collections.Generic;
using System.Text;

namespace Chapter4_Objective5
{
    public class PeopleCollection : List<Person>
    {
        public void RemoveById(int id)
        {
            for (int i = 0; i < this.Count; i++)
            {
                if (this[i].Id == id)
                {
                    this.RemoveAt(i);
                }
            }
        }

        public void RemoveById(params int[] ids)
        {
            for (int i = 0; i < this.Count; i++)
            {
                for (int j = 0; j < ids.Length; j++)
                {
                    if (this[i].Id == ids[j])
                    {
                        this.RemoveAt(i);
                    }
                }
            }
        }

        public override string ToString()
        {
            var sb = new StringBuilder();

            foreach (var person in this)
            {
                sb.AppendFormat($"{person.Id} : {person.Name}\r\n");
            }

            return sb.ToString();
        }
    }
}
