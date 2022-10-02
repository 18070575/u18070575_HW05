using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Hendric.Models
{
    public class BookBorrowsVM
    {
        public Book Book { get; set; }
        public List<BookBorrow> bookBorrows { get; set; }
    }
}