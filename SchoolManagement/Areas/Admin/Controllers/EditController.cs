using Dapper;
using Microsoft.AspNetCore.Mvc;
using SchoolManagement.DataAccess.Repository.IRepository;
using SchoolManagement.Models.ViewModels;
using SchoolManagement.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolManagement.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class EditController : Controller
    {
        public readonly IUnitOfWork _unitOfWork;

        public EditController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult StudentSearch()
        { 
           
            return View();
        }
        public IActionResult EditStudent(int? id)
        {
            UserModel usermodel = new UserModel();


            string claimvalue = User.FindFirst("id").Value;
            int UserId = Convert.ToInt32(claimvalue);

            usermodel = _unitOfWork.UserModel.Get(UserId);
            if (usermodel == null)
            {
                return NotFound();
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult StudentSearch(string FirstName)
        {
           
            var parameters = new DynamicParameters();
            parameters.Add("stFirstName", FirstName);
            if (ModelState.IsValid)
            {
                var obj=_unitOfWork.SPCall.List<StudentDetails>(SD.GetStudentDetails, parameters);

                _unitOfWork.Save();
                return View(obj);
                //Vaibhav
            }

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public IActionResult EditStudent(string FirstName, string LastName, string Gender, DateTime DOB, string Email, string Password, int Class)
        {
            UserModel usermodel = new UserModel()
            {
                FirstName = FirstName,
                LastName = LastName,
                Gender = Gender,
                DOB = DOB,
                Email = Email,
                Password = Password
            };
            var parameters = new DynamicParameters();
            parameters.Add("stEmail", Email);
            StudentModel studentmodel = new StudentModel()
            {
                Class = Class
            };
            return View(_unitOfWork.SPCall.List<StudentModel>(SD.EditStudentDetails, parameters));
            return RedirectToAction("Index", "Admin", new { area = "Admin" });
        }
    }
}
