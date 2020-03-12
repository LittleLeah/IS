using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using _1._4.Models;
using _1._4.Shared;

namespace _1._4.Controllers
{
    public class HomeController : Controller
    {
        DAL dal = new DAL();
        [HttpGet]
        public ActionResult Index()
        {

           
            IndexViewModel ivm = new IndexViewModel();
            List<Book> allBooks = dal.GetAllBooks();
            
            ivm.BooksInLibrary = new List<Book>();
            ivm.BooksOnLoan = new List<Book>();
            ivm.Users = dal.GetAllUsers();
            foreach (Book book in allBooks)
            {
                if (book.IsBorrowed)
                {
                    ivm.BooksOnLoan.Add(book);
                 
                }
                else
                {
                    ivm.BooksInLibrary.Add(book);
                }
            }
            return View(ivm);
        }

        [HttpPost]
        public ActionResult IndexCheckOut(IndexViewModel ivm)
        {
            dal.checkOutBook(ivm.ChosenBook.ID, ivm.ChosenUser.UserNo);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult IndexCheckIn(IndexViewModel ivm)
        {
            dal.checkInBook(ivm.ChosenBook.ID);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Users()
        {
            ViewBag.Message = "Nuværende biblioteksbrugere";
            UserViewModel users = new UserViewModel();
            users.users = dal.GetAllUsers();

            return View(users);
        }
        [HttpPost]
        public ActionResult Users(UserViewModel user)
        {
            dal.CreateUser(user.newUser);

            return RedirectToAction("Users");
        }

        [HttpGet]
        public ActionResult Books()
        {
            ViewBag.Message = "Alle Biblioteks bøger";
            ViewModel books = new ViewModel();
            List<Book> allBooks = dal.GetAllBooks();
            List<User> allUsers = dal.GetAllUsers();
            books.inLibrary = new List<Book>();
            books.onLoan = new List<Book>();
            books.onLoanTo = new List<User>();
            foreach (Book book in allBooks)
            {
                if (book.IsBorrowed)
                {
                    books.onLoan.Add(book);
                    books.onLoanTo.Add(allUsers.First(x => x.UserNo == book.CheckedOut.UserId));
                }
                else
                {
                    books.inLibrary.Add(book);
                }
            }

            return View(books);
        }

        [HttpPost]
        public ActionResult Books(ViewModel book)
        {
            dal.createNewBook(book.newBook);

            return RedirectToAction("Books");
        }
    }
}