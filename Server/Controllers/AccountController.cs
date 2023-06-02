using Server.Constants;
using Server.DTOs.AuthenticationDTOs;
using Server.Security;
using Server.Constants;
using Server.DTOs.AuthenticationDTOs;
using Server.DTOs.EntitiesDTOs;
using Server.Services.Authentication;
using Server.Services.EmailSending;
using Server.Services.SMS;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models.Entities;
using System.Security.Claims;

namespace Server.Controllers
{
    [ApiController]
    [Route("/[controller]/[action]")]
    public class AccountController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly ILogger<AccountController> _logger;
        private readonly IEmailSenderService _emailService;
        private readonly ISMSService _smsService;
        private readonly UserManager<User> _userManager;
        private readonly Dictionary<string, IDataProtector> _protectors;

        public AccountController(
            ILogger<AccountController> logger,
            IAuthService authService,
            IEmailSenderService emailService,
            ISMSService smsService,
            IDataProtectionProvider provider,
            UserManager<User> userManager)
        {
            _protectors = new Dictionary<string, IDataProtector>
            {
                ["Email"] = provider.CreateProtector(AccountProtectorPurpose.HashingMail),
                ["Id"] = provider.CreateProtector(AccountProtectorPurpose.HashingId),
                ["Token"] = provider.CreateProtector(AccountProtectorPurpose.HashingTokens)
            };
            _authService = authService;
            _userManager = userManager;
            _emailService = emailService;
            _logger = logger;
            _smsService = smsService;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(AuthDTO))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(AuthDTO))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(LoginDTO))]
        public async Task<ActionResult<AuthDTO>> Login([FromBody]LoginDTO model)
        {
            if (ModelState.IsValid)
            {
                var authDto = await _authService.GetUserCredentialsAsync(model);

                if (authDto.IsAuthenticated)
                    return Ok(authDto);

                return BadRequest(authDto);
            }
            return BadRequest(ModelState);
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
        /// <param name="model">RegisterDTO type</param>
        /// <returns>AuthDTO</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(RegisterDTO))]
        public async Task<ActionResult> Register([FromBody]RegisterDTO model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var authModel = await _authService.AddUserAsync(model);

                    if (!string.IsNullOrEmpty(authModel.Message) && authModel.Message == AuthMessages.CreatedSuccessfully)
                    {
                        var user = await _userManager.FindByEmailAsync(model.Email);

                        if (user == null)
                            return NotFound("Error Happened");

                        var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);

                        var link = Url.Action("VerifyEmail", "Account",
                            new
                            {
                                userId = _protectors["Id"].Protect(user.Id),
                                token = _protectors["Token"].Protect(token)
                            }, Request.Scheme);

                        if (await _emailService.SendEmailWithMessageAsync(model.Email, "Please Confirm Your Email To Use Our Application", "Email Confirmation", link))
                            return Ok("Please check you email to verify your email address");
                        else
                        {
                            return StatusCode(StatusCodes.Status503ServiceUnavailable, "Your account created successfully but email service isn't available now try to login then ask for email validation after short time.");
                        }
                    }
                    else
                    {
                        return BadRequest(authModel);
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

            return new RedirectResult("http://localhost:4200/email-valid");
        }

        [HttpGet]
        [Authorize]
        public async Task<ActionResult> AskForPhoneNumberVerification(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);

            if (user is null)
                return Ok(AuthMessages.NotFoundUser);

            if (user.isPhoneNumberVerified)
                return Conflict("Phone Number Is Already Verified");

            var token = _authService.GetPhoneNumberToken(userId);

            var result = await _smsService.SendSMSAsync(user.PhoneNumber, token);

            if (result.Status == 0)
                return Ok("We have just sent a token to your phone number, please verify it within 2 minutes.");
            else
                return Conflict("Sorry!!!!");
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult> VerifyPhoneNumber(VerificationDTO verificationDTO)
        {
            var user = await _userManager.FindByIdAsync(verificationDTO.userId);

            if (user == null)
                return Ok(AuthMessages.NotFoundUser);

            if (user.isPhoneNumberVerified)
                return Ok("Phone Number Is Already Verified");

            if (await _authService.VerifyPhoneNumberToken(user, verificationDTO.token))
                return Ok("Phone Number Verified Successfully");

            return Conflict("We Are Sorry");
        }

        [HttpGet]
        [Authorize]
        public async Task<ActionResult> ChangePhoneNumberValidateWithEmail(string userId, string phoneNumber)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(userId);

                var token = await _userManager.GenerateChangePhoneNumberTokenAsync(user, phoneNumber);

                if (await _emailService.SendEmailWithMessageAsync(user.Email, $"Please provide this code to us to enable you to change your phone number. <br/>{token}", "Verify Changed Phone Number"))
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
        [Authorize]
        public async Task<ActionResult> ChangePhoneNumber(string userId, string code, string phoneNumber)
        {
            try {
                var user = await _userManager.FindByIdAsync(userId);

                if (user == null)
                    return BadRequest(AuthMessages.NotFoundUser);

                var result = await _userManager.ChangePhoneNumberAsync(user, phoneNumber, code);

                if (!result.Succeeded)
                {
                    user.isPhoneNumberVerified = false;

                    await _userManager.UpdateAsync(user);

                    string errors = string.Empty;

                    foreach (var error in result.Errors)
                        errors += error.Description;

                    return BadRequest(errors);
                }

                return Ok("Phone number changed successfully");

            } catch {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error Happend At Server");
            }
        }

        [HttpPost]
        [Authorize(Policy = "Email Verified")]
        public async Task<ActionResult> ChangeEmail(string userId, string email)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(userId);

                if (user == null)
                {
                    return NotFound(AuthMessages.NotFoundUser);
                }

                var token = await _userManager.GenerateChangeEmailTokenAsync(user, email);

                var result = await _smsService.SendSMSAsync(user.PhoneNumber, token);

                return Ok("Use the generated token in ");

            } catch
            {
                return StatusCode(StatusCodes.Status503ServiceUnavailable);
            }
        }

        //[HttpPost]
        ////[Authorize]
        //public async Task<ActionResult> VerifyChangedEmail(string token, string userId)
        //{
        //    try
        //    {
        //        var user = await _userManager.FindByIdAsync(userId);

        //        if (user == null)
        //        {
        //            return NotFound();
        //        }

        //        IdentityResult result = await _userManager.ConfirmEmailAsync(user, token);

        //        if (result.Succeeded)
        //        {
        //            if (user.)
        //        }


        //    }
        //    catch (Exception e)
        //    {

        //    }
        //}


        [HttpPost]
        public async Task<ActionResult> ResetPasswordRequest(string userId, string method)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(userId);

                if (user == null)
                    return NotFound(AuthMessages.NotFoundUser);

                var token = await _userManager.GeneratePasswordResetTokenAsync(user);

                if (AuthMethods.Email.ToLower() == method.ToLower())
                {
                    var link = Url.Action("VerifyResetPasswordRequest", "Account");

                    await _emailService.SendEmailWithMessageAsync(user.Email, "Please verify your......", "Reset Password Code", link);
                }

                else if (AuthMethods.PhoneNumber.ToLower() == method.ToLower())
                {
                    var result = await _smsService.SendSMSAsync(user.PhoneNumber, token);

                    if (string.IsNullOrEmpty(result.ErrorMessage))
                    {

                    }
                }
                else
                    return BadRequest();
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }

            throw new NotImplementedException();
        }

        //[HttpGet]
        //public async Task<ActionResult> ResetPassword(string token, string userId, string oldPassword, string newPassword)
        //{
        //    var user = await _userManager.FindByIdAsync(userId);
            
        //    if (user == null)
        //        return NotFound();
        //}


        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> DeleteAccount(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);

            if (user == null)
            {
                return BadRequest();
            }

            var result = await _userManager.DeleteAsync(user);

            if (result.Errors.Any())
            {
                string errors = string.Empty;
                
                foreach (var error in result.Errors)
                    errors += error.Description;

                return BadRequest(errors);
            }

            return NoContent();
        }

        [Authorize]
        [HttpPatch]
        public async Task<ActionResult> EditAccount(string userId, UserDTO userDto)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(userId);
                
                if (user == null)
                    return NotFound();
                
                user.FirstName = userDto.FirstName;
                user.LastName  = userDto.LastName;

                var result = await _userManager.UpdateAsync(user);

                if (result.Succeeded)
                    return NoContent();

                else
                    return BadRequest(result.Errors.Select(e => e.Description));
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }


        [Authorize]
        [HttpGet]
        public async Task<ActionResult> GetProfile(string userId)
        {
            try
            {
                var user = _userManager.FindByIdAsync(userId);

                if (user == null)
                {
                    return NotFound();
                }

                //Must be userDto

                return Ok(user);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        private void DeleteImages(IEnumerable<HouseBaseImagePath> hbIps)
        {
            foreach (var hbIp in hbIps)
            {
                try
                {
                    if (System.IO.File.Exists(_web.WebRootPath + hbIp.ImagePath))
                        System.IO.File.Delete(_web.WebRootPath + hbIp.ImagePath);
                }
                catch { Console.WriteLine("Images not loaded successfully."); }
            }
        }
        private string GenerateImagePath(IFormFile file)
        {
            var imagePath = @"\Images\Properties\" + Guid.NewGuid().ToString() + file.FileName;
            var imageFolder = _web.WebRootPath + imagePath;
            using (var fs = new FileStream(imageFolder, FileMode.Create))
                file.CopyTo(fs);
            return imagePath;
        }



    }
}