using Domain.Models;
using SendGrid.Helpers.Mail;
using SendGrid;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Application.Utilities
{
    public class SendEmail
    {
        public void Main(List<Guest> guests)
        {
            Execute(guests).Wait();
        }

        static async Task Execute(List<Guest> guests)
        {
            //var client = new SendGridClient("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"); Descomentar para enviar mensaje
            var from = new EmailAddress("quinterojorge1234@gmail.com", "Jorge Quintero");
            var subject = "Booking Confirmation";
            var plainTextContent = "My dear {0}, your reservation has been confirmed.";
            var htmlContent = "<p>My dear {0}, your reservation has been confirmed.</p>";

            foreach (var guest in guests)
            {
                var to = new EmailAddress(guest.Email, guest.FirstName);
                var personalizedPlainTextContent = string.Format(plainTextContent, guest.FirstName + " " + guest.LastName);
                var personalizedHtmlContent = string.Format(htmlContent, guest.FirstName + " " + guest.LastName);
                var msg = MailHelper.CreateSingleEmail(from, to, subject, personalizedPlainTextContent, personalizedHtmlContent);
                var response = await client.SendEmailAsync(msg);
            }
        }
    }
}
