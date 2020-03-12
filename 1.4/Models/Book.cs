using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace _1._4.Models
{
    public class Book
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string ISBN { get; set; }
        public bool IsBorrowed { get; set; }

        public Checkout CheckedOut { get; set; }

        public Book()
        {
            Title = "";
            Author = "";
            ISBN = "";
        }
        public Book( string title, string author, string isbn)
        {
            
            Title = title;
            Author = author;
            ISBN = isbn;
        }
    }
}