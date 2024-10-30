using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CBT.Domain.Models
{
    public class UserAuth
    {
        public Guid UserId { get; set; }
        public required string Identity { get; set; }
        public string? PasswordHash { get; set; }
        public IdentityTypes Type { get; set; }
        public User User { get; set; }
    }
}
