using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MVC_MiniApp.Helpers.Enums;
using MVC_MiniApp.Models;
using MVC_MiniApp.ViewModels;
using MVC_MiniApp.ViewModels.Register;

namespace MVC_MiniApp.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterVM request)
        {
            if (!ModelState.IsValid)
            {
                return View(request);
            }
            AppUser user = new()
            {
                FullName = request.FullName,
                Email = request.Email,
                UserName = request.Username,
            };
            var result = await _userManager.CreateAsync(user, request.Password);
            if (!result.Succeeded)
            {
                foreach (var item in result.Errors)
                {
                    ModelState.AddModelError("", item.Description);
                }
                return View(request);
            }

            return RedirectToAction("Index", "Home");

        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginVM request)
        {
            if (!ModelState.IsValid)
                return View(request);

            AppUser user = await _userManager.FindByEmailAsync(request.EmailOrUsername);

            if (user == null)
                user = await _userManager.FindByNameAsync(request.EmailOrUsername);

            if (user == null)
            {
                ModelState.AddModelError("", "Email or Password is incorrect");
                return View(request);
            }

            var result = await _signInManager.PasswordSignInAsync(user, request.Password, false, false);

            if (!result.Succeeded)
            {
                ModelState.AddModelError("", "Email or Password is incorrect");
                return View(request);
            }

            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }


        //[HttpGet]
        //public async Task<IActionResult> CreateRoles()
        //{
        //    foreach(var item in Enum.GetValues(typeof(Roles)))
        //}
        
    }
       

}
