using CBT.Domain.Interfaces;
using CBT.Domain.Models;
using CBT.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CBT.Infrastructure.Repositories
{
    public class EventsRepository(SQLiteDbContext context) : IEventsRepository
    {
        public void Create(Event @event)
        {
            context.Events.Add(@event);
            context.SaveChanges();
        }

        public void Delete(int id)
        {
            context.Events
                .Where(ev => ev.Id == id)
                .ExecuteDeleteAsync();
            context.SaveChanges();
        }

        public Event? GetEvent(int id)
        {
            return context.Events
                .Where(ev => ev.Id == id)
                .Select(ev => new Event
                {
                    Id = ev.Id,
                    Title = ev.Title,
                    Description = ev.Description,
                    Date = ev.Date,
                    Owner = ev.Users
                    .Where(u => u.Id == ev.OwnerId)
                    .Select(u => new User
                    {
                        Id = u.Id,
                        Name = u.Name,
                        Username = u.Name
                    })
                    .First(),
                    Users = ev.Users.Select(u => new User
                    {
                        Id = u.Id,
                        Name = u.Name,
                        Username = u.Username
                    }).ToList()
                })
                .FirstOrDefault();
        }

        public IEnumerable<Event> GetEvents()
        {
            return context.Events
                .Select(ev => new Event
                { 
                    Id = ev.Id,
                    Title = ev.Title,
                    Description = ev.Description,
                    Date = ev.Date,
                    Owner = ev.Users.Where(u => u.Id == ev.OwnerId)
                    .Select(u => new User
                    { 
                        Id = u.Id,
                        Name = u.Name,
                        Username = u.Name
                    })
                    .First(),
                    Users = ev.Users.Select(u => new User
                    {
                        Id = u.Id,
                        Name = u.Name,
                        Username = u.Username
                    }).ToList()
                })
                .Include(ev => ev.Users);
        }

        public void Update(Event @event)
        {
            if (context.Events.FirstOrDefault(ev => ev.Id == @event.Id) is Event tempEvent)
            { 
                context.Entry(tempEvent).CurrentValues.SetValues(@event);
                context.SaveChanges();
            }
        }
    }
}
