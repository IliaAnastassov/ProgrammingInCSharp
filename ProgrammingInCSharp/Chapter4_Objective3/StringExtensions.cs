namespace Chapter4_Objective3
{
    using System;
    using System.Collections.Generic;

    public static class StringExtensions
    {
        private static IEnumerable<char> punctuation = new List<char> { ' ', ',', ';', '.' };

        /// <summary>
        /// Counts the number of words in a string (WORK IN PROGRESS)
        /// </summary>
        /// <param name="s"></param>
        /// <returns>the number of words</returns>
        public static int CountWords(this string s)
        {
            return s.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).Length;
        }
    }
}
