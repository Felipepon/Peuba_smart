
using Xunit;
using Moq;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using TravelAgency.API.Controllers;
using TravelAgency.Application.DTOs;
using TravelAgency.Application.Features.Bookings.Commands;
using TravelAgency.Application.Features.Bookings.Queries;
using MediatR;

namespace TravelAgency.Tests.Features.Bookings.Controllers
{
    public class BookingsControllerTests
    {
        private readonly Mock<IMediator> _mockMediator;
        private readonly BookingsController _controller;

        public BookingsControllerTests()
        {
            _mockMediator = new Mock<IMediator>();
            _controller = new BookingsController(_mockMediator.Object);
        }

        [Fact]
        public async Task CreateBooking_ValidCommand_ReturnsCreatedResult()
        {
            // Arrange
            var bookingId = Guid.NewGuid();
            var command = new CreateBookingCommand(
                Guid.NewGuid(),
                DateTime.Now,
                DateTime.Now.AddDays(1),
                new List<GuestDto>
                {
                    new GuestDto(
                        "John Doe",
                        new DateTime(1990, 1, 1),
                        "Male",
                        "Passport",
                        "123456789",
                        "john.doe@example.com",
                        "1234567890"
                    )
                },
                new EmergencyContactDto(
                    "Jane Doe",
                    "0987654321"
                )
            );

            _mockMediator.Setup(m => m.Send(command, It.IsAny<CancellationToken>()))
                .ReturnsAsync(bookingId);

            // Act
            var result = await _controller.CreateBooking(command);

            // Assert
            var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result);
            Assert.Equal(nameof(BookingsController.GetBooking), createdAtActionResult.ActionName);
            Assert.Equal(bookingId, createdAtActionResult.RouteValues["id"]);
        }

        [Fact]
        public async Task GetBooking_ExistingId_ReturnsOkResult()
        {
            // Arrange
            var bookingId = Guid.NewGuid();
            var booking = new BookingDetailsResponse(
                bookingId,
                DateTime.Now,
                DateTime.Now.AddDays(1),
                200,
                "Test Hotel",
                "Standard",
                new List<GuestResponse>
                {
                    new GuestResponse(
                        "John Doe",
                        new DateTime(1990, 1, 1),
                        "Male",
                        "Passport",
                        "123456789",
                        "john.doe@example.com",
                        "1234567890"
                    )
                },
                new EmergencyContactResponse(
                    "Jane Doe",
                    "0987654321"
                )
            );

            _mockMediator.Setup(m => m.Send(It.IsAny<GetBookingQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(booking);

            // Act
            var result = await _controller.GetBooking(bookingId);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(booking, okResult.Value);
        }

        
    }
}