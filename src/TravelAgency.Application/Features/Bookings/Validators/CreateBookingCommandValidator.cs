using FluentValidation;
using TravelAgency.Application.Features.Bookings.Commands;

namespace TravelAgency.Application.Features.Bookings.Validators;

public class CreateBookingCommandValidator : AbstractValidator<CreateBookingCommand>
{
    public CreateBookingCommandValidator()
    {
        RuleFor(x => x.RoomId).NotEmpty();
        RuleFor(x => x.CheckInDate)
            .NotEmpty()
            .GreaterThan(DateTime.Now.AddDays(-1));
        RuleFor(x => x.CheckOutDate)
            .NotEmpty()
            .GreaterThan(x => x.CheckInDate);
        RuleFor(x => x.Guests)
            .NotEmpty()
            .Must(g => g.Count > 0 && g.Count <= 6)
            .WithMessage("Debe haber entre 1 y 6 huéspedes");
        RuleFor(x => x.EmergencyContact).NotNull();
    }
}