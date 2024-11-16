using CBT.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace CBT.Application.Interfaces
{
    public interface ITokenProvider
    {
        public string GenerateToken(Claim[] claims, bool isRefresh = false);
        public string GenerateToken(Claim[] claims, out DateTime expireAt, bool isRefresh = false);
    }
}
