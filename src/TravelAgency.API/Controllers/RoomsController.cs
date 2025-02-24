// src/TravelAgency.API/Controllers/RoomsController.cs
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using TravelAgency.Application.Features.Rooms.Commands;
using TravelAgency.Application.Features.Rooms.Queries;

namespace TravelAgency.API.Controllers
{
    [ApiController]
    [Route("api/hotels/{hotelId}/rooms")]
    [Authorize(Roles = "Agent")]
    public class RoomsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public RoomsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> CreateRoom(Guid hotelId, [FromBody] CreateRoomCommand command)
        {
            command = command with { HotelId = hotelId };
            var roomId = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetRoom), new { hotelId, roomId }, null);
        }

        [HttpGet("{roomId}")]
        public async Task<IActionResult> GetRoom(Guid hotelId, Guid roomId)
        {
            var room = await _mediator.Send(new GetRoomQuery(hotelId, roomId));
            return Ok(room);
        }

        [HttpPut("{roomId:guid}")]
        public async Task<IActionResult> UpdateRoom(Guid hotelId, Guid roomId, [FromBody] UpdateRoomCommand command)
        {
            command = command with { Id = roomId, HotelId = hotelId };
            await _mediator.Send(command);
            return NoContent();
        }

        [HttpPatch("{roomId:guid}/toggle-status")]
        public async Task<IActionResult> ToggleRoomStatus(Guid hotelId, Guid roomId)
        {
            await _mediator.Send(new ToggleRoomStatusCommand(roomId));
            return NoContent();
        }
    }
}