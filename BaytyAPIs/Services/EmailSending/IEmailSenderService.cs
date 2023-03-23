namespace BaytyAPIs.Services.EmailSending
{
    public interface IEmailSenderService
    {
        Task<bool> SendEmailAsyncWithMessage(string email, string verificationMessage, string subject, string? link = null);
    }
}
