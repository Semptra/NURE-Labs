using System;
using System.Collections.Generic;

namespace Lab5.Database
{
    public class User
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
        public DateTime Birthdate { get; set; }

        public virtual ICollection<UserProduct> UserProducts { get; set; }
    }
}
