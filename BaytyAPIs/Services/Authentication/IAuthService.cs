using BaytyAPIs.DTOs.AuthenticationDTOs;
using Models.Entities;

namespace BaytyAPIs.Services.Authentication
{
    public interface IAuthService
    {
        Task<AuthDTO> RegisterUserAsync(RegisterDTO model);
        Task<AuthDTO> LoginAsync(LoginDTO model);
        Task<string> GetAccessTokenAsync(User user = null, string email = null);
        Task<bool> GetRefreshTokenStateForUserAsync(string email, string refreshToken);
        string GetPhoneNumberToken(string userId);
        Task<bool> VerifyPhoneNumberToken(User user, string token);
    }
}
