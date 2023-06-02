namespace Server.Services.EmailSending
{
    public interface IEmailSenderService
    {
        Task<bool> SendEmailWithMessageAsync(string email, string verificationMessage, string subject, string? link = null);
    }
}
