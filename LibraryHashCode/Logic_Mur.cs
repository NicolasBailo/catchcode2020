using LibraryHashCode.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LibraryHashCode
{
    public static class Logic_Mur
    {
        static float WeightScore = 1;
        static float WeightCapciti = 500;
        static float WeightDelay = 10000;
        static float WeightNumBooks= 200;
        static float WeightMoreBooksLessScore= 4000;

        static public List<Library> Process(List<Library> libraries)
        {
            var ret= new List<Library>();
            var librariesToRemove= new List<Library>();

            while(libraries.Count > 1){
                var library= libraries.OrderByDescending(x =>
                        (x.Books.ScoreSum * WeightScore)
                        + (x.NumBooksPerDay * WeightCapciti)
                        - (x.Signup * WeightDelay)
                        + (x.Books.Count * WeightNumBooks)
                        + ((x.Books.ScoreSum/x.Books.Count) * WeightMoreBooksLessScore)
                        ).First();
                ret.Add(library);
                libraries.Remove(library);


                foreach (var b in library.Books)
                {
                    foreach(var l in libraries){
                        l.Books.Remove(b);
                        if (l.Books.Count <= 0){
                            librariesToRemove.Add(l);
                        }
                    }
                }
                libraries= libraries.Except(librariesToRemove).ToList();
                librariesToRemove.Clear();
            }

            return ret;
/*
            return libraries.OrderByDescending(x =>
                    (x.Books.ScoreSum * WeightScore)
                    + (x.NumBooksPerDay * WeightCapciti)
                    - (x.Signup * WeightDelay)
                    + (x.Books.Count * WeightNumBooks)
                    + ((x.Books.ScoreSum/x.Books.Count) * WeightMoreBooksLessScore)
                    ).ToList();
*/
        }
    }
}
