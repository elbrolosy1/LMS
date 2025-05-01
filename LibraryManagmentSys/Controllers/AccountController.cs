using BLL__Buisness_Logic_Layer_.Dtos.AccountManager;
using DAL__Data_Access_Layer_.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace LibraryManagmentSys.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;

        public AccountController(
            UserManager<AppUser> userManager,
            SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        //Admin Add New Admin
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult AddAdmin()
        {
            return View();
        }
        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddAdmin(Register newUser) 
        {
            if (ModelState.IsValid)
            {
                var user = new AppUser
                {
                    //FullNameUser = newUser.FullNameUser,
                    UserName = newUser.FirstName,
                    Email = newUser.Email,
                    FirstName = newUser.FirstName,
                    LastName = newUser.LastName,
                    DateOfBirth = newUser.DateOfBirth,
                    PhoneNumber = newUser.PhoneNumber,
                    Address = newUser.Address,
                    gender = newUser.Gender,

                };

                var result = await _userManager.CreateAsync(user, newUser.Password);
                if (result.Succeeded)
                {
                    //Assign Role to User
                    await _userManager.AddToRoleAsync(user, "Admin");

                    //Create Cookie
                    await _signInManager.SignInAsync(user, isPersistent: false);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                        // Log the error for debugging
                        Console.WriteLine($"Error: {error.Code} - {error.Description}");
                    }
                }
            }
            return View(newUser);
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(Register newUser)
        {
            if (ModelState.IsValid)
            {
                var user = new AppUser
                {
                    //FullNameUser = newUser.FullNameUser,
                    UserName = newUser.FirstName,
                    Email = newUser.Email,
                    FirstName = newUser.FirstName,
                    LastName = newUser.LastName,
                    DateOfBirth = newUser.DateOfBirth,
                    PhoneNumber = newUser.PhoneNumber,
                    Address=newUser.Address,
                    gender = newUser.Gender,
                    
                };

                var result = await _userManager.CreateAsync(user, newUser.Password);
                if (result.Succeeded)
                {
                    await _signInManager.SignInAsync(user, isPersistent: false);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                        // Log the error for debugging
                        Console.WriteLine($"Error: {error.Code} - {error.Description}");
                    }
                }
            }
            return View(newUser);
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(Login userLogin)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(userLogin.Email);
                if (user != null)
                {
                    var result = await _userManager.CheckPasswordAsync(user, userLogin.Password);
                    if (result==true)
                    {
                        //await _signInManager.SignInAsync(user, userLogin.RememberMe );
                        var Claims = new List<Claim>
                        {
                            new Claim(ClaimTypes.Name, user.UserName),
                            new Claim(ClaimTypes.Email, user.Email),
                            new Claim("FullName", user.FirstName + " " + user.LastName)
                        };
                        await _signInManager.SignInWithClaimsAsync(user, userLogin.RememberMe,Claims);
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Invalid login attempt.");
                    }
                }
            }
            return View();
        }
    }
}
