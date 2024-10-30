using CBT.Application.Services;
using CBT.Domain.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace CBT.Application
{
    public static class ConfigureServices
    {
        public static void AddAplicationServices(this IServiceCollection services) 
        { 
            services.AddTransient<IPasswordHasher, PasswordHasher>();
            services.AddTransient<ITokenProvider, JWTokenProvider>();
            services.AddTransient<IAuthService, AuthService>();
        }
    }
}
