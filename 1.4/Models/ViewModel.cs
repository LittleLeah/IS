using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace _1._4.Models
{
    public class ViewModel
    {
        public List<Book> inLibrary;
        public List<Book> onLoan;
        public List<User> onLoanTo;
        public Book newBook { get; set; }

        public ViewModel()
        {
            newBook = new Book();
        }
    }
}