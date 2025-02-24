// tests/TravelAgency.Tests/Features/Bookings/Handlers/GetBookingDetailsHandlerTests.cs
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using TravelAgency.Application.DTOs;
using TravelAgency.Application.Features.Bookings.Handlers;
using TravelAgency.Application.Features.Bookings.Queries;
using TravelAgency.Domain.Entities;
using TravelAgency.Domain.Enums;
using TravelAgency.Domain.Interfaces;
using Xunit;

namespace TravelAgency.Tests.Features.Bookings.Handlers
{
    public class GetBookingDetailsHandlerTests
    {
        private readonly Mock<IBookingRepository> _mockRepository;
        private readonly GetBookingDetailsHandler _handler;

        public GetBookingDetailsHandlerTests()
        {
            _mockRepository = new Mock<IBookingRepository>();
            _handler = new GetBookingDetailsHandler(_mockRepository.Object);
        }

        [Fact]
        public async Task Handle_ValidQuery_ReturnsBookingDetails()
        {
            // Arrange
            var bookingId = Guid.NewGuid();
            var query = new GetBookingDetailsQuery(bookingId);

            var booking = new Booking
            {
                Id = bookingId,
                CheckInDate = DateTime.Now,
                CheckOutDate = DateTime.Now.AddDays(1),
                TotalCost = 200,
                Room = new Room
                {
                    Hotel = new Hotel { Name = "Test Hotel" },
                    Type = RoomType.Standard
                },
                Guests = new List<Guest>
                {
                    new Guest
                    {
                        FullName = "John Doe",
                        BirthDate = new DateTime(1990, 1, 1),
                        Gender = Gender.Male,
                        DocumentType = DocumentType.Passport,
                        DocumentNumber = "123456789",
                        Email = "john.doe@example.com",
                        Phone = "1234567890"
                    }
                },
                EmergencyContact = new EmergencyContact
                {
                    FullName = "Jane Doe",
                    Phone = "0987654321"
                }
            };

            _mockRepository.Setup(repo => repo.GetByIdAsync(bookingId, It.IsAny<Expression<Func<Booking, object>>[]>()))
                .ReturnsAsync(booking);

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(booking.Id, result.Id);
            Assert.Equal(booking.CheckInDate, result.CheckInDate);
            Assert.Equal(booking.CheckOutDate, result.CheckOutDate);
            Assert.Equal(booking.TotalCost, result.TotalCost);
            Assert.Equal(booking.Room.Hotel.Name, result.HotelName);
            Assert.Equal(booking.Room.Type.ToString(), result.RoomType);
            Assert.Single(result.Guests);
            Assert.Equal(booking.Guests.First().FullName, result.Guests.First().FullName);
            Assert.Equal(booking.EmergencyContact.FullName, result.EmergencyContact.FullName);
        }

        [Fact]
        public async Task Handle_InvalidBookingId_ReturnsNull()
        {
            // Arrange
            var bookingId = Guid.NewGuid();
            var query = new GetBookingDetailsQuery(bookingId);

            _mockRepository.Setup(repo => repo.GetByIdAsync(bookingId, It.IsAny<Expression<Func<Booking, object>>[]>()))
                .ReturnsAsync((Booking)null);

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.Null(result);
        }
    }
}