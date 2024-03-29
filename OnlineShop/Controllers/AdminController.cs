using System.Linq;
using System.Threading.Tasks;
using Core.Entities.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace OnlineShop.Controllers
{
    public class AdminController : BaseApiController
    {
        private readonly UserManager<AppUser> _userManager;
        public AdminController(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        [Authorize(Policy = "RequireAdminRole")]
        [HttpGet("users-with-roles")]
        public async Task<IActionResult> GetUsersWithRoles()
        {
            var users = await _userManager.Users
                .Include(r => r.UserRole)
                .ThenInclude(r => r.AppRole)
                .OrderBy(u => u.UserName)
                .Select( u => new 
                {
                    u.Id,
                    Usename = u.UserName,
                    Roles = u.UserRole.Select(r => r.AppRole.Name).ToList(),
                    DisplayName = u.DisplayName
                })
                .ToListAsync();

            return Ok(users);
        }

        [HttpPost("edit-roles/{email}")]
        public async Task<ActionResult> EditRoles(string email, [FromQuery] string roles)
        {
            var selectedRoles = roles.Split(",").ToArray();
            selectedRoles = selectedRoles.Select(r => r.ToLower()).ToArray();

            var user = await _userManager.FindByNameAsync(email);

            if(user == null) return NotFound("Could not find user");

            var userRoles = await _userManager.GetRolesAsync(user);
            
            var result = await _userManager.AddToRolesAsync(user,selectedRoles.Except(userRoles));

            if(!result.Succeeded) return BadRequest("Failed to added to roles");

            result = await _userManager.RemoveFromRolesAsync(user,userRoles.Except(selectedRoles));

            if(!result.Succeeded) return BadRequest("Failed to remove from roles");

            return Ok(await _userManager.GetRolesAsync(user));
        }

        [Authorize(Policy = "ModerateItems")]
        [HttpGet("items-to-moderate")]
        public IActionResult GetItemsForModeration()
        {
            return Ok("Admin and moderators can see this");
        }
    }
}