using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace CBT.Infrastructure.Database;

public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<SQLiteDbContext>
{
    private static DbContextOptionsBuilder<SQLiteDbContext> CreateOptions(DbContextOptionsBuilder<SQLiteDbContext> builder)
    => builder
        .UseSqlite();

    public SQLiteDbContext CreateDbContext(string[] args)
        => new SQLiteDbContext(CreateOptions(new DbContextOptionsBuilder<SQLiteDbContext>()).Options);
}
