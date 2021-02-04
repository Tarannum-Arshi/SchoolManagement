using Dapper;
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
       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult StudentRegister(string FirstName, string LastName, string Gender, DateTime DOB, string Email, string Password, int Class)
        {
            UserModel usermodel = new UserModel();
            usermodel.FirstName = FirstName;
            usermodel.LastName = LastName;
            usermodel.Gender = Gender;
            usermodel.DOB = DOB;
            usermodel.Email = Email;
            usermodel.Password = Password;
            StudentModel studentmodel = new StudentModel();
            studentmodel.Class = Class;
            var parameters = new DynamicParameters();
            parameters.Add("stFirstName", FirstName);
            parameters.Add("stLastName", LastName);
            parameters.Add("stGender", Gender);
            parameters.Add("dtDOB", DOB);
            parameters.Add("stEmail", Email);
            parameters.Add("stPassword", Password);
            parameters.Add("inClass", Class);
          
            if (ModelState.IsValid)
            {
                _unitOfWork.SPCall.List<StudentModel>(SD.Stud_Reg, parameters);

                _unitOfWork.Save();
                string emailbody = GetBody("welcome", FirstName, Email, Password);
                EmailConfig.SendMail(Email, "Welcome", emailbody);

                return RedirectToAction("Index", "Admin", new { area = "Admin" });
                
            }
            return View(usermodel);
        }
        

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult TeacherRegister(string FirstName, string LastName, string Gender, DateTime DOB, string Email, string Password, int Salary)
        {
            UserModel usermodel = new UserModel();
            usermodel.FirstName = FirstName;
            usermodel.LastName = LastName;
            usermodel.Gender = Gender;
            usermodel.DOB = DOB;
            usermodel.Email = Email;
            usermodel.Password = Password;
            TeacherModel teachermodel = new TeacherModel();
            teachermodel.Salary = Salary;
            var parameters = new DynamicParameters();
            parameters.Add("stFirstName", FirstName);
            parameters.Add("stLastName", LastName);
            parameters.Add("stGender", Gender);
            parameters.Add("dtDOB", DOB);
            parameters.Add("stEmail", Email);
            parameters.Add("stPassword", Password);
            parameters.Add("inSalary", Salary);
            if (ModelState.IsValid)
            {
                _unitOfWork.SPCall.List<TeacherModel>(SD.Teacher_Reg, parameters);
                _unitOfWork.Save();
                string emailbody = GetBody("welcome", FirstName, Email, Password);
                EmailConfig.SendMail(Email, "Welcome", emailbody);

                return RedirectToAction("Index", "Admin", new { area = "Admin" });

                
            }
            return View(usermodel);
        }
        
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddClass(int TeacherId, int UserId, int Class, int FeeCharge)
        {
            ClassModel classmodel = new ClassModel();
            TeacherModel teachermodel = new TeacherModel();
            teachermodel.TeacherId = UserId;
            classmodel.TeacherId = UserId;
            classmodel.Class = Class;
            classmodel.FeeCharge = FeeCharge;
            var parameters = new DynamicParameters();
            parameters.Add("inTeacherId", UserId);
            parameters.Add("inClass", Class);
            parameters.Add("inFeeCharge", FeeCharge);
            _unitOfWork.SPCall.List<ClassModel>(SD.ClassCreate, parameters);
            return RedirectToAction("Index", "Admin", new { area = "Admin" });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public IActionResult EditAdmin(int UserId, string FirstName, string LastName, string Gender, DateTime DOB, string Email, string Password)
        {
            UserModel usermodel = new UserModel()
           {
                UserId = UserId,
                FirstName = FirstName,
                LastName = LastName,
                Gender = Gender,
                DOB = DOB,
                Email = Email,
                Password = Password
            };
            if(ModelState.IsValid)
            {
               _unitOfWork.UserModel.Update(usermodel);
                _unitOfWork.Save();
            }
            return RedirectToAction("Index", "Admin", new { area = "Admin" });
            
        }


        #region GetEmailBody

        public string GetBody(string type, string Name=" ", string UserId=" " ,string Password=" ", string Notice = " " , string Month=" ", string Amount= " ", string Status=" " , string Assignment= " ")
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

    

   