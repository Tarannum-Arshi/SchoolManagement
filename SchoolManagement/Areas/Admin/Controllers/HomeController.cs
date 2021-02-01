using Microsoft.AspNetCore.Mvc;
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
        public IActionResult Register(string firstName, string lastName, string gender, DateTime dob, string email, string password)
        {
            UserModel usermodel = new UserModel();
            usermodel.FirstName = firstName;
            usermodel.LastName = lastName;
            usermodel.Gender = gender;
            usermodel.DOB = dob;
            usermodel.Email = email;
            usermodel.Password = password;
            usermodel.Role = "u";

            if (ModelState.IsValid)
            {
                _unitOfWork.UserModel.Add(usermodel);

                _unitOfWork.Save();
                return RedirectToAction(nameof(Index));
            }
            return View(usermodel);
        }
        public IActionResult Register()
        {
            UserModel UserModel = new UserModel();
            return View(UserModel);
        }

    }
}
