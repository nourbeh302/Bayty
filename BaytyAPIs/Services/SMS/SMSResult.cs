namespace BaytyAPIs.Services.SMS
{
    public class SMSResult
    {
        public short Status { get; set; }
        public SMSResponse Response { get; set; }
        public string ErrorMessage { get; set; }

    }
}
