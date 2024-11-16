using CBT.Application.Interfaces;
using CBT.Infrastructure.Common.Options;
using CBT.Infrastructure.Database;
using CBT.Infrastructure.Repositories;
using CBT.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace CBT.Infrastructure
{
    public static class ConfigureServices
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
        {
            services.AddTransient<IPasswordHasher, PasswordHasher>();
            services.AddTransient<ITokenProvider, JWTokenProvider>();
            services.AddTransient<IAuthService, AuthService>();
            services.AddTransient<IEventsService, EventsService>();

            services.AddDbContext<SQLiteDbContext>((serviceProvider, options) =>
            {
                var sqliteOptions = serviceProvider.GetRequiredService<IOptions<SQLiteOptions>>().Value;
                options.UseSqlite($"Data Source={sqliteOptions.DataSource}");
            });
            services.AddScoped<IAuthRepository, AuthRepository>();
            services.AddScoped<IEventsRepository, EventsRepository>();
            return services;
        }
    }
}
