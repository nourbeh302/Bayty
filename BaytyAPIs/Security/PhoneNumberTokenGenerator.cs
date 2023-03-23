using Microsoft.AspNetCore.Identity;
using Models.Entities;

namespace BaytyAPIs.Security
{
    public static class PhoneNumberTokenGenerator
    {
        public static string GeneratePhoneNumberToken(this UserManager<User> userManager)
        => Convert.ToString(new Random().NextDouble()).Split(".")[1].Substring(0, 7);
    }
}
