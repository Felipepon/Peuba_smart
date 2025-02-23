using FluentValidation;
using TravelAgency.Application.Features.Rooms.Commands;

namespace TravelAgency.Application.Features.Rooms.Validators;

public class UpdateRoomCommandValidator : AbstractValidator<UpdateRoomCommand>
{
    public UpdateRoomCommandValidator()
    {
        RuleFor(x => x.Id).NotEmpty();
        RuleFor(x => x.HotelId).NotEmpty();
        RuleFor(x => x.Taxes).GreaterThanOrEqualTo(0);
        RuleFor(x => x.Location).NotEmpty().MaximumLength(100);
    }
}