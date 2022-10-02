using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Web;

namespace Hendric.Models
{
    public class DataService
    {
        
        private String ConnectionString;
        public DataService()
        {           
            ConnectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        }

        // Get all books 
        public List<Book> GetBooks()
        {
            List<Book> Books = new List<Book>();
            using(SqlConnection conn = new SqlConnection(ConnectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("select * from books",conn))
                {
                    using(SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Book Book = new Book();
                            Book.Name = reader["Name"].ToString();
                            Book.BookId = Convert.ToInt32(reader["bookId"]);
                            Book.BookType = GetBookTypeById((int)reader["typeId"]);
                            Book.Author = GetAuthorById(Convert.ToInt32(reader["authorId"]));
                            Book.Points = Convert.ToInt32(reader["point"]);
                            Book.PageCount = (int)reader["pagecount"];

                            Books.Add(Book);
                        }
                    }
                }
                conn.Close();
            }
            foreach(var Book in Books)
            {
                var borrows = GetBookBorrowsById(Book.BookId);
                foreach(var borrow in borrows)
                {
                    if(String.IsNullOrEmpty(borrow.BroughtDate))
                    {
                        Book.Status = "Out";
                    }
                    else
                    {
                        Book.Status = "Avaliable";
                    }
                }
            }
            return Books;
        }

        public List<BookBorrow> GetBookBorrows()
        {
            List<BookBorrow> BookBorrow = new List<BookBorrow>();
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("select * from borrows", conn))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            BookBorrow bookBorrow = new BookBorrow
                            {
                                BorrowId = (int)reader["borrowId"],
                                BookId = (int)reader["bookid"],
                                BorrowedBy = GetStudentById((int)reader["studentId"]),
                                TakenDate = reader["takenDate"].ToString(),
                                BroughtDate = reader["broughtDate"].ToString()
                            }; 

                           
                        }
                    }
                }
                conn.Close();
            }
            return BookBorrow;
        }

        public List<BookBorrow> GetBookBorrowsById(int id)
        {
            List<BookBorrow> BookBorrows = new List<BookBorrow>();
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("select * from borrows where borrows.bookId = " + id, conn))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            BookBorrow bookBorrow = new BookBorrow
                            {
                                BorrowId = Convert.ToInt32(reader["borrowId"]),
                                BookId = Convert.ToInt32(reader["bookId"]),
                                BorrowedBy = GetStudentById(Convert.ToInt32(reader["studentId"])),
                                TakenDate = Convert.ToString(reader["takenDate"]),
                                BroughtDate = Convert.ToString(reader["broughtDate"])
                            };
                            BookBorrows.Add(bookBorrow);
                        }
                    }
                }
                conn.Close();
            }
            return BookBorrows;
        }

        public Student GetStudentById(int id)
        {
            Student student = null;
            using(SqlConnection conn = new SqlConnection(ConnectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("select * from students where studentId = " + id, conn))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            student = new Student
                            {
                                Id = (int)reader["studentId"],
                                Name = (string)reader["Name"],
                                Surname = (string)reader["Surname"],
                                Class = (string)reader["class"],
                                Points = (int)reader["point"]
                            };
                        }
                    }
                }
                conn.Close();
            }
            return student;
        }

        public List<Student> GetStudents()
        {
            List<Student> students = new List<Student>();
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("select * from students ", conn))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Student student = new Student
                            {
                                Id = (int)reader["studentId"],
                                Name = (string)reader["Name"],
                                Surname = (string)reader["Surname"], 
                                Class = (string)reader["class"],
                                Points = (int)reader["point"]
                            };
                            students.Add(student);
                        }
                    }
                }
                conn.Close();
            }
            return students;
        }
        public Book GetBooksById(int id)
        {
           Book  Book = null ;
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("select * from books where bookId = " + id, conn))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Book = new Book();
                            Book.Name = reader["Name"].ToString();
                            Book.BookId = Convert.ToInt32(reader["bookId"]);
                            Book.BookType = GetBookTypeById((int)reader["typeId"]);
                            Book.Author = GetAuthorById(Convert.ToInt32(reader["authorId"]));
                            Book.Points = Convert.ToInt32(reader["point"]);
                            Book.PageCount = (int)reader["pagecount"];                            
                        }
                    }
                }
                conn.Close();
            }
            
            var borrows = GetBookBorrowsById(Book.BookId);
            foreach (var borrow in borrows)
            {
                if (String.IsNullOrEmpty(borrow.BroughtDate))
                {
                    Book.Status = "Out";
                }
                else
                {
                    Book.Status = "Avaliable";
                }
            }
            
            return Book;
        }

        // Get Type by Id 
        public BookType GetBookTypeById(int id)
        {
            BookType bookType = null;
            using(SqlConnection con = new SqlConnection(ConnectionString))
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand("select * from types where typeId = " + id, con))
                {
                    using(SqlDataReader r = cmd.ExecuteReader())
                    {
                        while (r.Read())
                        {
                            bookType = new BookType
                            {
                                BookTypeId = (int)r["typeId"],
                                Name = r["Name"].ToString()
                            };
                        }
                    }
                }
                con.Close();
            }
            return bookType;
        }
        public List<BookType> GetBookTypes()
        {
            List<BookType> bookTypes = new List<BookType>();
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand("select * from types", con))
                {
                    using (SqlDataReader r = cmd.ExecuteReader())
                    {
                        while (r.Read())
                        {
                            BookType bookType = new BookType
                            {
                                BookTypeId = (int)r["typeId"],
                                Name = r["Name"].ToString()
                            };
                            bookTypes.Add(bookType);
                        }
                    }
                }
                con.Close();
            }
            return bookTypes;
        }

        // Get Author by id 
        public Author GetAuthorById(int id)
        {
            Author author = null;
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand("select * from authors where authorId = " + id, con))
                {
                    using (SqlDataReader r = cmd.ExecuteReader())
                    {
                        while (r.Read())
                        {
                            author = new Author
                            {
                                AuhtorId = (int)r["authorId"],
                                Name = r["Name"].ToString(),
                                Surname = r["surname"].ToString()
                            };
                        }
                    }
                }
                con.Close();
            }
            return author;
        }

        public List<Author> GetAuthors()
        {
            List<Author> authors = new List<Author>();
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand("select * from authors ", con))
                {
                    using (SqlDataReader r = cmd.ExecuteReader())
                    {
                        while (r.Read())
                        {
                            Author author = new Author
                            {
                                AuhtorId = (int)r["authorId"],
                                Name = r["Name"].ToString(),
                                Surname = r["surname"].ToString()
                            };
                            authors.Add(author);
                        }
                    }
                }
                con.Close();
            }
            return authors;
        }

    }
}