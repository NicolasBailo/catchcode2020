using System;
using System.Collections.Generic;
using System.Text;

namespace LibraryHashCode.Models
{
    public class Library
    {
        public int Id { get; set; }

        public Library(int id)
        {
            Id = id;
            Books = new List<Book>();
        }

        public List<Book> Books { get; set; }
        public int Days { get; set; }
        public int NumBooksPerDay { get; set; }
        public int Signup { get; set; }
    }
}
