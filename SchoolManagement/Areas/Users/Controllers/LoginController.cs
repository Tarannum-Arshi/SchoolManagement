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
using SchoolManagement.Utility;

namespace SchoolManagement.Areas.Users.Controllers
{
    [Area("Users")]
    public class LoginController : Controller
    {
        public readonly IUnitOfWork _unitOfWork;
        public LoginController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Login()
        {
            return View();
        }
        public IActionResult Logout()
        {
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Login", new { area = "Users" });
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
                    new Claim(ClaimTypes.Role, admin.Role),
                    new Claim("image", admin.ImageUrl.ToString()),
                    }, CookieAuthenticationDefaults.AuthenticationScheme);

                    var principal = new ClaimsPrincipal(identity);

                    var login = HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

                    string value = admin.Role.ToString();
                    if (value== SD.Admin)
                    {
                        return RedirectToAction("Index", "Admin", new { area = "Admin" });
                    }
                    else if(value == SD.Teacher)
                    {
                        return RedirectToAction("Index", "Teacher", new { area = "Teacher" });
                    }
                    else if(value == SD.Student)
                    {
                        return RedirectToAction("Index", "Student", new { area = "Student" });
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Wrong username and password");

                }
            }

                return View();
        }
    }
}
