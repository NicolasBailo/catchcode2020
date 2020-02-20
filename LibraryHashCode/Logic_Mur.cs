using LibraryHashCode.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LibraryHashCode
{
    public static class Logic_Mur
    {
        static float WeightScore = 10f;
        static float WeightCapciti = 500f;
        static float WeightDelay = 1000f;

        public static void Process( ref List<Library> libraries)
        {
            libraries = libraries
                .OrderBy(x => x.Signup)
                .ThenByDescending(y => y.NumBooksPerDay)
                .ThenByDescending(x => x.Books.AvgScore).ToList();
                
                ////.OrderByDescending(x =>
                ////(x.Books.ScoreSum * WeightScore)
                ////+ (x.NumBooksPerDay * WeightCapciti)
                ////- (x.Signup * WeightDelay)
                ////).ToList();
        }
    }
}
