using CBT.Domain.Interfaces;
using CBT.Domain.Models;
using CBT.Domain.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CBT.Application.Services
{
    public class EventsService(
        IEventsRepository Repository, 
        IAuthRepository authRepository
        ) : IEventsService
    {
        public Event? Create(CreateEventRequest createRequest, Guid UserId)
        {
            if (authRepository.GetUser(UserId) is User user)
            {
                Event @event = new Event
                {
                    Title = createRequest.Title,
                    Description = createRequest.Description,
                    Date = createRequest.Date,
                    UserLimit = createRequest.JoinLimit,
                    Owner = user
                };
                Repository.Create(@event);
                return @event;
            }
            return null;
        }

        public bool DeleteEvent(int id, Guid UserId)
        {
            if (authRepository.GetUser(UserId) is User user)
            {
                Repository.Delete(@id);
                return true;
            }
            return false;
        }
        public bool ModifyEvent(ModifyEventRequest modifyRequest, Guid UserId)
        {
            if (authRepository.GetUser(UserId) is User user)
            {
                Event @event = new Event
                {
                    Id = modifyRequest.Id,
                    Title = modifyRequest.Title,
                    Description = modifyRequest.Description,
                    Date = modifyRequest.Date,
                    UserLimit = modifyRequest.JoinLimit,
                    OwnerId = user.Id,
                    Owner = user
                };
                Repository.Update(@event);
                return true;
            }
            return false;
        }
        public bool Join(int id, Guid UserId)
        {
            if (Repository.GetEvent(id) is Event @event && authRepository.GetUser(UserId) is User user)
            {
                if (@event.OwnerId.CompareTo(UserId) == 0) return false;
                if (@event.UserLimit != -1 && @event.UserLimit <= @event.Users.Count) return false;
                if (@event.Users.Contains(user)) return false;
                @event.Users.Add(user);
                Repository.Update(@event);
                return true;
            }
            return false;
        }

        public bool Leave(int id, Guid UserId)
        {
            if (Repository.GetEvent(id) is Event @event && authRepository.GetUser(UserId) is User user)
            {
                if (@event.OwnerId.CompareTo(UserId) == 0) return false;
                if (!@event.Users.Contains(user)) return false;
                @event.Users = @event.Users.Where(u => u.Id != user.Id).ToList();
                Repository.Update(@event);
                return true;
            }
            return false;
        }

        public ICollection<Event> GetEvents()
        {
            return Repository.GetEvents().ToList();
        }

        public Event? GetEvent(int id)
        {
            return Repository.GetEvent(id);
        }
    }
}
