using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CBT.Domain.Models.Auth;

namespace CBT.Domain.Models
{
    public class User
    {
        public Guid Id { get; set; }
        public required string Name { get; set; }
        public string? Username { get; set; }
        public Role Role { get; set; }
        public ICollection<UserAuth> UserAuth { get; set; } = new List<UserAuth>();
        public ICollection<Event> EventsOwner { get; set; } = new List<Event>();
        public ICollection<Event> Events { get; set; } = new List<Event>();
    }
}
