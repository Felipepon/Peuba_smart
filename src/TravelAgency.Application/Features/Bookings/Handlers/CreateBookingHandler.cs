using MediatR;
using TravelAgency.Application.DTOs;
using TravelAgency.Application.Features.Bookings.Commands;
using TravelAgency.Domain.Entities;
using TravelAgency.Domain.Interfaces;

namespace TravelAgency.Application.Features.Bookings.Handlers
{
    public class CreateBookingHandler : IRequestHandler<CreateBookingCommand, Guid>
    {
        private readonly IBookingRepository _bookingRepository;
        private readonly IRoomRepository _roomRepository;
        private readonly IEmailService _emailService;

        public CreateBookingHandler(IBookingRepository bookingRepository,
                                    IRoomRepository roomRepository,
                                    IEmailService emailService)
        {
            _bookingRepository = bookingRepository;
            _roomRepository = roomRepository;
            _emailService = emailService;
        }

        public async Task<Guid> Handle(CreateBookingCommand request, CancellationToken cancellationToken)
        {
            // Validaciones
            if (request.Guests == null || !request.Guests.Any())
                throw new ArgumentException("Debe haber al menos un huésped.");
            if (request.CheckOutDate <= request.CheckInDate)
                throw new ArgumentException("La fecha de salida debe ser posterior a la de entrada.");

            var room = await _roomRepository.GetByIdAsync(request.RoomId);
            if (room == null)
                throw new KeyNotFoundException("La habitación no existe.");
            if (!room.IsEnabled || !await IsRoomAvailable(request.RoomId, request.CheckInDate, request.CheckOutDate))
                throw new InvalidOperationException("La habitación no está disponible");

            // Calcular costo total
            var totalDays = (request.CheckOutDate - request.CheckInDate).Days;
            var totalCost = (room.BaseCost + room.Taxes) * totalDays;

            // Crear la entidad Booking
            var booking = new Booking
            {
                CheckInDate = request.CheckInDate,
                CheckOutDate = request.CheckOutDate,
                RoomId = request.RoomId,
                TotalCost = totalCost
            };

            // Mapear cada GuestDto a Guest y asignar la relación Booking
            booking.Guests = request.Guests.Select(dto => new Guest
            {
                FullName = dto.FullName,
                BirthDate = dto.BirthDate,
                Gender = Enum.Parse<Gender>(dto.Gender), // Convertir string a Gender
                DocumentType = Enum.Parse<DocumentType>(dto.DocumentType), // Convertir string a DocumentType
                DocumentNumber = dto.DocumentNumber,
                Email = dto.Email,
                Phone = dto.Phone,
                Booking = booking
            }).ToList();

            // Mapear EmergencyContactDto a EmergencyContact y asignar la relación Booking
            booking.EmergencyContact = new EmergencyContact
            {
                FullName = request.EmergencyContact.FullName,
                Phone = request.EmergencyContact.Phone,
                Booking = booking
            };

            // Persistir la reserva (asegúrate de que el repositorio llame a SaveChangesAsync o se use un Unit of Work)
            await _bookingRepository.AddAsync(booking);

            // Enviar correo de confirmación (manejo de errores en el envío de correo)
            try
            {
                var guestEmail = booking.Guests.First().Email;
                await _emailService.SendBookingConfirmationAsync(guestEmail, booking);
            }
            catch (Exception ex)
            {
                // Registrar el error y continuar
                Console.WriteLine($"Error al enviar correo: {ex.Message}");
            }

            return booking.Id;
        }

        private async Task<bool> IsRoomAvailable(Guid roomId, DateTime checkIn, DateTime checkOut)
        {
            var bookings = await _bookingRepository.GetBookingsByRoomAsync(roomId);
            return !bookings.Any(b => checkIn < b.CheckOutDate && checkOut > b.CheckInDate);
        }
    }
}