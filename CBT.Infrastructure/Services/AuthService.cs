using CBT.Application.Interfaces;
using CBT.Domain.Models;
using CBT.Domain.Models.Enums;
using CBT.Domain.Requests;
using System.Security.Claims;

namespace CBT.Infrastructure.Services
{
    public class AuthService(
            IAuthRepository Repository,
            IPasswordHasher PasswordHasher,
            ITokenProvider TokenProvider)
        : IAuthService
    {
        public string CreateAuthToken(User user)
        {
            Claim[] claims = [
                new(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new(ClaimTypes.Name, user.Name),
                new(ClaimTypes.Role, user.Role.ToString())
                ];
            return TokenProvider.GenerateToken(claims);
        }

        public string CreateRefreshToken(User user)
        {
            Claim[] claims = [
                new(ClaimTypes.NameIdentifier, user.Id.ToString())
                ];
            var token = TokenProvider.GenerateToken(claims, out DateTime expireAt, true);
            Repository.AddRefreshToken(user.Id, token, expireAt);
            return token;
        }

        public User? GetAuthUser(LoginRequest login)
        {
            var user = Repository.GetAuthUser(login.Identity);
            if (user is UserAuth Auth && PasswordHasher.Verify(login.Password, Auth.PasswordHash))
            {
                return Auth.User;
            }
            return null;
        }

        public User? UseRefreshToken(string token)
        {
            if (Repository.UseRefreshToken(token) is Guid id && Repository.GetUser(id) is User user)
            {
                return user;
            }
            return null;
        }

        public void Register(RegisterRequest register)
        {
            var PasswordHash = PasswordHasher.Hash(register.Password);
            List<UserAuth> Auths = new List<UserAuth> {
                new UserAuth{ Identity = register.Email, PasswordHash = PasswordHash, Type = IdentityTypes.Email },
                new UserAuth{ Identity = register.Name, PasswordHash = PasswordHash, Type = IdentityTypes.Name }
            };
            Repository.CreateUser(register.Name, Auths, register.Username);
        }
    }
}
