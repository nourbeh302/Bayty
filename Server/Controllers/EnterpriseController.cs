using Server.Services.EmailSending;
using Servers.DTOs.EnterpriseDTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Models.DataStoreContract;
using Models.Entities;

namespace Servers.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class EnterpriseController : ControllerBase
    {
        private readonly IDataStore _dataStore;
        private readonly IEmailSenderService _emailService;
        private readonly UserManager<User> _userManager;
        public EnterpriseController(IDataStore dataStore, IEmailSenderService emailService, UserManager<User> userManager)
        {
            _dataStore = dataStore;
            _emailService = emailService;
            _userManager = userManager;
        }

        [HttpPost]
        public async Task<ActionResult> AddEnterprise(EnterpriseDTO dto)
        {
            if (ModelState.IsValid)
            {
                if (Request.Form.Files.Count == 1)
                {

                }
                else
                {
                    BadRequest("Must be one image which is logo");
                }
            }
            return BadRequest(ModelState);
        }


        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> VerifyEnterprise(int enterpriseId)
        {

            try
            {
                var enterprise = await _dataStore.Enterprises.FindByIdAsnyc(enterpriseId);
            
                if (enterprise == null)
                    return NotFound();

                await _emailService.SendEmailWithMessageAsync(enterprise.Email, "Your account created successfully now you can login", "Enterprise Account Verification");
            }
            catch (Exception ex)
            {

            }
            throw new NotImplementedException();
        }
    }
}
