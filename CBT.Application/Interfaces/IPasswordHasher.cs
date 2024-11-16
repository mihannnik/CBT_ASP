using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CBT.Application.Interfaces
{
    public interface IPasswordHasher
    {
        string Hash(string Password);
        bool Verify(string Password, string HashedPassword);
    }
}
