using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CBT.Domain.Models
{
    public class VirtualEvent
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
        public int UserLimit { get; set; } = -1;
        public VirtualUser Owner { get; set; }
        public ICollection<VirtualUser> Users { get; set; } = new List<VirtualUser>();
        public VirtualEvent(Event @event)
        {
            this.Id = @event.Id;
            this.Title = @event.Title;
            this.Description = @event.Description;
            this.Date = @event.Date;
            this.UserLimit = @event.UserLimit;
            this.Owner = (VirtualUser)@event.Owner;
            this.Users = @event.Users.Select(u => (VirtualUser)u).ToList();
        }
        public VirtualEvent(int id, string title, string description, DateTime date, VirtualUser owner, List<VirtualUser> users, int userLimit = -1)
        {
            this.Id = id;
            this.Title = title;
            this.Description = description;
            this.Date = date;
            this.Owner = owner;
            this.Users = users;
            this.UserLimit = userLimit;
        }
        public VirtualEvent(int id, string title, string description, DateTime date, User owner, List<User> users, int userLimit = -1)
        {
            this.Id = id;
            this.Title = title;
            this.Description = description;
            this.Date = date;
            this.Owner = (VirtualUser)owner;
            this.Users = users.Select(u => (VirtualUser)u).ToList();
            this.UserLimit = userLimit;
        }
    }
}
