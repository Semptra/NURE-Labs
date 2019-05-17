using System;
using System.Collections.Generic;

namespace Lab5.Database
{
    public class Product
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public double Cost { get; set; }

        public virtual ICollection<UserProduct> UserProducts { get; set; }
    }
}
