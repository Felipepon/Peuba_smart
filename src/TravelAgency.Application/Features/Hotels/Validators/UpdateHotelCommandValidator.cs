using FluentValidation;
using MediatR;
using TravelAgency.Application.Features.Hotels.Commands;
using TravelAgency.Domain.Entities;
using TravelAgency.Domain.Interfaces;

namespace TravelAgency.Application.Features.Hotels.Validators;

public class UpdateHotelCommandValidator : AbstractValidator<UpdateHotelCommand>
{
    public UpdateHotelCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("El ID del hotel es obligatorio");

        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("El nombre del hotel es obligatorio")
            .MaximumLength(100).WithMessage("El nombre no puede exceder 100 caracteres");

        RuleFor(x => x.City)
            .NotEmpty().WithMessage("La ciudad es obligatoria")
            .MaximumLength(50).WithMessage("La ciudad no puede exceder 50 caracteres");
    }
}