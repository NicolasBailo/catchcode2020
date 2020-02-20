using LibraryHashCode.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LibraryHashCode
{
    public static class Logic_Mur
    {
        static float WeightScore = 0.33f;
        static float WeightCapciti = 0.33f;
        static float WeightDelay = 0.33f;

        public static void Process(List<Library> libraries)
        {
            libraries=libraries.OrderBy(x =>
                (x.Books.ScoreSum * WeightScore)
                + (x.NumBooksPerDay * WeightCapciti)
                - (x.Signup * WeightDelay)
                ).ToList();
        }
    }
}
