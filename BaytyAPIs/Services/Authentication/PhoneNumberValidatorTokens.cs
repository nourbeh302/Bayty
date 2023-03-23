using System.Collections.Concurrent;

namespace BaytyAPIs.Services.Authentication
{
    public class PhoneNumberValidatorTokens
    {
        public ConcurrentDictionary<string, PhoneNumberToken> PhoneNumberTokens { get; set; } = new ConcurrentDictionary<string, PhoneNumberToken>();
    }
}
