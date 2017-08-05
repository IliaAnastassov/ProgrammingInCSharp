namespace Chapter4_Objective3
{
    using System;
    using System.Collections.Generic;

    public static class MyLinq
    {
        public static IEnumerable<T> MyWhere<T>(this IEnumerable<T> source, Func<T, bool> predicate)
        {
            foreach (var item in source)
            {
                if (predicate(item))
                {
                    yield return item;
                }
            }
        }
    }
}
