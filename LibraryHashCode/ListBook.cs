using LibraryHashCode.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace LibraryHashCode
{
    public class ListBook: List<Book>
    {
        public float ScoreSum  =0.0f;
        public new void Add(Book item)
        {
            ScoreSum += item.Score;
            base.Add(item);
        }
    }
}
