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
    public class AdminController : Controller
    {
        public readonly IUnitOfWork _unitOfWork;

        public AdminController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Register(string FirstName, string LastName, string Gender, DateTime DOB, string Email, string Password)
        {
            UserModel usermodel = new UserModel();
            usermodel.Role = "a";

            if (ModelState.IsValid)
            {
                _unitOfWork.UserModel.Add(usermodel);

                _unitOfWork.Save();
                return View();
            }
            return View(usermodel);
        }
        public IActionResult Register()
        {
            UserModel usermodel = new UserModel();
            return View();
        }

        public IActionResult NewRegister()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SRegister(string FirstName, string LastName, string Gender, DateTime DOB, string Email, string Password, int ClassId)
        {
            UserModel usermodel = new UserModel();
            usermodel.FirstName = FirstName;
            usermodel.LastName = LastName;
            usermodel.Gender = Gender;
            usermodel.DOB = DOB;
            usermodel.Email = Email;
            usermodel.Password = Password;
            StudentModel studentmodel = new StudentModel();
            studentmodel.ClassId = ClassId;
            var parameters = new DynamicParameters();
            parameters.Add("FirstName", FirstName);
            parameters.Add("LastName", LastName);
            parameters.Add("Gender", Gender);
            parameters.Add("DOB", DOB);
            parameters.Add("Email", Email);
            parameters.Add("Password", Password);
            parameters.Add("ClassId", ClassId);
          
            if (ModelState.IsValid)
            {
                _unitOfWork.SPCall.List<StudentModel>(SD.Stud_Reg, parameters);

                _unitOfWork.Save();
                return RedirectToAction("Index", "Admin", new { area="Admin"});
                //Vaibhav
            }
            return View(usermodel);
        }
        public IActionResult SRegister()
        {
            UserModel usermodel = new UserModel();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult TRegister(string FirstName, string LastName, string Gender, DateTime DOB, string Email, string Password, int Salary,)
        {
            UserModel usermodel = new UserModel();
            usermodel.FirstName = FirstName;
            usermodel.LastName = LastName;
            usermodel.Gender = Gender;
            usermodel.DOB = DOB;
            usermodel.Email = Email;
            usermodel.Password = Password;
            TeacherModel teachermodel = new TeacherModel();
            var parameters = new DynamicParameters();
            parameters.Add("FirstName", FirstName);
            parameters.Add("LastName", LastName);
            parameters.Add("Gender", Gender);
            parameters.Add("DOB", DOB);
            parameters.Add("Email", Email);
            parameters.Add("Password", Password);
            //usermodel.Role = "t";
            if (ModelState.IsValid)
            {
                _unitOfWork.UserModel.Add(usermodel);

                _unitOfWork.Save();
                return RedirectToAction("Index", "Admin", new { area = "Admin" });
                //Vaibhav
            }
            return View(usermodel);
        }
        public IActionResult TRegister()
        {
            UserModel usermodel = new UserModel();
            return View();
        }
        public IActionResult SEdit(int? id)
        {
            UserModel usermodel = new UserModel();
            usermodel = _unitOfWork.UserModel.Get(id.GetValueOrDefault());
            if(usermodel==null)
            {
                return NotFound();
            }
            return View(usermodel);
        }


    }
        

    }

