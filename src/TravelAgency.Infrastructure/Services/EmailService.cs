
using SendGrid;
using SendGrid.Helpers.Mail;
using System.Threading.Tasks;
using TravelAgency.Domain.Interfaces;
using TravelAgency.Domain.Entities;

namespace TravelAgency.Infrastructure.Services
{
    public class EmailService : IEmailService
    {
        private readonly ISendGridClient _sendGridClient;

        public EmailService(ISendGridClient sendGridClient)
        {
            _sendGridClient = sendGridClient;
        }

        public async Task SendEmailAsync(string toEmail, string subject, string message)
        {
            var msg = new SendGridMessage
            {
                From = new EmailAddress("no-reply@travelagency.com", "Travel Agency"),
                Subject = subject,
                PlainTextContent = message,
                HtmlContent = message
            };
            msg.AddTo(new EmailAddress(toEmail));
            await _sendGridClient.SendEmailAsync(msg);
        }

        public async Task SendBookingConfirmationAsync(string toEmail, Booking booking)
        {
            var subject = "Booking Confirmation";
            var message = $"Your booking with ID {booking.Id} has been confirmed.";
            await SendEmailAsync(toEmail, subject, message);
        }
    }
}