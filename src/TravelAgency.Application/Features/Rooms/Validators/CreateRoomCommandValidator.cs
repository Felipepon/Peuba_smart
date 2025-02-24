
using FluentValidation;
using TravelAgency.Application.Features.Rooms.Commands;

namespace TravelAgency.Application.Features.Rooms.Validators;

public class CreateRoomCommandValidator : AbstractValidator<CreateRoomCommand>
{
    public CreateRoomCommandValidator()
    {
        RuleFor(x => x.Type).IsInEnum().WithMessage("Invalid room type.");
        RuleFor(x => x.BaseCost).GreaterThan(0).WithMessage("Base cost must be greater than zero.");
        RuleFor(x => x.Taxes).GreaterThanOrEqualTo(0).WithMessage("Taxes must be greater than or equal to zero.");
        RuleFor(x => x.Location).NotEmpty().WithMessage("Location is required.");
    }
}