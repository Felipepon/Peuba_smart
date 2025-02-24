using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using TravelAgency.Application.Features.Bookings.Commands;
using TravelAgency.Application.Features.Bookings.Queries;

namespace TravelAgency.API.Controllers
{
    [ApiController]
    [Route("api/bookings")]
    public class BookingsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public BookingsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [Authorize(Roles = "Traveler")]
        public async Task<IActionResult> CreateBooking([FromBody] CreateBookingCommand command)
        {
            var bookingId = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetBooking), new { id = bookingId }, null);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetBooking(Guid id)
        {
            var booking = await _mediator.Send(new GetBookingQuery(id));
            return Ok(booking);
        }

        [HttpGet("available-rooms")]
        public async Task<IActionResult> GetAvailableRooms([FromQuery] GetAvailableRoomsQuery query)
        {
            var rooms = await _mediator.Send(query);
            return Ok(rooms);
        }

        [HttpGet("hotel/{hotelId}")]
        [Authorize(Roles = "Agent")]
        public async Task<IActionResult> GetBookingsByHotel(Guid hotelId)
        {
            var bookings = await _mediator.Send(new GetBookingsByHotelQuery(hotelId));
            return Ok(bookings);
        }

        [HttpDelete("{bookingId}")]
        [Authorize(Roles = "Traveler,Agent")]
        public async Task<IActionResult> CancelBooking(Guid bookingId)
        {
            await _mediator.Send(new CancelBookingCommand(bookingId));
            return NoContent();
        }
    }
}