using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Hendric.Models
{
    public class Student
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public  string Surname { get; set; }
        public  string Class { get; set; }
        public int Points { get; set; }
        public bool HasBook { get; set; }
       
    }
}