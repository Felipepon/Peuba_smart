// tests/TravelAgency.Tests/Features/Hotels/HotelsControllerTests.cs
using Xunit;
using Moq;
using Microsoft.AspNetCore.Mvc;
using TravelAgency.API.Controllers;
using MediatR;
using TravelAgency.Application.Features.Hotels.Commands;

public class HotelsControllerTests
{
    [Fact]
    public async Task CreateHotel_Returns_CreatedResult()
    {
        // Arrange
        var mockMediator = new Mock<IMediator>();
        mockMediator.Setup(m => m.Send(It.IsAny<CreateHotelCommand>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(Guid.NewGuid());

        var controller = new HotelsController(mockMediator.Object);
        var command = new CreateHotelCommand("Test Hotel", "Madrid");

        // Act
        var result = await controller.CreateHotel(command);

        // Assert
        Assert.IsType<CreatedAtActionResult>(result);
    }
}