using Microsoft.EntityFrameworkCore;
using Quiz.Core.Models.Database.Entities;

namespace Quiz.Data
{
    public class QuizContext : DbContext
    {
        public QuizContext(DbContextOptions<QuizContext> options) : base(options)
        {
        }

        public virtual DbSet<User> Organizations { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .ToTable("Users", "dbo")
                .HasKey(x => x.Id);
        }
    }
}
