// // tests/TravelAgency.Tests/Domain/Validators/CreateRoomCommandValidatorTests.cs
// using FluentValidation.TestHelper;
// using System;
// using TravelAgency.Application.Features.Rooms.Commands;
// using TravelAgency.Application.Features.Rooms.Validators;
// using TravelAgency.Domain.Enums;
// using Xunit;

// namespace TravelAgency.Tests.Domain.Validators
// {
//     public class CreateRoomCommandValidatorTests
//     {
//         private readonly CreateRoomCommandValidator _validator = new();

//         [Fact]
//         public void ShouldFail_WhenHotelIdIsEmpty()
//         {
//             // Arrange
//             var command = new CreateRoomCommand(
//                 Guid.Empty, 
//                 RoomType.Standard,
//                 10,
//                 "Piso 3"
//             );

//             // Act
//             var result = _validator.TestValidate(command);

//             // Assert
//             result.ShouldHaveValidationErrorFor(c => c.HotelId)
//                 .WithErrorMessage("El ID del hotel es obligatorio.");
//         }

//         [Theory]
//         [InlineData(0)]
//         [InlineData(-50)]
//         public void ShouldFail_WhenTaxesAreInvalid(decimal invalidTaxes)
//         {
//             // Arrange
//             var command = new CreateRoomCommand(
//                 Guid.NewGuid(),
//                 RoomType.Standard,
//                 invalidTaxes, 
//                 "Piso 3"
//             );

//             // Act
//             var result = _validator.TestValidate(command);

//             // Assert
//             result.ShouldHaveValidationErrorFor(c => c.Taxes)
//                 .WithErrorMessage("Los impuestos deben ser mayores o iguales a 0.");
//         }

//         [Fact]
//         public void ShouldPass_WhenCommandIsValid()
//         {
//             // Arrange
//             var command = new CreateRoomCommand(
//                 Guid.NewGuid(),
//                 RoomType.Standard,
//                 10,
//                 "Piso 3"
//             );

//             // Act
//             var result = _validator.TestValidate(command);

//             // Assert
//             result.ShouldNotHaveAnyValidationErrors();
//         }
//     }
// }