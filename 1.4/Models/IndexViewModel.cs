using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace _1._4.Models
{
    public class IndexViewModel
    {
        public List<Book> BooksInLibrary { get; set; }
        public List<Book> BooksOnLoan { get; set; }
        public List<User> Users { get; set; }

        public Book ChosenBook { get; set; }
        public User ChosenUser { get; set; }

        public IndexViewModel()
        {
            ChosenBook = new Book();
            ChosenUser = new User();
        }
    }
}