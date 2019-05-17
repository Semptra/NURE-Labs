using System;
using System.ComponentModel.DataAnnotations;

namespace Labs.Lab1.Database.Models
{
    public sealed class CurrencyBuy
    {
        [Key]
        public Guid Id { get; set; }
        public DateTime Date { get; set; }

        public Guid UserId { get; set; }
        public User User { get; set; }

        public string Name { get; set; }
        public float Price { get; set; }
        public int Income { get; set; }
    }
}
