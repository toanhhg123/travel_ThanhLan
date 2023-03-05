using System.Diagnostics;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NToastNotify;
using source.Models;
using source.utils;

namespace source.Controllers;

public class AuthController : Controller
{
    private readonly ILogger<AuthController> _logger;
    private readonly TravelContext _dbCOntext;
    private readonly IToastNotification _toastNotification;

    public AuthController(ILogger<AuthController> logger, IToastNotification toastNotification, TravelContext travelContext)
    {
        _logger = logger;
        _toastNotification = toastNotification;
        _dbCOntext = travelContext;
    }

   public IActionResult Login(string? returnUrl)
        {
            returnUrl = returnUrl ?? "/";
            ViewBag.returnUrl = returnUrl;
            if (User?.Identity?.IsAuthenticated == true)
                return Redirect(returnUrl);
            return View();
        }



        [HttpPost]
        public async Task<IActionResult> Login(string email, string password, string returnUrl)
        {
            returnUrl = returnUrl ?? "/";
            ViewBag.returnUrl = returnUrl;


            try
            {
                Account user = validateUser(email, password);
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.email),
                    new Claim("username", user.userName),
                    new Claim(ClaimTypes.NameIdentifier, user.id.ToString()),
                    new Claim(ClaimTypes.Role, user.Role.RoleName),
                };

                var claimsIdentity = new ClaimsIdentity(
                    claims, CookieAuthenticationDefaults.AuthenticationScheme);

                var authProperties = new AuthenticationProperties
                {
                };

                await HttpContext.SignInAsync(
                    scheme: "TRAVEL",
                    new ClaimsPrincipal(claimsIdentity),
                    authProperties);
                return Redirect(returnUrl);
            }
            catch (Exception ex)
            {
                ViewBag.errorMessage = ex.Message;
                return View();
            }
        }

        public async Task<IActionResult> Logout(string? returnUrl)
        {
            returnUrl = returnUrl ?? "/auth/login";

            await HttpContext.SignOutAsync(
            scheme: "TRAVEL");

            return Redirect(returnUrl);
        }

        public async Task<IActionResult> Forbidden()
        {

            
            await HttpContext.SignOutAsync(
            scheme: "TRAVEL");
            return View();
        }

        private Account validateUser(string email, string password)
        {
            try
            {
                var user = _dbCOntext.Accounts.Include(x => x.Role).FirstOrDefault(x => x.email == email);
                if (user == null)
                    throw new Exception("email is incorrect ");
                if (!MD5Password.ComparePassword(password, user.hashPassword))
                    throw new Exception("password is incorrect");
                return user;
            }
            catch (System.Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }


   

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
