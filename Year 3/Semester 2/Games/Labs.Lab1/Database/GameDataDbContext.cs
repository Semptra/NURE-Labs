using Microsoft.EntityFrameworkCore;
using Labs.Lab1.Database.Models;

namespace Labs.Lab1.Database
{
    public sealed class GameDataDbContext : DbContext
    {
        public GameDataDbContext(){ }
        public GameDataDbContext(DbContextOptions options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<GameStart> GameStart { get; set; }
        public DbSet<StageStart> StageStart { get; set; }
        public DbSet<StageEnd> StageEnd { get; set; }
        public DbSet<ItemBuy> ItemBuy { get; set; }
        public DbSet<CurrencyBuy> CurrencyBuy { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
