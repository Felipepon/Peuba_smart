// // tests/TravelAgency.Tests/Features/Rooms/RoomsControllerTest.cs
// using Microsoft.AspNetCore.Mvc;
// using Moq;
// using System;
// using System.Threading;
// using System.Threading.Tasks;
// using TravelAgency.Application.Features.Rooms.Commands;
// using TravelAgency.Application.Features.Rooms.Queries;
// using TravelAgency.API.Controllers;
// using TravelAgency.Domain.Entities;
// using TravelAgency.Domain.Enums;
// using MediatR;
// using Xunit;

// namespace TravelAgency.API.Tests.Controllers
// {
//     public class RoomsControllerTests
//     {
//         private readonly Mock<IMediator> _mockMediator;
//         private readonly RoomsController _controller;

//         public RoomsControllerTests()
//         {
//             _mockMediator = new Mock<IMediator>();
//             _controller = new RoomsController(_mockMediator.Object);
//         }

//         [Fact]
//         public async Task CreateRoom_ValidCommand_ReturnsCreatedResult()
//         {
//             // Arrange
//             var hotelId = Guid.NewGuid();
//             var command = new CreateRoomCommand(
//                 hotelId,
//                 RoomType.Standard,
//                 10,
//                 "Piso 3"
//             );

//             _mockMediator
//                 .Setup(m => m.Send(command, It.IsAny<CancellationToken>()))
//                 .ReturnsAsync(Guid.NewGuid());

//             // Act
//             var result = await _controller.CreateRoom(hotelId, command);

//             // Assert
//             var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result);
//             Assert.Equal(nameof(RoomsController.GetRoom), createdAtActionResult.ActionName);
//         }

//         [Fact]
//         public async Task CreateRoom_InvalidCommand_ReturnsBadRequest()
//         {
//             // Arrange
//             var hotelId = Guid.NewGuid();
//             var command = new CreateRoomCommand(
//                 hotelId,
//                 RoomType.Standard,
//                 0,
//                 "Piso 3"
//             );

//             _mockMediator
//                 .Setup(m => m.Send(command, It.IsAny<CancellationToken>()))
//                 .ThrowsAsync(new ArgumentException());

//             // Act
//             var result = await _controller.CreateRoom(hotelId, command);

//             // Assert
//             Assert.IsType<BadRequestObjectResult>(result);
//         }

//         [Fact]
//         public async Task GetRoom_ExistingId_ReturnsOkResult()
//         {
//             // Arrange
//             var hotelId = Guid.NewGuid();
//             var roomId = Guid.NewGuid();
//             var room = new Room
//             {
//                 Id = roomId,
//                 HotelId = hotelId,
//                 Type = RoomType.Standard,
//                 BaseCost = 100,
//                 Taxes = 10,
//                 Location = "Piso 3"
//             };

//             _mockMediator
//                 .Setup(m => m.Send(It.IsAny<GetRoomQuery>(), It.IsAny<CancellationToken>()))
//                 .ReturnsAsync(room);

//             // Act
//             var result = await _controller.GetRoom(hotelId, roomId);

//             // Assert
//             var okResult = Assert.IsType<OkObjectResult>(result);
//             Assert.Equal(room, okResult.Value);
//         }
//     }
// }