using CBT.Domain.Models;
using CBT.Domain.Models.Enums;

namespace CBT.Application.Interfaces
{
    public interface IAuthRepository
    {
        public User CreateUser(string Name, ICollection<UserAuth> auths, string? Username = null, Role Role = Role.User);
        public UserAuth? GetAuthUser(string Identity);
        public User? GetUser(Guid id);
        public UserRefreshToken AddRefreshToken(Guid id, string token, DateTime expirateDate);
        public Guid? UseRefreshToken(string token);
    }
}
