using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CBT.Domain.Models
{
    public class Event
    {
        public int Id { get; set; }
        public required string Title { get; set; }
        public required string Description { get; set; }
        public required DateTime Date { get; set; }
        public int UserLimit { get; set; } = -1;
        public Guid OwnerId { get; set; }
        public required User Owner { get; set; }
        public ICollection<User> Users { get; set; } = new List<User>();
    }
}
