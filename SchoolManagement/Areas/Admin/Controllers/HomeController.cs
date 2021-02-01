using Microsoft.AspNetCore.Mvc;
using SchoolManagement.DataAccess.Data.Repository.IRepository;
using SchoolManagement.DataAccess.Repository.IRepository;
using SchoolManagement.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolManagement.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class HomeController : Controller
    {
        public readonly IUnitOfWork _unitOfWork;
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Register(UserModel usermodel)
        {
            usermodel.Role = "u";


            _unitOfWork.UserModel.Add(usermodel);

                _unitOfWork.Save();
                return RedirectToAction("Register", "Home", new { area="Admin"});
            
           
        }
        public IActionResult Register()
        {
            UserModel UserModel = new UserModel();
            return View(UserModel);
        }

    }
}
