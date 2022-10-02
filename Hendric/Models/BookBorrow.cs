using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Hendric.Models
{
    public class BookBorrow
    {
        public int BorrowId { get; set; }
        public int BookId { get; set; }
        public string TakenDate { get; set; }
        public string BroughtDate { get; set; }
        public Student BorrowedBy { get; set; }
    }
}