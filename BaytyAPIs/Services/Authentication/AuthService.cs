using BaytyAPIs.Constants;
using BaytyAPIs.Dtos.AuthenticationDtos;
using BaytyAPIs.Security;
using BaytyAPIs.Security;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Models.DataStoreContract;
using Models.Entities;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace BaytyAPIs.Services.Authentication;
public class AuthService : IAuthService
{
    private readonly UserManager<User> _userManger;
    private readonly SignInManager<User> _signInManager;
    private readonly JWT _jwt;
    private readonly IDataProtector _protector;
    private readonly IDataStore _dataStore;
    private readonly PhoneNumberValidatorTokens _phoneNumsWithTokens;

    public AuthService(IDataProtectionProvider provider,
                       IOptions<JWT> jwt,
                       IDataStore dataStore,
                       UserManager<User> userManger,
                       SignInManager<User> signInManager,
                       PhoneNumberValidatorTokens phoneNumWithTokens)
    {
        _dataStore = dataStore;
        _userManger = userManger;
        _signInManager = signInManager;
        _jwt = jwt.Value;
        _phoneNumsWithTokens = phoneNumWithTokens;
        _protector = provider.CreateProtector(AccountProtectorPurpose.HashingMail);
    }

    public async Task<AuthDto> RegisterUserAsync(RegisterDto model)
    {

        if (await _userManger.FindByEmailAsync(model.Email) != null)
            return new AuthDto { Message = "Email is already exists." };

        //_userManger.GeneratePhoneNumberToken();

        User user = new User
        {
            FirstName = model.FirstName,
            LastName = model.LastName,
            Email = model.Email,
            UserName = model.Email,
            Address = model.Address,
            Type = model.AccountType,
            PhoneNumber = model.PhoneNumber,
            ImagePath = model.ImagePath
        };

        var result = await _userManger.CreateAsync(user, model.Password);

        AuthDto authDto = new AuthDto();

        if (!result.Succeeded)
        {
            foreach (var error in result.Errors)
                authDto.Message += error.Description;
            return authDto;
        }

        authDto.Message = "Your account created successfully! please verify your email!";

        return authDto;
    }

    public async Task<AuthDto> LoginAsync(LoginDto model)
    {
        var user = await _userManger.Users.FirstOrDefaultAsync(u => u.Email == model.Email || u.PhoneNumber == model.Email);

        if (user == null || !(await _signInManager.CheckPasswordSignInAsync(user, model.Password, false)).Succeeded)
            return new AuthDto { Message = AuthMessages.NotFoundUser };

        var token = await GetAccessTokenAsync(user);

        var refreshToken = await _dataStore.RefreshTokens.FindOneAsync(rt => rt.UserId == user.Id && rt.ExpiresOn > DateTime.Now);

        if (refreshToken == null)
        {
            refreshToken = new RefreshToken
            {
                Token = GetRefreshToken(),
                ExpiresOn = DateTime.Now.AddMonths(4),
                CreatedOn = DateTime.Now,
            };

            user.RefreshTokens.Add(refreshToken);

            await _userManger.UpdateAsync(user);
        }

        return new AuthDto
        {
            RefreshToken = refreshToken.Token,
            AccessToken = token,
            AccessTokenLifeTime = DateTime.Now.AddHours(_jwt.LifeTimeInMinutes).ToLocalTime(),
            IsAuthenticated = true,
            UserId = user.Id,
            Message = "User is authenticated now",
        };

    }

    // Reuse this function in multiple scopes
    public async Task<string> GetAccessTokenAsync(User user = null, string email = null)
    {
        if (email != null && user == null)
        {
            user = await _userManger.FindByEmailAsync(email);
        }

        if (user == null)
        {
            return string.Empty;
        }

        var userClaims = await _userManger.GetClaimsAsync(user);
        var userRoles = await _userManger.GetRolesAsync(user);

        foreach (var role in userRoles)
            userClaims.Add(new Claim("role", role));


        var encodedEmailAsString = _protector.Protect(user.Email);

        userClaims.Add(new Claim("Email", encodedEmailAsString));

        var symmetricKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwt.Key));

        var signingCredentials = new SigningCredentials(symmetricKey, SecurityAlgorithms.HmacSha256);

        JwtSecurityToken jwtSecurityToken = new JwtSecurityToken(
            audience: _jwt.Audience,
            issuer: _jwt.Issuer,
            claims: userClaims,
            signingCredentials: signingCredentials,
            notBefore: DateTime.Now,
            expires: DateTime.Now.AddSeconds(_jwt.LifeTimeInMinutes).ToLocalTime());


        return new JwtSecurityTokenHandler()
                        .WriteToken(jwtSecurityToken);
    }

    public async Task<bool> GetRefreshTokenStateForUserAsync(string email, string refreshToken)
    {
        var user = await _userManger.FindByEmailAsync(email);

        var rt = await _dataStore.RefreshTokens.FindOneAsync(rt => rt.ExpiresOn > DateTime.Now && rt.UserId == user.Id);

        return rt.ExpiresOn > DateTime.Now;
    }

    public async Task<bool> VerifyPhoneNumberToken(User user, string token)
    {
        if (_phoneNumsWithTokens.PhoneNumberTokens.TryGetValue(token, out var phoneWithToken))
        {

            if (phoneWithToken.Expires < DateTime.Now)
            {
                if (_phoneNumsWithTokens.PhoneNumberTokens.TryRemove(token, out phoneWithToken))
                    return false;
            }
            else
            {
                if (_phoneNumsWithTokens.PhoneNumberTokens.TryRemove(token, out phoneWithToken))
                    user.IsPhoneNumberVerified = true;
                    await _userManger.UpdateAsync(user);
                return true;
            }
        }
        return false;
    }

    public string GetPhoneNumberToken(string userId)
    {
        string phoneNumberToken = GeneratePhoneNumberToken();

        var phoneAnDtoken = new PhoneNumberToken
        {
            Token = phoneNumberToken,
            UserId = userId
        };

        _phoneNumsWithTokens.PhoneNumberTokens.TryAdd(phoneNumberToken, phoneAnDtoken);

        return phoneNumberToken;
    }

    private string GeneratePhoneNumberToken() => Convert.ToString(new Random().NextDouble()).Split(".")[1].Substring(0, 6);

    private string GetRefreshToken()
    {
        var randomNumbers = new byte[32];
        var rsgCrypto = new RNGCryptoServiceProvider();
        rsgCrypto.GetBytes(randomNumbers);
        return Convert.ToBase64String(randomNumbers);
    }
}
