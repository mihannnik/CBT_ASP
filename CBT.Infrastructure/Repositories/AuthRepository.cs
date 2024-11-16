using CBT.Application.Interfaces;
using CBT.Domain.Models;
using CBT.Domain.Models.Enums;
using CBT.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace CBT.Infrastructure.Repositories
{
    public class AuthRepository(SQLiteDbContext context) : IAuthRepository
    {
        public UserRefreshToken AddRefreshToken(Guid id, string token, DateTime expirateDate)
        {
            UserRefreshToken UserToken = new UserRefreshToken
            {
                UserId = id,
                RefreshToken = token,
                ExpireAt = expirateDate
            };
            context.Tokens.Add(UserToken);
            context.SaveChanges();
            return UserToken;
        }

        public User CreateUser(string Name, ICollection<UserAuth> auths, string? Username = null, Role Role = Role.User)
        {
            User user = new User
            {
                Name = Name,
                Role = Role,
                Username = Username,
                UserAuth = auths
            };
            context.Users.Add(user);
            context.SaveChanges();
            return user;
        }

        public UserAuth? GetAuthUser(string Identity)
        {
            return context.Auth.Include(a => a.User).Where(a => a.Identity == Identity && (a.Type == IdentityTypes.Name || a.Type == IdentityTypes.Email)).FirstOrDefault();
            //return context.Users
                //.Include(u => u.UserAuth)
                //.Where(u => u.UserAuth.Any() && u.UserAuth.Any(a => a.Identity == Identity)).FirstOrDefault();
        }

        public User? GetUser(Guid id)
        {
            return context.Users.FirstOrDefault(u => u.Id.CompareTo(id) == 0);
        }

        public Guid? UseRefreshToken(string token)
        {
            if (context.Tokens.FirstOrDefault(t => t.RefreshToken == token) is UserRefreshToken UserToken)
            {
                context.Tokens.Remove(UserToken);
                context.SaveChanges();
                if (UserToken.ExpireAt > DateTime.UtcNow) 
                    return UserToken.UserId;
            }
            return null;
        }
    }
}
