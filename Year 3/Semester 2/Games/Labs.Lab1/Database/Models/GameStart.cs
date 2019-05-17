using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Labs.Lab1.Database.Models
{
    public sealed class GameStart
    {
        [Key]
        public Guid Id { get; set; }
        public DateTime Date { get; set; }

        public Guid UserId { get; set; }
        public User User { get; set; }
    }
}
