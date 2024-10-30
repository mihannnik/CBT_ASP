using CBT.Domain.Models;
using CBT.Domain.Requests;

namespace CBT.Domain.Interfaces
{
    public interface IAuthService
    {
        public void Register(RegisterRequest register);
        public User? GetAuthUser(LoginRequest login);
        public string GetAuthToken(User user);
        public string GetRefreshToken(User user);
        public string RefreshToken();
    }
}
