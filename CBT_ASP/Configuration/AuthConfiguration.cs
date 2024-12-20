﻿using CBT.Application.Interfaces;
using CBT.Domain.Models;
using CBT.Domain.Models.Enums;
using CBT.Infrastructure.Common.Options;
using CBT.Web.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Text;

namespace CBT.Web.Configuration
{
    public static class AuthConfiguration
    {
        public static void AddAuthentication(this IServiceCollection Services, JWTOptions Options)
        {
            Services.AddAuthorization(options =>
                {
                    foreach (var permission in Permissions.GetValues<Permissions>().Cast<Permissions>())
                    {
                        options.AddPolicy(new StringBuilder().Append("Event").Append(permission.ToString()).Append("Policy").ToString(),
                            policy =>
                            {
                                policy
                                    .RequireAuthenticatedUser()
                                    .RequireClaim(ClaimTypes.Role, UserPermissions.RolesPermissions
                                        .Where(i => i.Value.Contains(permission))
                                        .Select(i => i.Key.ToString())
                                        .ToList());
                            });
                    }
                }
            );
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
