// tests/TravelAgency.Tests/Features/Hotels/Handlers/GetHotelByIdHandlerTests.cs
using Moq;
using System;
using System.Threading;
using System.Threading.Tasks;
using TravelAgency.Application.Features.Hotels.Handlers;
using TravelAgency.Application.Features.Hotels.Queries;
using TravelAgency.Domain.Entities;
using TravelAgency.Domain.Interfaces;
using Xunit;

namespace TravelAgency.Tests.Features.Hotels.Handlers
{
    public class GetHotelByIdHandlerTests
    {
        [Fact]
        public async Task Handle_ValidId_ReturnsHotel()
        {
            // Arrange
            var mockRepository = new Mock<IHotelRepository>();
            var handler = new GetHotelByIdHandler(mockRepository.Object);
            var query = new GetHotelByIdQuery(Guid.NewGuid());
            var hotel = new Hotel("Test Hotel", "Test City");

            mockRepository.Setup(repo => repo.GetByIdAsync(query.Id))
                .ReturnsAsync(hotel);

            // Act
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(hotel, result);
        }

        [Fact]
        public async Task Handle_InvalidId_ReturnsNull()
        {
            // Arrange
            var mockRepository = new Mock<IHotelRepository>();
            var handler = new GetHotelByIdHandler(mockRepository.Object);
            var query = new GetHotelByIdQuery(Guid.NewGuid());

            mockRepository.Setup(repo => repo.GetByIdAsync(query.Id))
                .ReturnsAsync((Hotel?)null);

            // Act
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.Null(result);
        }
    }
}