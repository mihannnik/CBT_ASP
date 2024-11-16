using CBT.Application.Interfaces;

namespace CBT.Infrastructure.Services
{
    public class PasswordHasher : IPasswordHasher
    {
        public string Hash(string Password)
        {
            return BCrypt.Net.BCrypt.EnhancedHashPassword(Password);
        }

        public bool Verify(string Password, string HashedPassword)
        {
            return BCrypt.Net.BCrypt.EnhancedVerify(Password, HashedPassword);
        }
    }
}
