namespace BaytyAPIs.Services.SMS
{
    public interface ISMSService
    {
        Task<SMSResult> SendSMS(string to, string token);
    }
}
