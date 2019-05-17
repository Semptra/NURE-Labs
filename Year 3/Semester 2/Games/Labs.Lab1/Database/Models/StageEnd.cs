using System;
using System.ComponentModel.DataAnnotations;

namespace Labs.Lab1.Database.Models
{
    public sealed class StageEnd
    {
        [Key]
        public Guid Id { get; set; }
        public DateTime Date { get; set; }

        public Guid UserId { get; set; }
        public User User { get; set; }

        public int Stage { get; set; }
        public int Time { get; set; }
        public bool Win { get; set; }
        public int Income { get; set; }
    }
}
