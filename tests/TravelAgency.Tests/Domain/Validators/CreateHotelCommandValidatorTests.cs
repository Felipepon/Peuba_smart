// tests/TravelAgency.Tests/Domain/Validators/CreateHotelCommandValidatorTests.cs
using Xunit;
using FluentValidation.TestHelper;
using TravelAgency.Application.Features.Hotels.Commands;
using TravelAgency.Application.Features.Hotels.Validators;

public class CreateHotelCommandValidatorTests
{
    private readonly CreateHotelCommandValidator _validator = new();

    [Fact]
    public void Should_Have_Error_When_Name_Is_Empty()
    {
        // Arrange
        var command = new CreateHotelCommand("", "Paris");

        // Act
        var result = _validator.TestValidate(command);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.Name);
    }
}