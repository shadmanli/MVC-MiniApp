using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MVC_MiniApp.Helpers.Enums;
using MVC_MiniApp.Models;
using MVC_MiniApp.ViewModels;
using MVC_MiniApp.ViewModels.Register;

public class AccountController : Controller
{
    private readonly UserManager<AppUser> _userManager;
    private readonly SignInManager<AppUser> _signInManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    public AccountController(UserManager<AppUser> userManager,
                            SignInManager<AppUser> signInManager,
                            RoleManager<IdentityRole> roleManager)
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
        await _userManager.AddToRoleAsync(user, Roles.SuperAdmin.ToString());
        await _signInManager.SignInAsync(user, false);

        return View(nameof(Login));
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
        {
            return View(request);
        }

        AppUser user = await _userManager.FindByEmailAsync(request.EmailOrUsername);

        if (user is null)
        {
            user = await _userManager.FindByNameAsync(request.EmailOrUsername);
        }

        if (user is null)
        {
            ModelState.AddModelError("", "Email ,Username or password is wrong");
            return View(request);
        }

        var result = await _userManager.CheckPasswordAsync(user, request.Password);
        if (!result)
        {
            ModelState.AddModelError("", "Email ,Username or password is wrong");
            return View(request);
        }
        var response = await _signInManager.PasswordSignInAsync(user, request.Password, false, false);



        return RedirectToAction("Index", "Home");
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Logout()
    {
        await _signInManager.SignOutAsync();
        return RedirectToAction("Index", "Home");
    }


    [HttpGet]
    public async Task<ActionResult> CreateRoles()
    {
        foreach (var role in Enum.GetValues(typeof(Roles)))
        {
            if (!await _roleManager.RoleExistsAsync(role.ToString()))
            {
                await _roleManager.CreateAsync(new IdentityRole { Name = role.ToString() });
            }
        }
        return Ok();
    }
}