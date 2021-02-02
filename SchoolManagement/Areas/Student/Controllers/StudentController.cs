using Dapper;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using SchoolManagement.DataAccess.Repository.IRepository;
using SchoolManagement.Models.ViewModels;
using SchoolManagement.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace SchoolManagement.Areas.Student.Controllers
{
    [Area("Student")]
    public class StudentController : Controller
    {
        public readonly IUnitOfWork _unitOfWork;

        public StudentController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Fee()
        {
            string claimvalue = User.FindFirst("id").Value;
            var parameters = new DynamicParameters();
            parameters.Add("UserId", claimvalue);
            var fee = _unitOfWork.SPCall.List<Fee>(SD.GetFee ,parameters);
            //var identity = new ClaimsIdentity(new[] {
            //new Claim("fee", Fee.FeeCharge.ToString()) 
            //}, CookieAuthenticationDefaults.AuthenticationScheme);

            //var principal = new ClaimsPrincipal(identity);
            //var login = HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

            return View(fee);
        }

    }
}

