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
        public DbSet<Event> Events { get; set; }

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
                    e.HasKey(a => a.Id);
                    e.Property(a => a.Id)
                        .ValueGeneratedOnAdd();
                    e.HasOne(a => a.User)
                        .WithMany(u => u.UserAuth)
                        .HasForeignKey(a => a.UserId);
                    e.HasIndex(a => new { a.UserId, a.Type })
                        .IsUnique();
                }
            );
            modelBuilder.Entity<Event>(e =>
                {
                    e.ToTable("events");
                    e.HasKey(ev => ev.Id);
                    e.Property(ev => ev.Id)
                        .ValueGeneratedOnAdd();
                    e.HasOne(ev => ev.Owner)
                        .WithMany(u => u.EventsOwner)
                        .HasForeignKey(ev => ev.OwnerId);
                });
            base.OnModelCreating(modelBuilder);
        }
    }
}
