using CBT.Domain.Interfaces;
using CBT.Domain.Models;
using CBT.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace CBT.Infrastructure.Repositories
{
    public class AuthRepository(SQLiteDbContext context) : IAuthRepository
    {
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
            context.SaveChangesAsync();
            return user;
        }

        public UserAuth? GetUser(string Identity)
        {
            return context.Auth.Include(a => a.User).Where(a => a.Identity == Identity && (a.Type == IdentityTypes.Name || a.Type == IdentityTypes.Email)).FirstOrDefault();
            //return context.Users
                //.Include(u => u.UserAuth)
                //.Where(u => u.UserAuth.Any() && u.UserAuth.Any(a => a.Identity == Identity)).FirstOrDefault();
        }
    }
}
