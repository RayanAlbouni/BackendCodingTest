using Microsoft.EntityFrameworkCore;
using DatabaseManager.Models;

namespace DatabaseManager.Data
{
    public class GameContext : DbContext
    {
        public GameContext()
        {
        }
        public GameContext(DbContextOptions options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserModel>().HasIndex(b => b.Username).IsUnique();
        }
        //entities
        public DbSet<UserModel> Users { get; set; }
        public DbSet<UserScoreModel> UserScores { get; set; }
    }
}
