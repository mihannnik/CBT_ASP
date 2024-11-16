using CBT.Application.Common.VM;
using CBT.Application.Interfaces;
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
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public IActionResult GetEvents() => Ok(eventsService.GetEvents().Select(ev => new VirtualEvent(ev)));

        [Route("{id}")]
        [HttpGet]
        [Authorize(Policy = "EventReadPolicy")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult GetEvent([FromRoute] int id) 
            => eventsService.GetEvent(id) is Event ev 
                ? Ok(new VirtualEvent(ev)) 
                : BadRequest();

        [HttpPost]
        [Authorize(Policy = "EventCreatePolicy")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult CreateEvent([FromBody] CreateEventRequest createRequest, 
            [FromServices] IUser user) 
            => user.Id is Guid id 
            && eventsService.Create(createRequest, id) is Event ev
                ? Ok(ev.Id)
                : (IActionResult)BadRequest();

        [HttpPut]
        [Authorize(Policy = "EventModifyPolicy")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult ModifyEvent(
            [FromBody] ModifyEventRequest modifyRequest, 
            [FromServices] IUser user, 
            CancellationToken cancellationToken) 
            => user.Id is Guid id 
            && eventsService.ModifyEvent(modifyRequest, id) 
                ? Ok() 
                : BadRequest();

        [Route("{eventId}")]
        [HttpDelete]
        [Authorize(Policy = "EventDeletePolicy")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult DeleteEvent([FromRoute] int eventId, 
            [FromServices] IUser user) 
            => user.Id is Guid id 
            && eventsService.DeleteEvent(eventId, id) 
                ? Ok() 
                : BadRequest();
        [Route("join/{eventId}")]
        [HttpGet]
        [Authorize(Policy = "EventJoinPolicy")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult JoinEvent([FromRoute] int eventId, 
            [FromServices] IUser user) 
            => user.Id is Guid id 
            && eventsService.Join(eventId, id) 
                ? Ok() 
                : BadRequest();

        [Route("leave/{eventId}")]
        [HttpGet]
        [Authorize(Policy = "EventJoinPolicy")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult LeaveEvent([FromRoute] int eventId, 
            [FromServices] IUser user) 
            => user.Id is Guid id 
            && eventsService.Leave(eventId, id) 
                ? Ok() 
                : BadRequest();
    }
}
