using BLL__Buisness_Logic_Layer_.Dtos.AccountManager;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagmentSys.Controllers
{
    [Authorize(Roles = "Admin")]
    public class RolesController : Controller
    {
        private readonly RoleManager<IdentityRole<int>> _roleManager;

        public RolesController(RoleManager<IdentityRole<int>> roleManager)
        {
            _roleManager = roleManager;
        }

        //Create roles
        [HttpGet]
        public IActionResult CreateRole()
        {
            return View();
        }
        [HttpPost]
        public async Task <IActionResult> CreateRole(RoleDto NewRole)
        {
            if (ModelState.IsValid)
            {
                var role = new IdentityRole<int>();
                role.Name = NewRole.RoleName;
                var result = await _roleManager.CreateAsync(role);
                if (result.Succeeded)
                {
                    return View(new RoleDto());
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                        Console.WriteLine($"Error: {error.Code} - {error.Description}");
                    }
                }
            }
            
            return View();
        }
    }
}
