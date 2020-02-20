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
        public float AvgScore => this
            .OrderBy(x=>x.Score)
            .Skip((int)(base.Count*0.1))
            .Take((int)(base.Count * 0.9))
            .Sum(x=>x.Score) 
            / base.Count;
        public List<int> ids = new List<int>();
        public new void Add(Book item)
        {
            if(!ids.Contains(item.Id))
            {
                ScoreSum += item.Score;
                base.Add(item);
                item.NumApear++;
            }
        }

        internal void AddUsage(IEnumerable<int> ids)
        {
            foreach (var book in this.Where(x => ids.Contains(x.Id)))
                book.HasUsed = 1;
        }

        public void OrderByScored ()
        {
            base.Sort((x, y) => x.HasUsed.CompareTo(y.HasUsed) ==0 ?  
            y.Score.CompareTo(x.Score)==0?  
                x.NumApear.CompareTo(y.NumApear) :
                y.Score.CompareTo(x.Score):
                x.HasUsed.CompareTo(y.HasUsed));
        }
    }
}
