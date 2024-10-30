using CBT.Domain.Models;
using CBT.Domain.Options;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace CBT.Infrastructure.Database
{
    public class SQLiteDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<UserAuth> Auth { get; set; }

        public SQLiteDbContext(DbContextOptions<SQLiteDbContext> contextOptions) : base(contextOptions)
        {
            //Database.EnsureDeleted();
            Database.EnsureCreated();
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(e =>
                {
                    e.ToTable("users");
                    e.HasKey(u => u.Id);
                    e.Property(u => u.Id).ValueGeneratedOnAdd();
                    e.Property(u => u.Name).IsRequired();
                }
            );
            modelBuilder.Entity<UserAuth>(e =>
                { 
                    e.ToTable("auth");
                    e.HasKey(a => a.UserId);
                    e.HasKey(a => a.Type);
                    e.HasOne(a => a.User)
                        .WithMany(u => u.UserAuth)
                        .HasForeignKey(a => a.UserId);
                }
            );
        }
    }
}
