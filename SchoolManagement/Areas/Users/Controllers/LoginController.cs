using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using SchoolManagement.Models.ViewModels;
using SchoolManagement.DataAccess.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace SchoolManagement.Areas.Users.Controllers
{
    public class LoginController : Controller
    {
        public readonly IUnitOfWork _unitOfWork;
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public IActionResult Login(string email, string password)
        {


            if (ModelState.IsValid)
            {
                var admin = _unitOfWork.UserModel.GetFirstOrDefault(a => a.Email == email && a.Password == password);
                if (admin != null)
                {
                    var identity = new ClaimsIdentity(new[] {
                    new Claim("id", admin.UserId.ToString()),
                    new Claim("names", admin.FirstName.ToString()),
                    new Claim("email", admin.Email.ToString()),
                    new Claim(ClaimTypes.Role, admin.Role)
                    }, CookieAuthenticationDefaults.AuthenticationScheme);

                    var principal = new ClaimsPrincipal(identity);

                    var login = HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

                    return RedirectToAction("Index", "User", new { area = "Reader" });
                }
                else
                {
                    ViewData["Message"] = "Incorrect username or password";
                    return RedirectToAction(nameof(Login));

                }
            }

                return View();
        }
    }
}
