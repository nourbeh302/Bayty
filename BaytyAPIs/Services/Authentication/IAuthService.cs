using BaytyAPIs.Dtos.AuthenticationDtos;
using Models.Entities;

namespace BaytyAPIs.Services.Authentication
{
    public interface IAuthService
    {
        Task<AuthDto> RegisterUserAsync(RegisterDto model);
        Task<AuthDto> LoginAsync(LoginDto model);
        Task<string> GetAccessTokenAsync(User user = null, string email = null);
        Task<bool> GetRefreshTokenStateForUserAsync(string email, string refreshToken);
        string GetPhoneNumberToken(string userId);
        Task<bool> VerifyPhoneNumberToken(User user, string token);
    }
}
