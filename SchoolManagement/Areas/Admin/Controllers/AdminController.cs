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
                _unitOfWork.SPCall.List<StudentDetailsModel>(SD.Stud_Reg, parameters);

                _unitOfWork.Save();
                return RedirectToAction("Index", "Admin", new { area = "Admin" });
                //Vaibhav
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
                return RedirectToAction("Index", "Admin", new { area = "Admin" });
                //Vaibhav
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
           
            if(ModelState.IsValid)
            {
               _unitOfWork.UserModel.Update(usermodel);
                _unitOfWork.Save();
            }
            return RedirectToAction("Index", "Admin", new { area = "Admin" });
            
        }

        


    }

}

    

   