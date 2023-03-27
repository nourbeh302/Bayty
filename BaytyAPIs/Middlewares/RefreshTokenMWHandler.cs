using BaytyAPIs.Security;
using BaytyAPIs.Services.Authentication;
using Microsoft.AspNetCore.DataProtection;
using System.IdentityModel.Tokens.Jwt;

namespace BaytyAPIs.Middlewares;

public class RefreshTokenMWHandler
{
    private readonly RequestDelegate _next;
    
    public RefreshTokenMWHandler(RequestDelegate next) => _next = next;
    
    public async Task InvokeAsync(HttpContext context)
    {

        Console.WriteLine($"Connected? {context.WebSockets.IsWebSocketRequest}");

        string accessToken = context.Request.Headers["Authorization"];

        string refreshToken = context.Request.Headers["refresh-token"];

        if (accessToken != null && refreshToken != null)
        {
            var _authService = context.RequestServices.GetServices(typeof(IAuthService)).FirstOrDefault() as AuthService;

            var _protector = context.RequestServices
                                    .GetDataProtectionProvider()
                                    .CreateProtector(AccountProtectorPurpose.HashingMail);

            var token = accessToken.Split(" ").LastOrDefault();

            var dataInToken = new JwtSecurityToken(token);

            if (dataInToken.ValidTo.ToLocalTime() < DateTime.Now.ToLocalTime())
            {
                var encodedEmail = dataInToken.Claims.FirstOrDefault(c => c.Type == "Email")?.Value;

                var email = _protector.Unprotect(encodedEmail);

                if (await _authService.GetRefreshTokenStateForUserAsync(email, refreshToken))
                {
                    accessToken = await _authService.GetAccessTokenAsync(email: email);

                    context.Request.Headers["Authorization"] = $"Bearer {accessToken}";

                    context.Response.Headers["Authorization"] = $"Bearer {accessToken}";
                }
            }
        }

        await _next(context);

    }
}