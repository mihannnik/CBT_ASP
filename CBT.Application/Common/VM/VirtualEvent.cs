using CBT.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CBT.Application.Common.VM
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
            Id = @event.Id;
            Title = @event.Title;
            Description = @event.Description;
            Date = @event.Date;
            UserLimit = @event.UserLimit;
            Owner = (VirtualUser)@event.Owner;
            Users = @event.Users.Select(u => (VirtualUser)u).ToList();
        }
        public VirtualEvent(int id, string title, string description, DateTime date, VirtualUser owner, List<VirtualUser> users, int userLimit = -1)
        {
            Id = id;
            Title = title;
            Description = description;
            Date = date;
            Owner = owner;
            Users = users;
            UserLimit = userLimit;
        }
        public VirtualEvent(int id, string title, string description, DateTime date, User owner, List<User> users, int userLimit = -1)
        {
            Id = id;
            Title = title;
            Description = description;
            Date = date;
            Owner = (VirtualUser)owner;
            Users = users.Select(u => (VirtualUser)u).ToList();
            UserLimit = userLimit;
        }
    }
}
