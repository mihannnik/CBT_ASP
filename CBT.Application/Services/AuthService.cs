using CBT.Domain.Interfaces;
using CBT.Domain.Models;
using CBT.Domain.Requests;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace CBT.Application.Services
{
    public class AuthService(IAuthRepository Repository, IPasswordHasher PasswordHasher, ITokenProvider TokenProvider) : IAuthService
    {
        public string GetAuthToken(User user)
        {
            Claim[] claims = [
                new("Id", user.Id.ToString()),
                new("Name", user.Name),
                new("Role", user.Role.ToString())
                ];
            return TokenProvider.GenerateToken(claims);
        }

        public string GetRefreshToken(User user)
        {
            Claim[] claims = [
                new("Id", user.Id.ToString())
                ];
            return TokenProvider.GenerateRefreshToken(claims);
        }

        public User? GetAuthUser(LoginRequest login)
        {
            var user = Repository.GetUser(login.Identity);
            if(user is UserAuth Auth && PasswordHasher.Verify(login.Password, Auth.PasswordHash))
            {
                return Auth.User;
            }
            return null;
        }

        public string RefreshToken()
        {
            throw new NotImplementedException();
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
