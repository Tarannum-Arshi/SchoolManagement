﻿using Dapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using SchoolManagement.DataAccess.Repository.IRepository;
using SchoolManagement.Models.ViewModels;
using SchoolManagement.Utility;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolManagement.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AdminController : Controller
    {
        public readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _host;

        public AdminController(IUnitOfWork unitOfWork , IWebHostEnvironment host)
        {
            _unitOfWork = unitOfWork;
            _host = host;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Register()
        {
            UserModel usermodel = new UserModel();
            return View();
        }

        public IActionResult StudentRegister()
        {
            return View();
        }

        public IActionResult TeacherRegister()
        {
            return View();
        }

        public IActionResult AddClass()
        {
            var usermodels = _unitOfWork.SPCall.List<Drop>(SD.Drop, null);
            ViewBag.Data = usermodels;
            return View();
        }

        public IActionResult EditAdmin(int? id)
        {
            UserModel usermodel = new UserModel();
            string claimvalue = User.FindFirst("id").Value;
            int UserId = Convert.ToInt32(claimvalue);

            usermodel = _unitOfWork.UserModel.Get(UserId);

            if (usermodel==null)
            {
                return NotFound();
            }
            return View(usermodel);
        }

        

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Register(UserModel usermodel)
        {
            usermodel.Role = "a";

            if (ModelState.IsValid)
            {
                _unitOfWork.UserModel.Add(usermodel);

                _unitOfWork.Save();
                return View();
            }
            return View(usermodel);
        }
       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult StudentRegister(StudentUserDetails student)
        {
            var parameters = new DynamicParameters();
            parameters.Add("stFirstName", student.FirstName);
            parameters.Add("stLastName", student.LastName);
            parameters.Add("stGender", student.Gender);
            parameters.Add("dtDOB", student.DOB);
            parameters.Add("stEmail", student.Email);
            parameters.Add("stPassword", student.Password);
            parameters.Add("inClass", student.Class);
          
            if (ModelState.IsValid)
            {
                _unitOfWork.SPCall.List<StudentModel>(SD.Stud_Reg, parameters);

                _unitOfWork.Save();
                string emailbody = GetBody("welcome", student.FirstName, student.Email, student.Password);
                EmailConfig.SendMail(student.Email, "Welcome", emailbody);

                return RedirectToAction("Index", "Admin", new { area = "Admin" });
                
            }
            return View(student);
        }
        

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult TeacherRegister(TeacherUserDetails teacher)
        {
            var parameters = new DynamicParameters();
            parameters.Add("stFirstName", teacher.FirstName);
            parameters.Add("stLastName", teacher.LastName);
            parameters.Add("stGender", teacher.Gender);
            parameters.Add("dtDOB", teacher.DOB);
            parameters.Add("stEmail", teacher.Email);
            parameters.Add("stPassword", teacher.Password);
            parameters.Add("inSalary", teacher.Salary);
            if (ModelState.IsValid)
            {
                _unitOfWork.SPCall.List<TeacherModel>(SD.Teacher_Reg, parameters);
                _unitOfWork.Save();
                string emailbody = GetBody("welcome", teacher.FirstName, teacher.Email, teacher.Password);
                EmailConfig.SendMail(teacher.Email, "Welcome", emailbody);

                return RedirectToAction("Index", "Admin", new { area = "Admin" });

                
            }
            return View(teacher);
        }
        
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddClass(AddClass add)
        {
            
            var parameters = new DynamicParameters();
            parameters.Add("TeacherId", add.TeacherId);
            parameters.Add("UserId", add.UserId);
            parameters.Add("Class", add.Class);
            parameters.Add("FeeCharge", add.FeeCharge);
            _unitOfWork.SPCall.List<ClassModel>(SD.InsertClass, parameters);
            return RedirectToAction("Index", "Admin", new { area = "Admin" });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public IActionResult EditAdmin(UserModel usermodel)
        {
            string claimvalue = User.FindFirst("id").Value;
            int UserId = Convert.ToInt32(claimvalue);
            var parameters = new DynamicParameters();
            parameters.Add("inUserId", UserId);
            parameters.Add("stFirstName", usermodel.FirstName);
            parameters.Add("stLastName", usermodel.LastName);
            parameters.Add("stGender", usermodel.Gender);
            parameters.Add("dtDOB", usermodel.DOB);
            parameters.Add("stEmail", usermodel.Email);
            parameters.Add("stPassword", usermodel.Password);
            if (ModelState.IsValid)
            {
                _unitOfWork.SPCall.List<ClassModel>(SD.EditAdminDetails, parameters);
                _unitOfWork.Save();
            }
            return RedirectToAction("Index", "Admin", new { area = "Admin" });
            
        }

        #region GetEmailBody

        public string GetBody(string type, string Name = " ", string UserId = " ", string Password = " ", string Notice = " ", string Month = " ", string Amount = " ", string Status = " ", string Assignment = " ")
        {
            string str = null;


            switch (type)
            {
                case "welcome":
                    using (StreamReader reader = new StreamReader(Path.Combine(_host.WebRootPath, "EmailTemplates/Welcome.html")))
                    {
                        str = reader.ReadToEnd();
                    }

                    str = str.Replace("{Name}", Name);
                    str = str.Replace("{UserId}", UserId);
                    str = str.Replace("{Password}", Password);


                    return str;

                    break;

                case "notice":
                    using (StreamReader reader = new StreamReader(Path.Combine(_host.WebRootPath, "EmailTemplates/Notice.html")))
                    {
                        str = reader.ReadToEnd();
                    }

                    str = str.Replace("{Name}", Name);
                    str = str.Replace("{Notice}", Notice);


                    return str;
                    break;

                case "feepayment":
                    using (StreamReader reader = new StreamReader(Path.Combine(_host.WebRootPath, "EmailTemplates/FeePayment.html")))
                    {
                        str = reader.ReadToEnd();
                    }

                    str = str.Replace("{Name}", Name);
                    str = str.Replace("{Month}", Month);
                    str = str.Replace("{Amount}", Amount);
                    str = str.Replace("{Status}", Status);

                    return str;
                    break;

                case "feereminder":
                    using (StreamReader reader = new StreamReader(Path.Combine(_host.WebRootPath, "EmailTemplates/FeeReminder.html")))
                    {
                        str = reader.ReadToEnd();
                    }

                    str = str.Replace("{Name}", Name);
                    str = str.Replace("{Month}", Month);

                    return str;
                    break;

                case "assignment":
                    using (StreamReader reader = new StreamReader(Path.Combine(_host.WebRootPath, "EmailTemplates/Assignment.html")))
                    {
                        str = reader.ReadToEnd();
                    }

                    str = str.Replace("{Name}", Name);
                    str = str.Replace("{Assignment}", Assignment);

                    return str;
                    break;


            }
            return str;

        }

        #endregion

    }

}

    

   