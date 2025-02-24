
// using FluentAssertions;
// using Moq;
// using System;
// using System.Threading;
// using System.Threading.Tasks;
// using TravelAgency.Application.Features.Rooms.Commands;
// using TravelAgency.Application.Features.Rooms.Handlers;
// using TravelAgency.Domain.Entities;
// using TravelAgency.Domain.Enums;
// using TravelAgency.Domain.Interfaces;
// using Xunit;

// namespace TravelAgency.Tests.Features.Rooms.Handlers
// {
//     public class CreateRoomCommandHandlerTests
//     {
//         private readonly Mock<IRoomRepository> _mockRoomRepository;
//         private readonly Mock<IHotelRepository> _mockHotelRepository;
//         private readonly CreateRoomHandler _handler;

//         public CreateRoomCommandHandlerTests()
//         {
//             _mockRoomRepository = new Mock<IRoomRepository>();
//             _mockHotelRepository = new Mock<IHotelRepository>();
//             _handler = new CreateRoomHandler(_mockRoomRepository.Object, _mockHotelRepository.Object);
//         }

//         [Fact]
//         public async Task Handle_ValidCommand_ReturnsRoomId()
//         {
//             // Arrange
//             var hotelId = Guid.NewGuid();
//             var command = new CreateRoomCommand(
//                 hotelId,
//                 RoomType.Standard,
//                 10,
//                 "Piso 3, Habitación 301"
//             );

//             _mockHotelRepository
//                 .Setup(r => r.GetByIdAsync(hotelId))
//                 .ReturnsAsync(new Hotel("Test Hotel", "Test City"));

//             _mockRoomRepository
//                 .Setup(r => r.AddAsync(It.IsAny<Room>()))
//                 .Returns(Task.CompletedTask);

//             // Act
//             var result = await _handler.Handle(command, CancellationToken.None);

//             // Assert
//             result.Should().NotBeEmpty();
//             _mockRoomRepository.Verify(r => r.AddAsync(It.IsAny<Room>()), Times.Once);
//         }

//         [Fact]
//         public async Task Handle_InvalidHotelId_ThrowsException()
//         {
//             // Arrange
//             var hotelId = Guid.NewGuid();
//             var command = new CreateRoomCommand(
//                 hotelId,
//                 RoomType.Standard,
//                 10,
//                 "Piso 3, Habitación 301"
//             );

//             _mockHotelRepository
//                 .Setup(r => r.GetByIdAsync(hotelId))
//                 .ReturnsAsync((Hotel)null);

//             // Act & Assert
//             await Assert.ThrowsAsync<KeyNotFoundException>(() => _handler.Handle(command, CancellationToken.None));
//         }

//         [Theory]
//         [InlineData(0)]
//         [InlineData(-100)]
//         public async Task Handle_InvalidTaxes_ThrowsValidationException(decimal invalidTaxes)
//         {
//             // Arrange
//             var hotelId = Guid.NewGuid();
//             var command = new CreateRoomCommand(
//                 hotelId,
//                 RoomType.Standard,
//                 invalidTaxes,
//                 "Piso 3, Habitación 301"
//             );

//             // Act & Assert
//             await Assert.ThrowsAsync<ArgumentException>(() => _handler.Handle(command, CancellationToken.None));
//         }
//     }
// }