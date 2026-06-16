using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Security.Claims;
using System.Text;
using VideoSummarization.Models;
using Microsoft.EntityFrameworkCore;

namespace VideoSummarization.Controllers
{
    public class AccountsController : Controller
    {
        public static string CreateHash(string password)
        {
            byte[] secretKey = Encoding.UTF8.GetBytes("key#%&12KEY");
            var SHA256 = new System.Security.Cryptography.HMACSHA256(secretKey);
            var data = Encoding.ASCII.GetBytes(password);
            data = SHA256.ComputeHash(data);
            return Encoding.ASCII.GetString(data);
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Register(User userInfo)
        {
            if (ModelState.IsValid)
            {
                userInfo.Password = CreateHash(userInfo.Password);
                VideoSummDBContext db = new VideoSummDBContext();
                userInfo.RoleId = 2;
                db.Users.Add(userInfo);
                db.SaveChanges();
                return RedirectToAction(nameof(Login));
            }
            else
            {
                return View(userInfo);
            }
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(User userInfo)
        {
            if (userInfo != null)
            {
                VideoSummDBContext db = new VideoSummDBContext();
                if (userInfo.Password != string.Empty && userInfo.Password != null)
                {
                    userInfo.Password = CreateHash(userInfo.Password);
                }
                User login = db.Users.Where(log => log.Email == userInfo.Email && log.Password == userInfo.Password).Include(r => r.Role).FirstOrDefault();
                if (login != null)
                {
                    //A claim is a statement about a subject by an issuer and
                    //represent attributes of the subject that are useful in the context of authentication and authorization operations.
                    var claims = new List<Claim>() {
                    new Claim(ClaimTypes.NameIdentifier,Convert.ToString(login.Id)),
                    new Claim(ClaimTypes.Name,login.Name),
                    new Claim(ClaimTypes.Role,login.Role.Name),
                    new Claim(ClaimTypes.Email,login.Email)
                    };
                    //Initialize a new instance of the ClaimsIdentity with the claims and authentication scheme
                    var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    //Initialize a new instance of the ClaimsPrincipal with ClaimsIdentity
                    var principal = new ClaimsPrincipal(identity);
                    //SignInAsync is a Extension method for Sign in a principal for the specified scheme.
                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                         principal, new AuthenticationProperties() { IsPersistent = false });

                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    return View(userInfo);
                }
            }
            else
            {
                return View();
            }
        }

        public async Task<IActionResult> LogOut()
        {
            //SignOutAsync is Extension method for SignOut
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            //Redirect to home page
            return RedirectToAction("Index", "Home");
        }
    }
}
