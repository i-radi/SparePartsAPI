using System.Net;
using System.Net.Mail;

namespace SpareParts.API.Services.Email
{
    [AllowAnonymous]
    public class EmailSender : IEmailSender
    {
#pragma warning disable CS1998
        public async Task SendEmailAsync(string email, string subject, string htmlMessage)
#pragma warning restore CS1998
        {
            var fromMail = "mh600789@outlook.com";
            var fromPassword = "WorkWork44";

            var message = new MailMessage();

            message.From = new MailAddress(fromMail);
            message.To.Add(email);
            message.Body = $"<html><body> {htmlMessage}</body></html>";
            message.IsBodyHtml = true;

            var smtpClient = new SmtpClient("smtp-mail.outlook.com")
            {
                Port = 587,
                Credentials = new NetworkCredential(fromMail, fromPassword),
                EnableSsl = true,
            };
            smtpClient.Send(message);
        }
    }
}