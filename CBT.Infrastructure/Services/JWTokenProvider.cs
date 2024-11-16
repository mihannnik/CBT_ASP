using CBT.Domain.Models;
using Microsoft.Extensions.Options;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using CBT.Application.Interfaces;
using CBT.Infrastructure.Common.Options;

namespace CBT.Infrastructure.Services
{
    public class JWTokenProvider : ITokenProvider
    {
        private readonly JWTOptions _options;
        public JWTokenProvider(IOptions<JWTOptions> options) => _options = options.Value;
        public string GenerateToken(Claim[] claims, out DateTime expireAt, bool isRefresh = false)
        {
            var SecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_options.SecretKey));
            var SigningCredentials = new SigningCredentials(SecurityKey, SecurityAlgorithms.HmacSha256);
            var Token = new JwtSecurityToken(
                claims: claims,
                signingCredentials: SigningCredentials,
                expires: expireAt = DateTime.UtcNow.Add(isRefresh ? _options.RefreshExpireTime : _options.ExpireTime)
            );
            return new JwtSecurityTokenHandler().WriteToken(Token);
        }

        public string GenerateToken(Claim[] claims, bool isRefresh = false) 
            => GenerateToken(claims, out _, isRefresh);
    }
}
