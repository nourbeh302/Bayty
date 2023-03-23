namespace BaytyAPIs.Services.Authentication
{
    public class PhoneNumberToken
    {
        public string UserId { get; set; }
        public string Token { get; set; }
        public DateTime Expires => DateTime.Now.AddMinutes(2);
    }
}
