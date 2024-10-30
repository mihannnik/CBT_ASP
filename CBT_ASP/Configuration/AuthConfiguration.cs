using CBT.Domain.Options;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace CBT.Web.Configuration
{
    public static class AuthConfiguration
    {
        public static void AddAuthentication(this IServiceCollection Services, JWTOptions Options)
        {
            Services.AddAuthorization();
            var SecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Options.SecretKey));
            Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).
                AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
                {
                    options.TokenValidationParameters = new()
                    {
                        ValidateIssuer = (Options.Issuer == null)?false:true,
                        ValidateAudience = (Options.Audience == null) ? false : true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = SecurityKey
                    };
                    options.Events = new JwtBearerEvents
                    {
                        OnMessageReceived = context =>
                        {
                            context.Token =
                                context.Request.Cookies[JWTOptions.CookiesName];
                            return Task.CompletedTask;
                        }
                    };
                   
                });
        }
    }
}
