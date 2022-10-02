using Hendric.Models;
using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;

namespace Hendric.Controllers
{
    public class HomeController : Controller
    {
        DataService DataService = new DataService();
        

        public ActionResult Index()
        {
            BookVM bookVM = new BookVM();
            bookVM.bookTypes = DataService.GetBookTypes();
            bookVM.Authors = DataService.GetAuthors();
            bookVM.books = DataService.GetBooks();
            return View(bookVM);
        }

        public ActionResult BookViewDetails(int bookId)
        {
            DataService.GetBookBorrowsById(bookId);
            BookBorrowsVM BBVM = new BookBorrowsVM();
            BBVM.Book = DataService.GetBooksById(bookId);
            BBVM.bookBorrows = DataService.GetBookBorrowsById(bookId);

            return View(BBVM);
        }
        public void StudentStatus(int bookid)
        {
            foreach (var student in DataService.GetStudents())
            {
                foreach (var borrow in DataService.GetBookBorrowsById(bookid))
                {
                    if (String.IsNullOrEmpty(borrow.BroughtDate))
                    {                        
                        student.HasBook = true;
                    }
                    else 
                    {
                        student.HasBook = false;                        
                    }
                }
            }
        }

        public ActionResult ViewStudents(int bookid)
        {

            StudentStatus(bookid);            
            List<Class> classes = new List<Class>();
            foreach (Student student in DataService.GetStudents())
            {
                Class cl = new Class
                {
                    Name = student.Class
                };
                if (classes.Where(n => n.Name == student.Class).Count() == 0)
                {
                    classes.Add(cl);
                }
            }
            

            StudentVM studentVM = new StudentVM
            {
                Book = DataService.GetBooksById(bookid),
                Students = DataService.GetStudents(),
                Class = classes
            };
            return View(studentVM);
        }


        public ActionResult SearchBook(string bookName = null, int authorId = 0,int bookTypeId = 0 )
        {
            BookVM bookVM = new BookVM();
            bookVM.bookTypes = DataService.GetBookTypes();
            bookVM.Authors = DataService.GetAuthors();
            // Search Name, Type and Author
            if(bookName != null && authorId != 0 && bookTypeId != 0)
            {
                bookVM.books = DataService.GetBooks().Where(book => book.Name.Contains(bookName.Trim()) && book.BookType.BookTypeId == bookTypeId && book.Author.AuhtorId == authorId).ToList();
            }
            // Search Name
            if (bookName != null)
            {
                bookVM.books = DataService.GetBooks().Where(book => book.Name.Contains(bookName.Trim())).ToList();
            }
            // Search Author
            if (authorId != 0)
            {
                bookVM.books = DataService.GetBooks().Where(book => book.Author.AuhtorId == authorId).ToList();
            }
            // Search Type
            if (bookTypeId != 0)
            {
                bookVM.books = DataService.GetBooks().Where(book => book.BookType.BookTypeId == bookTypeId).ToList();
            }
            return View("Index", bookVM);
          
        }

        [HttpPost]
        public ActionResult SearchStudent(int bookid, string studentName = null, string _class = null)
        {
            StudentStatus(bookid);
            List<Class> classes = new List<Class>();
            foreach (Student student in DataService.GetStudents())
            {
                Class cl = new Class
                {
                    Name = student.Class
                };
                if (classes.Where(n => n.Name == student.Class).Count() == 0)
                {
                    classes.Add(cl);
                }
            }
            StudentVM studentVM = new StudentVM();
            studentVM.Class = classes;
            studentVM.Book = DataService.GetBooksById(bookid);
            if (_class != "Select a Class" || _class != null) {
                studentVM.Students = DataService.GetStudents().Where(cl => cl.Class == _class).ToList();
            }
            if(studentName != "")
            {
                studentVM.Students = DataService.GetStudents().Where(cl => cl.Name.Contains(studentName)).ToList();
            }
            //if (studentName != "" && ((_class != "Select a Class" || _class != null)) )
            //{
            //    studentVM.Students = DataService.GetStudents().Where(cl => cl.Name.Contains(studentName.Trim()) && cl.Class == _class).ToList();
            //}
                        
            return View("ViewStudents", studentVM);
            
        }


    }
}