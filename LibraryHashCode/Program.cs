using LibraryHashCode.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace LibraryHashCode
{
    class Program
    {
        static void Main(string[] args)
        {
            ProcessFile("a_example");
            ProcessFile("b_read_on");
            ProcessFile("c_incunabula");
            ProcessFile("d_tough_choices");
            ProcessFile("e_so_many_books");
            ProcessFile("f_libraries_of_the_world");
        }

        static void ProcessFile(string name)
        {
            var lines = File.ReadLines("Files/" + name + ".txt");
            var numLine = 0;
            var boolLine = true;

            var libraries = new List<Library>();
            var books = new List<Book>();

            var numOfLibraries = default(int);
            var daysForScanning = default(int);
            var scoreBooks = new List<int>();

            var numLibrary = 0;

            foreach (var line in lines)
            {
                //Console.WriteLine(line);

                var items = line.Split(" ");

                if (numLine == 0)
                {
                    numOfLibraries = int.Parse(items[1]);
                    daysForScanning = int.Parse(items[2]);
                    numLine++;

                    for (int i = 0; i < numOfLibraries; i++)
                    {
                        libraries.Add(new Library(i));
                    }
                }
                else if (numLine == 1)
                {
                    books = new List<Book>();
                    for (int i = 0; i < items.Count(); i++)
                    {
                        var bookString = items[i];
                        var book = new Book
                        {
                            Id = i,
                            Score = int.Parse(bookString)
                        };

                        books.Add(book);
                    }
                    numLine++;
                }
                else
                {
                    if (numLibrary > libraries.Count() - 1)
                    {
                        break;
                    }
                    var library = libraries[numLibrary];
                    if (boolLine)
                    {
                        library.Signup = int.Parse(items[1]);
                        library.NumBooksPerDay = int.Parse(items[2]);
                        boolLine = false;
                    }
                    else
                    {
                        for (int i = 0; i < items.Count(); i++)
                        {
                            var id = int.Parse(items[i]);
                            library.Books.Add(books[id]);
                        }

                        boolLine = true;
                        numLibrary++;
                    }
                }
            }

            //////  EL MELME
            Logic_Mur.Process(ref libraries);



            //libraries
            //books

            var fileNameResult = name + ".txt";

            if (File.Exists(fileNameResult))
            {
                File.Delete(fileNameResult);
            }

            using (StreamWriter sw = File.AppendText(fileNameResult))
            {
                sw.WriteLine(libraries.Count());
                var init = 0;
                var count = 0;
                foreach (var library in libraries)
                {
                    library.Books.OrderByScored();
                    init += library.Signup;

                    var result1 = library.Id + " " + library.Books.Count();
                    sw.WriteLine(result1);
                    var ids=library.Books.Take((daysForScanning - init) * library.NumBooksPerDay)
                        .Select(book => book.Id);
                    var result2 = string.Join(" ", library.Books
                        .Select(book => book.Id.ToString()));
                    
                    if ((libraries.Count() - 1) != count)
                        sw.WriteLine(result2);
                    else
                        sw.Write(result2);

                    library.AddUsage(ids);
                    count++;
                }
            }
        }
    }
}
