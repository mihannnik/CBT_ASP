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
                .Include(ev => ev.Owner)
                .Include(ev => ev.Users)
                .FirstOrDefault();
        }

        public IEnumerable<Event> GetEvents()
        {
            return context.Events
                .Include(ev => ev.Owner)
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
