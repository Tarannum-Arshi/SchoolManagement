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
        public IActionResult StudentSearch1()
        {
            return View();
        }
        public IActionResult TeacherSearch1()
        {
            return View();
        }
        public IActionResult EditStudent()
        {
            StudentDetails student = new StudentDetails();
            if (student == null)
            {
                return NotFound();
            }
            return View(student);
        }
        public IActionResult EditTeacher()
        {
            TeacherDetails teacher = new TeacherDetails();
            if(teacher==null)
            {
                return NotFound();
            }
            return View(teacher);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditStudent(int id, StudentDetails studentuser)
        {
            var parameters = new DynamicParameters();
            parameters.Add("inUserId", id);
            parameters.Add("stFirstName", studentuser.FirstName);
            parameters.Add("stLastName", studentuser.LastName);
            parameters.Add("stGender", studentuser.Gender);
            parameters.Add("dtDOB", studentuser.DOB);
            parameters.Add("stEmail", studentuser.Email);
            parameters.Add("inClass", studentuser.Class);
            _unitOfWork.SPCall.List<StudentDetails>(SD.EditStudentDetails, parameters);
            return RedirectToAction("StudentSearch1", "Edit", new { area = "Admin" });
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditTeacher(int id, TeacherDetails teacheruser)
        {
            var parameters = new DynamicParameters();
            parameters.Add("inUserId", id);
            parameters.Add("stFirstName", teacheruser.FirstName);
            parameters.Add("stLastName", teacheruser.LastName);
            parameters.Add("stGender", teacheruser.Gender);
            parameters.Add("dtDOB", teacheruser.DOB);
            parameters.Add("stEmail", teacheruser.Email);
            parameters.Add("inSalary", teacheruser.Salary);
            _unitOfWork.SPCall.List<TeacherDetails>(SD.EditTeacherDetails, parameters);
            return RedirectToAction("TeacherSearch1", "Edit", new { area = "Admin" });
        }
        #region API CALLS
        [HttpGet]
        public IActionResult GetAll()
        {
            var obj = _unitOfWork.SPCall.List<StudentUserDetails>(SD.GetStudentDetails, null);
            _unitOfWork.Save();
            return Json(new { data = obj });
        }
        public IActionResult GetAll1()
        {
            var obj1 = _unitOfWork.SPCall.List<TeacherUserDetails>(SD.GetTeacherDetails, null);
            _unitOfWork.Save();
            return Json(new { data = obj1 });
        }
        #endregion
    }
}
