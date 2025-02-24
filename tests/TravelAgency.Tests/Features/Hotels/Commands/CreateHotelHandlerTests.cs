// tests/TravelAgency.Tests/Features/Hotels/Commands/CreateHotelHandlerTests.cs
using Moq;
using System;
using System.Threading;
using System.Threading.Tasks;
using TravelAgency.Application.Features.Hotels.Commands;
using TravelAgency.Application.Features.Hotels.Handlers;
using TravelAgency.Domain.Entities;
using TravelAgency.Domain.Interfaces;
using Xunit;

public class CreateHotelHandlerTests
{
    [Fact]
    public async Task Handle_ValidCommand_ReturnsHotelId()
    {
        // Arrange
        var mockRepository = new Mock<IHotelRepository>();
        var handler = new CreateHotelHandler(mockRepository.Object);
        var command = new CreateHotelCommand("Test Hotel", "Test City");

        mockRepository.Setup(repo => repo.AddAsync(It.IsAny<Hotel>()))
            .Callback<Hotel>(hotel => hotel.Id = Guid.NewGuid())
            .Returns(Task.CompletedTask);

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.NotEqual(Guid.Empty, result);
    }
}