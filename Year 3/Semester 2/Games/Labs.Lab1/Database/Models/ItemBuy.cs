using System;
using System.ComponentModel.DataAnnotations;

namespace Labs.Lab1.Database.Models
{
    public sealed class ItemBuy
    {
        [Key]
        public Guid Id { get; set; }
        public DateTime Date { get; set; }

        public Guid UserId { get; set; }
        public User User { get; set; }

        public string Item { get; set; }
        public float Price { get; set; }
    }
}
