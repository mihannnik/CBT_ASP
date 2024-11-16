using CBT.Domain.Models;
using CBT.Domain.Requests;

namespace CBT.Application.Interfaces
{
    public interface IAuthService
    {
        public void Register(RegisterRequest register);
        public User? GetAuthUser(LoginRequest login);
        public string CreateAuthToken(User user);
        public string CreateRefreshToken(User user);
        public User? UseRefreshToken(string token);
    }
}
