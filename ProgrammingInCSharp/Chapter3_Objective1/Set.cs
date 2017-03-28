namespace Chapter3_Objective1
{
    using System.Collections.Generic;

    public class Set<T>
    {
        private List<T>[] buckets = new List<T>[100];

        public void Insert(T item)
        {
            var bucket = GetBucket(item.GetHashCode());

            if (Contains(item, bucket))
            {
                return;
            }
            else if (buckets[bucket] == null)
            {
                buckets[bucket] = new List<T>();
            }

            buckets[bucket].Add(item);
        }

        public bool Contains(T item)
        {
            return Contains(item, GetBucket(item.GetHashCode()));
        }

        private bool Contains(T item, int bucket)
        {
            if (buckets[bucket] != null)
            {
                foreach (var member in buckets[bucket])
                {
                    if (member.Equals(item))
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        private int GetBucket(int hashcode)
        {
            unchecked
            {
                return (int)((uint)hashcode % (uint)buckets.Length);
            }
        }
    }
}
