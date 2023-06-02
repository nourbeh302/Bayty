using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models.DataStoreContract;
using Models.Entities;

namespace Server.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class AdministrationController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IDataStore _dataStore;
        
        public AdministrationController(UserManager<User> userManager,
                                        IDataStore dataStore,
                                        RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _dataStore = dataStore;
        }

        [HttpGet]
        //[Authorize(Roles = "Admin")]
        public async Task<ActionResult<IEnumerable<User>>> UsersList() => Ok(await _userManager.Users.ToListAsync());

        [HttpPost]
        public async Task<ActionResult<string>> AddRole(string roleName)
        {
            IdentityRole identityRole = new IdentityRole { Name = roleName };

            var result = await _roleManager.CreateAsync(identityRole);

            if (!result.Succeeded)
            {
                string errors = string.Empty;

                foreach (var error in result.Errors)
                    errors += error.Description;
                
                return BadRequest(errors);
            }

            return Ok();
        }

        [HttpPost]
        public async Task<ActionResult> EditRole(string roleId, string newRoleName)
        {
            var role = await _roleManager.FindByIdAsync(roleId);

            if (role == null)
            {
                return NotFound("Role Not Found");
            }

            role.Name = newRoleName;

            await _roleManager.UpdateAsync(role);

            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult> RemoveRole(string roleId)
        {
            var role = await _roleManager.FindByIdAsync(roleId);
            
            if (role == null)
            {
                return BadRequest("Role Not Found");
            }
            
            await _roleManager.DeleteAsync(role);

            return NoContent();
        }
    }
}
