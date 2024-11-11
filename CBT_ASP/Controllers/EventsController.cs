using CBT.Domain.Interfaces;
using CBT.Domain.Models;
using CBT.Domain.Requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CBT.Web.Controllers
{
    [Route("events")]
    [Authorize]
    public class EventsController(
        IEventsService eventsService
        ): Controller
    {
        [HttpGet]
        [Authorize(Policy = "EventReadPolicy")]
        public IActionResult GetEvents()
        {
            return Ok(eventsService.GetEvents().Select(ev =>new VirtualEvent(ev)));
        }

        [Route("{id}")]
        [HttpGet]
        [Authorize(Policy = "EventReadPolicy")]
        public IActionResult GetEvent([FromRoute] int id)
        {
            if (eventsService.GetEvent(id) is Event ev)
            {
                return Ok(new VirtualEvent(ev));
            }
            return BadRequest();
        }

        [HttpPost]
        [Authorize(Policy = "EventCreatePolicy")]
        public IActionResult CreateEvent([FromBody] CreateEventRequest createRequest)
        {
            if (eventsService.Create(createRequest, Guid.Parse(User.FindFirst("Id")?.Value??"")) is Event ev)
            {
                return Ok(ev.Id);
            }
            return BadRequest();
        }

        [HttpPut]
        [Authorize(Policy = "EventModifyPolicy")]
        public IActionResult ModifyEvent([FromBody] ModifyEventRequest modifyRequest)
        {
            if (eventsService.ModifyEvent(modifyRequest, Guid.Parse(User.FindFirst("Id")?.Value ?? "")))
            {
                return Ok();
            }
            return BadRequest();
        }

        [Route("{id}")]
        [HttpDelete]
        [Authorize(Policy = "EventDeletePolicy")]
        public IActionResult DeleteEvent([FromRoute] int id)
        {
            if (eventsService.DeleteEvent(id, Guid.Parse(User.FindFirst("Id")?.Value ?? "")))
            {
                return Ok();
            }
            return BadRequest();
        }
        [Route("join/{id}")]
        [HttpGet]
        [Authorize(Policy = "EventJoinPolicy")]
        public IActionResult JoinEvent([FromRoute] int id)
        {
            if (eventsService.Join(id, Guid.Parse(User.FindFirst("Id")?.Value ?? "")))
            {
                return Ok();
            }
            return BadRequest();
        }

        [Route("leave/{id}")]
        [HttpGet]
        [Authorize(Policy = "EventJoinPolicy")]
        public IActionResult LeaveEvent([FromRoute] int id)
        {
            if (eventsService.Leave(id, Guid.Parse(User.FindFirst("Id")?.Value ?? "")))
            {
                return Ok();
            }
            return BadRequest();
        }
    }
}
