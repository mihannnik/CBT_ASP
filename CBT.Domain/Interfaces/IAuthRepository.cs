using CBT.Domain.Models;
using CBT.Domain.Models.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CBT.Domain.Interfaces
{
    public interface IAuthRepository
    {
        public User CreateUser(string Name, ICollection<UserAuth> auths, string? Username = null, Role Role = Role.User);
        public UserAuth? GetAuthUser(string Identity);
        public User? GetUser(Guid id);
    }
}
