using CBT.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CBT.Application.Interfaces
{
    public interface IEventsRepository
    {
        public Event? GetEvent(int id);
        public IEnumerable<Event> GetEvents();
        public void Create(Event @event);
        public void Update(Event @event);
        public void Delete(int id);
    }
}
