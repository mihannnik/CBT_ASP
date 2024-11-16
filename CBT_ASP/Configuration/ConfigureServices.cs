using CBT.Application.Interfaces;
using CBT.Web.Services;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace CBT.Web.Configuration
{
    public static class ConfigureServices
    {
        public static IServiceCollection AddWebServices(this IServiceCollection services)
        {
            services.AddHttpContextAccessor();
            services.AddScoped<IUser, CurrentUser>();
            return services;
        }
    }
}
