namespace Chapter3_Objective1
{
    using System.Collections.Generic;

    public class SimpleSet<T>
    {
        private List<T> items = new List<T>();

        public bool Insert(T item)
        {
            if (!Contains(item))
            {
                items.Add(item);
                return true;
            }

            return false;
        }

        private bool Contains(T item)
        {
            foreach (var member in items)
            {
                if (member.Equals(item))
                {
                    return true;
                }
            }

            return false;
        }
    }
}
