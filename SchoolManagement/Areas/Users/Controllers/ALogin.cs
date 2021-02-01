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
    public class ALogin : Controller
    {
        public readonly IUnitOfWork _unitOfWork;
        public IActionResult NewRegister()
        {
            return View();
        }

        
    }
}
