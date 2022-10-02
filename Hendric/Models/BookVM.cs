using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Hendric.Models
{
    public class BookVM
    {
        public List<Book> books = new List<Book>();
        public List<Author> Authors = new List<Author>();
        public List<BookType> bookTypes = new List<BookType>();
    }
}