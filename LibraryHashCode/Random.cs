using LibraryHashCode.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace LibraryHashCode
{
    public static class RandomLogic 
    {

        private static Random rng = new Random();

        public static void Process(List<Library> libraries)
        {
            libraries.Shuffle();

            foreach(var library in libraries)
            {
                library.Books.Shuffle();
            }
        }


        public static void Shuffle<T>(this IList<T> list)
        {
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = rng.Next(n + 1);
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }
    }
}
