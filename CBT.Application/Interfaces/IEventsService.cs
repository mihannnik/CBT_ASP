using CBT.Domain.Models;
using CBT.Domain.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CBT.Application.Interfaces
{
    public interface IEventsService
    {
        public ICollection<Event> GetEvents();
        public Event? GetEvent(int id);
        public Event? Create(CreateEventRequest createRequest, Guid UserId);
        public bool ModifyEvent(ModifyEventRequest modifyRequest, Guid UserId);
        public bool DeleteEvent(int id, Guid UserId);
        public bool Join(int id, Guid UserId);
        public bool Leave(int id, Guid UserId);
    }
}
