using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages;
using MimeKit;
using MimeKit.Text;
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
        await _userManager.AddToRoleAsync(user, Roles.Member.ToString());

        var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
        var link = Url.Action(nameof(ConfirmEmail), "Account", new { userId = user.Id, token }, Request.Scheme, Request.Host.ToString());

        var email = new MimeMessage();
        email.From.Add(MailboxAddress.Parse("intizarshadmanli55@gmail.com"));
        email.To.Add(MailboxAddress.Parse(user.Email));
        email.Subject = "Email Confirmation";
        email.Body = new TextPart(TextFormat.Html) { Text =  $"<a href='{link}'>Click here</a>" };

        // send email
        using var smtp = new SmtpClient();
        smtp.Connect("smtp.gmail.com", 587, SecureSocketOptions.StartTls);
        smtp.Authenticate("intizarshadmanli55@gmail.com", "ehqjghadwenvpexn");
        smtp.Send(email);
        smtp.Disconnect(true);

        await _signInManager.SignInAsync(user, false);

        return View(nameof(VerifyEmail));
    }
    //ehqj ghad wenv pexn
    
    [HttpGet]
    public async Task<IActionResult> ConfirmEmail(string UserId,string token)
    {
        if(string.IsNullOrWhiteSpace(UserId) || string.IsNullOrWhiteSpace(token))
        {
            return BadRequest();
        }
        AppUser user =await _userManager.FindByIdAsync(UserId);
        if(user is null)
        {
            return NotFound();
        }
        var result =await _userManager.ConfirmEmailAsync(user,token);
        if (!result.Succeeded)
        {
            return BadRequest();
        }
        await _signInManager.SignInAsync(user, false);
        return RedirectToAction("Index","Home");

        
    }



    [HttpGet]
    public IActionResult VerifyEmail()
    {
        return View();
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
            ModelState.AddModelError("", "Email, Username or password is wrong");
            return View(request);
        }

        var result = await _signInManager.PasswordSignInAsync(user, request.Password, false, false);

        if (!result.Succeeded)
        {
            ModelState.AddModelError("", "Email, Username or password is wrong");
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
    //public async Task<ActionResult> CreateRoles()
    //{
    //    foreach (var role in Enum.GetValues(typeof(Roles)))
    //    {
    //        if (!await _roleManager.RoleExistsAsync(role.ToString()))
    //        {
    //            await _roleManager.CreateAsync(new IdentityRole { Name = role.ToString() });
    //        }
    //    }
    //    return Ok();
    //}
}
