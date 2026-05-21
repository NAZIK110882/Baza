using Microsoft.EntityFrameworkCore;
using Baza.Models.Entities;
using Baza.Models;

namespace Baza.Data
{
    public class ApplicationDbContext : Microsoft.EntityFrameworkCore.DbContext
    {
        public ApplicationDbContext(Microsoft.EntityFrameworkCore.DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public Microsoft.EntityFrameworkCore.DbSet<Player> Players { get; set; } = default!;
        public Microsoft.EntityFrameworkCore.DbSet<GameSession> GameSessions { get; set; } = default!;

        public Microsoft.EntityFrameworkCore.DbSet<Achievement> Achievements { get; set; } = default!;

        protected override void OnModelCreating(Microsoft.EntityFrameworkCore.ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Player>().ToTable("Players");
            modelBuilder.Entity<GameSession>().ToTable("GameSessions");

            modelBuilder.Entity<Achievement>().ToTable("Achievements");
        }
    }
}