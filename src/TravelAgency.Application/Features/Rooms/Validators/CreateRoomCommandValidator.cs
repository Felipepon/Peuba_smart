using FluentValidation;
using TravelAgency.Application.Features.Rooms.Commands;

namespace TravelAgency.Application.Features.Rooms.Validators;

public class CreateRoomCommandValidator : AbstractValidator<CreateRoomCommand>
{
    public CreateRoomCommandValidator()
    {
        RuleFor(x => x.HotelId).NotEmpty();
        RuleFor(x => x.Type).IsInEnum();
        RuleFor(x => x.Taxes).GreaterThanOrEqualTo(0);
        RuleFor(x => x.Location).NotEmpty().MaximumLength(100);
    }
}