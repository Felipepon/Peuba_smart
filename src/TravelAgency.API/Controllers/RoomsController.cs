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

    // Crear una habitación en un hotel específico
    [HttpPost]
    public async Task<IActionResult> CreateRoom(
        Guid hotelId,
        [FromBody] CreateRoomCommand command)
    {
        command = command with { HotelId = hotelId };
        var roomId = await _mediator.Send(command);
        return CreatedAtAction(nameof(GetRoom), new { hotelId, roomId }, null);
    }

    // Actualizar una habitación
    [HttpPut("{roomId:guid}")]
    public async Task<IActionResult> UpdateRoom(
        Guid hotelId,
        Guid roomId,
        [FromBody] UpdateRoomCommand command)
    {
        command = command with { Id = roomId, HotelId = hotelId };
        await _mediator.Send(command);
        return NoContent();
    }

    // Habilitar/deshabilitar una habitación
    [HttpPatch("{roomId:guid}/toggle-status")]
    public async Task<IActionResult> ToggleRoomStatus(Guid hotelId, Guid roomId)
    {
        await _mediator.Send(new ToggleRoomStatusCommand(roomId));
        return NoContent();
    }
}