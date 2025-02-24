
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TravelAgency.Application.DTOs;
using TravelAgency.Application.Features.Bookings.Commands;
using TravelAgency.Application.Features.Bookings.Handlers;
using TravelAgency.Domain.Entities;
using TravelAgency.Domain.Enums;
using TravelAgency.Domain.Interfaces;
using Xunit;

namespace TravelAgency.Tests.Features.Bookings.Handlers
{
    public class CreateBookingHandlerTests
    {
        private readonly Mock<IBookingRepository> _mockBookingRepository;
        private readonly Mock<IRoomRepository> _mockRoomRepository;
        private readonly Mock<IEmailService> _mockEmailService;
        private readonly CreateBookingHandler _handler;

        public CreateBookingHandlerTests()
        {
            _mockBookingRepository = new Mock<IBookingRepository>();
            _mockRoomRepository = new Mock<IRoomRepository>();
            _mockEmailService = new Mock<IEmailService>();
            _handler = new CreateBookingHandler(_mockBookingRepository.Object, _mockRoomRepository.Object, _mockEmailService.Object);
        }

        [Fact]
        public async Task Handle_ValidCommand_CreatesBooking()
        {
            // Arrange
            var roomId = Guid.NewGuid();
            var command = new CreateBookingCommand(
                roomId,
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

            var room = new Room
            {
                Id = roomId,
                BaseCost = 100,
                Taxes = 10,
                IsEnabled = true
            };

            _mockRoomRepository.Setup(repo => repo.GetByIdAsync(roomId))
                .ReturnsAsync(room);

            _mockBookingRepository.Setup(repo => repo.GetBookingsByRoomAsync(roomId))
                .ReturnsAsync(new List<Booking>());

            _mockBookingRepository.Setup(repo => repo.AddAsync(It.IsAny<Booking>()))
                .Returns(Task.CompletedTask);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.NotEqual(Guid.Empty, result);
            _mockBookingRepository.Verify(repo => repo.AddAsync(It.IsAny<Booking>()), Times.Once);
            _mockEmailService.Verify(service => service.SendBookingConfirmationAsync(It.IsAny<string>(), It.IsAny<Booking>()), Times.Once);
        }

        [Fact]
        public async Task Handle_InvalidRoomId_ThrowsKeyNotFoundException()
        {
            
            var roomId = Guid.NewGuid();
            var command = new CreateBookingCommand(
                roomId,
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

            _mockRoomRepository.Setup(repo => repo.GetByIdAsync(roomId))
                .ReturnsAsync((Room)null);

            
            await Assert.ThrowsAsync<KeyNotFoundException>(() => _handler.Handle(command, CancellationToken.None));
        }
    }
}