using CBT.Domain.Interfaces;
using CBT.Domain.Models;
using CBT.Domain.Options;
using Microsoft.Extensions.Options;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;

namespace CBT.Application.Services
{
    public class JWTokenProvider : ITokenProvider
    {
        private readonly JWTOptions _options;
        public JWTokenProvider(IOptions<JWTOptions> options) => _options = options.Value;
        public string GenerateToken(Claim[] claims)
        {
            var SecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_options.SecretKey));
            var SigningCredentials = new SigningCredentials(SecurityKey, SecurityAlgorithms.HmacSha256);
            var Token = new JwtSecurityToken(
                claims: claims,
                signingCredentials: SigningCredentials,
                expires: DateTime.UtcNow.Add(TimeSpan.Parse(_options.ExpireTime))
                );
            return new JwtSecurityTokenHandler().WriteToken(Token);
        }
        public string GenerateRefreshToken(Claim[] claims)
        {
            var SecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_options.SecretKey));
            var SigningCredentials = new SigningCredentials(SecurityKey, SecurityAlgorithms.HmacSha256);
            var Token = new JwtSecurityToken(
                claims: claims,
                signingCredentials: SigningCredentials,
                expires: DateTime.UtcNow.Add(TimeSpan.Parse(_options.RefreshExpireTime))
                );
            return new JwtSecurityTokenHandler().WriteToken(Token);
        }
    }
}
