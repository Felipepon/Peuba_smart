using MediatR;
using Microsoft.AspNetCore.Mvc;
using TravelAgency.Application.Features.Hotels.Commands;
using TravelAgency.Application.Features.Hotels.Queries;

namespace TravelAgency.API.Controllers;

[ApiController]
[Route("api/hotels")]
public class HotelsController : ControllerBase
{
    private readonly IMediator _mediator;

    public HotelsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    [Authorize(Roles = "Agent")]
    public async Task<IActionResult> CreateHotel([FromBody] CreateHotelCommand command)
    {
        var hotelId = await _mediator.Send(command);
        return CreatedAtAction(nameof(GetHotel), new { id = hotelId }, null);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetHotel(Guid id)
    {
        var hotel = await _mediator.Send(new GetHotelByIdQuery(id));
        return Ok(hotel);
    }

    [HttpPut("{id:guid}")]
    [Authorize(Roles = "Agent")]
    public async Task<IActionResult> UpdateHotel(
        Guid id,
        [FromBody] UpdateHotelCommand command)
    {
        command = command with { Id = id };
        await _mediator.Send(command);
        return NoContent();
    }

    [HttpPatch("{id:guid}/toggle-status")]
    [Authorize(Roles = "Agent")]
    public async Task<IActionResult> ToggleHotelStatus(Guid id)
    {
        await _mediator.Send(new ToggleHotelStatusCommand(id));
        return NoContent();
    }
}