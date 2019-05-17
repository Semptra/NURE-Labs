using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Labs.Lab1.Database.Models
{
    public sealed class User
    {
        [Key]
        public Guid Id { get; set; }
        public DateTime Date { get; set; }

        public string Country { get; set; }
        public string Gender { get; set; }
        public int Age { get; set; }

        public ICollection<GameStart> GameStart { get; set; }
        public ICollection<StageStart> StageStart { get; set; }
        public ICollection<StageEnd> StageEnd { get; set; }
        public ICollection<ItemBuy> ItemBuy { get; set; }
        public ICollection<CurrencyBuy> CurrencyBuy { get; set; }
    }
}
