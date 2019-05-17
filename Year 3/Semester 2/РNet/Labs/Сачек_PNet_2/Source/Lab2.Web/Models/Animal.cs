using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Lab2.Web.Models
{
    public class Animal
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
    }
}