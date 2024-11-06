using CBT.Application.Services;
using CBT.Domain.Interfaces;
using CBT.Domain.Options;
using CBT.Infrastructure.Database;
using CBT.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace CBT.Infrastructure
{
    public static class ConfigureServices
    {
        public static void AddInfrastructureServices(this IServiceCollection services)
        {
            services.AddDbContext<SQLiteDbContext>((serviceProvider, options) =>
            {
                var sqliteOptions = serviceProvider.GetRequiredService<IOptions<SQLiteOptions>>().Value;
                options.UseSqlite($"Data Source={sqliteOptions.DataSource}");
            });
            services.AddScoped<IAuthRepository, AuthRepository>();
            services.AddScoped<IEventsRepository, EventsRepository>();
        }
    }
}
