using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Web;
using System.Web.Razor.Parser.SyntaxTree;

namespace Hendric.Models
{
    public class Book
    {
        public int BookId { get; set; }
        public string Name { get; set; }
        public Author Author { get; set; }
        public BookType BookType { get; set; }
        public int PageCount { get; set; }
        public int Points { get; set; }
        public string Status { get; set; }

        
    }

    
}