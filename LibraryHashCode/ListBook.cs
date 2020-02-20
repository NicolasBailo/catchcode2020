using LibraryHashCode.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace LibraryHashCode
{
    public class ListBook: List<Book>
    {
        public float ScoreSum  =0.0f;
        public float AvgScore => ScoreSum / base.Count;
        public List<int> ids = new List<int>();
        public new void Add(Book item)
        {
            if(!ids.Contains(item.Id))
            {
                ScoreSum += item.Score;
                base.Add(item);
            }
        }

        public void OrderByScored ()
        {
            base.Sort((x, y) => y.Score.CompareTo(x.Score));
        }
    }
}
