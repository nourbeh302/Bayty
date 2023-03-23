namespace BaytyAPIs.DTOs.AuthenticationDTOs
{
    public class AuthDTO
    {
        public bool IsAuthenticated { get; set; }
        public string Message { get; set; }
        public string AccessToken { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string UserId { get; set; }
        public DateTime AccessTokenLifeTime { get; set; }
        public DateTime RefreshTokenLifeTime { get; set; }
        public string RefreshToken { get; set; }
    }
}
