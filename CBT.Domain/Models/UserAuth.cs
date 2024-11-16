using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CBT.Domain.Models.Enums;

namespace CBT.Domain.Models
{
    public class UserAuth
    {
        public int Id;
        public Guid UserId { get; set; }
        public required string Identity { get; set; }
        public string PasswordHash { get; set; } = null!;
        public IdentityTypes Type { get; set; }
        public User? User { get; set; }
    }
}
