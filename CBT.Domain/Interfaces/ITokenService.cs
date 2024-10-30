using CBT.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace CBT.Domain.Interfaces
{
    public interface ITokenProvider
    {
        public string GenerateToken(Claim[] claims);
        public string GenerateRefreshToken(Claim[] claims);
    }
}
