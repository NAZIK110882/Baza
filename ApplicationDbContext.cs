using Microsoft.EntityFrameworkCore;

namespace Baza.Data
{
    // Явно вказуємо, що ми наслідуємось від правильного DbContext
    public class ApplicationDbContext : Microsoft.EntityFrameworkCore.DbContext
    {
        // Передаємо параметри саме в базовий клас Microsoft.EntityFrameworkCore.DbContext
        public ApplicationDbContext(Microsoft.EntityFrameworkCore.DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public Microsoft.EntityFrameworkCore.DbSet<Player> Players { get; set; } = default!;

        protected override void OnModelCreating(Microsoft.EntityFrameworkCore.ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Налаштовуємо таблицю
            modelBuilder.Entity<Player>().ToTable("Players");
        }
    }

    public class Player
    {
        public int Id { get; set; }
        public string Nickname { get; set; } = string.Empty;
        public int Score { get; set; }
    }
}