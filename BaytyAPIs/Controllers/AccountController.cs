using BaytyAPIs.Constants;
using BaytyAPIs.Dtos.AuthenticationDtos;
using BaytyAPIs.Security;
using BaytyAPIs.Services.Authentication;
using BaytyAPIs.Services.EmailSending;
using BaytyAPIs.Services.SMS;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models.Entities;
using System.Security.Claims;

namespace BaytyAPIs.Controllers
{
    [ApiController]
    [Route("/[controller]/[action]")]
    public class AccountController : ControllerBase
    {
        private readonly IAuthService _authService;         //  Implements The JWT Logic plus authentication
        private readonly ILogger<AccountController> _logger;
        private readonly IEmailSenderService _emailService;
        private readonly ISMSService _smsService;
        private readonly UserManager<User> _userManager;
        private readonly Dictionary<string, IDataProtector> _protectors = new Dictionary<string, IDataProtector>();

        public AccountController(
            ILogger<AccountController> logger,
            IAuthService authService,
            IEmailSenderService emailService,
            ISMSService smsService,
            IDataProtectionProvider provider,
            UserManager<User> userManager)
        {
            _protectors["Email"] = provider.CreateProtector(AccountProtectorPurpose.HashingMail);
            _protectors["Id"] = provider.CreateProtector(AccountProtectorPurpose.HashingId);
            _protectors["Token"] = provider.CreateProtector(AccountProtectorPurpose.HashingTokens);
            _authService = authService;
            _userManager  = userManager;
            _emailService = emailService;
            _logger       = logger;
            _smsService = smsService;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(AuthDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(AuthDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(LoginDto))]
        public async Task<ActionResult<AuthDto>> Login([FromBody] LoginDto model)
        {
            if (ModelState.IsValid)
            {
                var authDto = await _authService.LoginAsync(model);

                if (authDto.IsAuthenticated)
                    return Ok(authDto);

                return BadRequest(authDto);
            }
            return BadRequest(model);
        }


        [Authorize]
        [HttpPost]
        public async Task<ActionResult> Logout()
        {
            var user = _userManager.Users.Include("RefreshTokens")
                .FirstOrDefault(u => u.RefreshTokens.SingleOrDefault(rt => rt.UserId == u.Id) ==
                Request.Headers["refresh-token"]);

            var refreshToken = user.RefreshTokens.SingleOrDefault(rt => rt.Token == Request.Headers["refresh-token"]);

            refreshToken.RevokedOn = DateTime.Now.ToLocalTime();

            await _userManager.UpdateAsync(user);

            return Ok();
        }


        /// <summary>
        /// This method stores the data of user for the first time and tell the user to verify his email
        /// </summary>
        /// <param name="model">RegisterDto type</param>
        /// <returns>AuthDto</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(RegisterDto))]
        public async Task<ActionResult> Register([FromBody] RegisterDto model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var authModel = await _authService.RegisterUserAsync(model);

                    if (!string.IsNullOrEmpty(authModel.Message))
                    {
                        var user = await _userManager.FindByEmailAsync(model.Email);

                        if (user == null)
                            return NotFound("Error Happened");

                        var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);

                        var link = Url.Action("VerifyEmail", "Account", new { userId = _protectors["Id"].Protect(user.Id), token = _protectors["Token"].Protect(token) }, Request.Scheme);

                        //Verify Email First
                        if (await _emailService.SendEmailAsyncWithMessage(model.Email, "Please Confirm Your Email To Use Our Application", "Email Confirmation", link))
                            return Ok("Please check you email to verify your email address");
                    }

                } catch (Exception ex)
                {
                    Console.WriteLine($"{ex.Message}");
                    return StatusCode(StatusCodes.Status500InternalServerError, "Server has internal errors!");
                }
            }
            return BadRequest(model);
        }

        /// <summary>
        /// Verify email with user id
        /// </summary>
        /// <param name="userId">User Id</param>
        /// <param name="token">Token which verifies the </param>
        /// <returns>Object</returns>
        [HttpGet]
        public async Task<ActionResult> VerifyEmail(string userId, string token)
        {
            userId = _protectors["Id"].Unprotect(userId);
            token = _protectors["Token"].Unprotect(token);

            var user = await _userManager.FindByIdAsync(userId);

            if (user == null)
                return NotFound();

            var result = await _userManager.ConfirmEmailAsync(user, token);

            if (!result.Succeeded)
            {
                string errors = string.Empty;

                foreach (var error in result.Errors)
                    errors += error.Description;

                return BadRequest(errors);
            }

            Claim claim = new Claim("EmailVerified", "True");

            user.EmailConfirmed = true;

            await _userManager.AddClaimAsync(user, claim);

            await _userManager.UpdateAsync(user);

            return Ok("Email Verified Successfully");
        }

        [HttpGet]
        [Authorize]
        public async Task<ActionResult> AskForPhoneNumberVerification(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            
            if (user is null)
                return Ok(AuthMessages.NotFoundUser);

            if (user.IsPhoneNumberVerified)
                return Ok("Phone Number Is Already Verified");

            var token = _authService.GetPhoneNumberToken(userId);

            var result = await _smsService.SendSMS(user.PhoneNumber, token);

            if (result.Status == 0)
                return Ok("We have just sent a token to your phone number, please verify it.");
            else
                return Conflict("Sorry!!!!");
        }

        [HttpGet]
        [Authorize]
        public async Task<ActionResult> VerifyPhoneNumber(string userId, string token)
        {
            var user = await _userManager.FindByIdAsync(userId);

            if (user == null)
                return Ok(AuthMessages.NotFoundUser);

            if (user.IsPhoneNumberVerified)
                return Ok("Phone Number Is Already Verified");

            if (await _authService.VerifyPhoneNumberToken(user, token))
                return Ok("Phone Number Verified Successfully");

            return Conflict("We Are Sorry");
        }

        [HttpGet]
        //[Authorize]
        public async Task<ActionResult> ChangePhoneNumberValidateWithEmail(string userId, string phoneNumber)
        {
            try
            {
                
                var user = await _userManager.FindByIdAsync(userId);

                var token = await _userManager.GenerateChangePhoneNumberTokenAsync(user, phoneNumber);

                if (await _emailService.SendEmailAsyncWithMessage(user.Email, $"Please provide this code to us to enable you to change your phone number. <br/>{token}", "Verify Changed Phone Number"))
                    return Ok("Go check your email for the code");
                else
                    return StatusCode(StatusCodes.Status501NotImplemented, "Email service doesn't work now for some reason.");
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error Happend At Server");
            }
        }

        [HttpPost]
        //[Authorize]
        public async Task<ActionResult> ChangePhoneNumber(string userId, string code, string phoneNumber)
        {
            try {

                var user = await _userManager.FindByIdAsync(userId);
                
                if (user == null)
                    return BadRequest(AuthMessages.NotFoundUser);

                var result = await _userManager.ChangePhoneNumberAsync(user, phoneNumber, code);
                

                if (!result.Succeeded)
                {

                    user.IsPhoneNumberVerified = false;

                    await _userManager.UpdateAsync(user);

                    string errors = string.Empty;

                    foreach (var error in result.Errors)
                        errors += error.Description + "\n";

                    return BadRequest(errors);
                }

                return Ok("Phone number changed successfully");

            } catch {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error Happend At Server");
            }
        }

        [HttpPost]
        //[Authorize(Policy = "Email Verified")]
        public async Task<ActionResult> ChangeEmail() => throw new NotImplementedException();

        [HttpPost]
        //[Authorize]
        public async Task<ActionResult> VerifyChangedEmail() => throw new NotImplementedException();

        //[Authorize]
        [HttpPut]
        public async Task<ActionResult> EditAccount() => throw new NotImplementedException();

    }
}