using Microsoft.EntityFrameworkCore;
using Baza.Models.Entities;

namespace Baza.Data
{
    // Ми чітко вказуємо, що наслідуємось від системного DbContext
    public class ApplicationDbContext : Microsoft.EntityFrameworkCore.DbContext
    {
        // Використовуємо повний шлях до DbContextOptions
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
}