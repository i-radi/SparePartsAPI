using System.Net.Mail;
using System.Net;

namespace SpareParts.API;

    public static class EmailSender
    {
#pragma warning disable CS1998
        public static async Task SendEmailAsync(string email, string subject, string htmlMessage)
#pragma warning restore CS1998
        {
            string fromMail = "radi.allowforall@gmail.com";
            var fromMailAddress = new MailAddress(fromMail,"Spart Parts Application");
            string fromPassword = "bdxlqeqidjgofwaq";

            MailMessage message = new MailMessage();
            message.From = fromMailAddress;
            message.Subject = subject;
            message.To.Add(new MailAddress(email, "Name"));
            message.Body = "<html><body> " + htmlMessage + " </body></html>";
            message.IsBodyHtml = true;

            var smtpClient = new SmtpClient("smtp.gmail.com")
            {
                Port = 587,
                Credentials = new NetworkCredential(fromMailAddress.Address, fromPassword),
                EnableSsl = true,
                UseDefaultCredentials = false,
            };

            smtpClient.Send(message);
        }
    }

