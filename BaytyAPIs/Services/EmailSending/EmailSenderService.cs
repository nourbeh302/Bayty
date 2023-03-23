using Microsoft.Extensions.Options;
using System.Net;
using System.Net.Mail;

namespace BaytyAPIs.Services.EmailSending
{
    public class EmailSenderService : IEmailSenderService
    {
        private readonly MailSettings _mailSettings;

        public EmailSenderService(IOptions<MailSettings> mailSettings)
        {
            _mailSettings = mailSettings.Value;
        }

        public async Task<bool> SendEmailAsyncWithMessage(string email, string verificationMessage, string subject, string? link = null)
        {
            try
            {
                using var smtpClient = new SmtpClient(_mailSettings.Host, _mailSettings.Port);

                smtpClient.Credentials = new NetworkCredential(_mailSettings.Mail, _mailSettings.Password);

                smtpClient.EnableSsl = true;

                smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;

                MailAddress mailAddressFrom = new MailAddress(_mailSettings.Mail, _mailSettings.DisplayName);

                MailAddress mailAddressTo = new MailAddress(email);

                MailMessage message = new MailMessage(mailAddressFrom, mailAddressTo);

                message.IsBodyHtml = true;

                message.Body = $@"<h1 style='background-color:navy; color: red;'>{verificationMessage}</h1>"
                                + (link == null ? "":$"<a href='{link}'>Click Me</a>");

                message.Subject = subject;

                await smtpClient.SendMailAsync(message);

                return true;
            }

            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
                return false;
            }
        }
    }
}
