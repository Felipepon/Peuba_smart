// src/TravelAgency.Domain/Interfaces/IEmailService.cs
using System.Threading.Tasks;
using TravelAgency.Domain.Entities;

namespace TravelAgency.Domain.Interfaces
{
    public interface IEmailService
    {
        Task SendEmailAsync(string toEmail, string subject, string message);
        Task SendBookingConfirmationAsync(string toEmail, Booking booking);
    }
}