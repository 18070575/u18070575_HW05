using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Hendric.Models
{
    public class StudentVM
    {
        public Book Book { get; set; }
        public List<Student> Students { get; set; }
        public List<Class> Class { get; set; }
    }
}