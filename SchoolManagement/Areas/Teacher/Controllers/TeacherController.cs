using Dapper;
using Microsoft.AspNetCore.Mvc;
using SchoolManagement.DataAccess.Repository.IRepository;
using SchoolManagement.Models.ViewModels;
using SchoolManagement.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolManagement.Areas.Teacher.Controllers
{
    [Area("Teacher")]
    public class TeacherController : Controller
    {
        public readonly IUnitOfWork _unitOfWork;

        public TeacherController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Result()
        {
            return View();
        }
        public IActionResult CreateResult()
        {
            Subject subject = new Subject();
            return View(subject);
        }
        public IActionResult ViewResult()
        {
            return View();
        }
        public IActionResult EditResult()
        {
            Subject subject = new Subject();
            return View(subject);
        }
        public IActionResult Routine()
        {
            return View();
        }
        public IActionResult Leave()
        {
            TeacherModel teacher = new TeacherModel();
            string claimvalue = User.FindFirst("id").Value;
            int UserId = Convert.ToInt32(claimvalue);
            teacher = _unitOfWork.TeacherModel.GetFirstOrDefault(a => a.UserId == UserId);
            return View(teacher);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Leave(TeacherModel teacher)
        {
            TeacherModel teachers = new TeacherModel();
            string claimvalue = User.FindFirst("id").Value;
            int UserId = Convert.ToInt32(claimvalue);
            teachers = _unitOfWork.TeacherModel.GetFirstOrDefault(a => a.UserId == UserId);
            var parameters = new DynamicParameters();
            parameters.Add("inTeacherId", teachers.TeacherId);
            parameters.Add("inLeaveDays", teacher.LeaveDays);
            parameters.Add("dtStartDate", teacher.StartDate);
                _unitOfWork.SPCall.Execute(SD.ApplyForLeave, parameters);
                _unitOfWork.Save();
                return RedirectToAction("Index","Teacher",new { area = "Teacher" });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateResult(int id, Subject subject)
        {
            var parameters = new DynamicParameters();
            parameters.Add("inUserId", id);
            parameters.Add("inMaths", subject.Maths);
            parameters.Add("inScience", subject.Science);
            parameters.Add("inEnglish", subject.English);
            parameters.Add("inHindi", subject.Hindi);
            parameters.Add("inComputer", subject.Computer);
            if (ModelState.IsValid)
            {
                _unitOfWork.SPCall.List<Subject>(SD.InsertSubject, parameters);
                _unitOfWork.Save();
                return RedirectToAction("Index", "Teacher", new { area = "Teacher" });
            }
            return View(subject);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditResult(int id, Subject subject)
        {
            var parameters = new DynamicParameters();
            parameters.Add("inUserId", id);
            parameters.Add("inMaths", subject.Maths);
            parameters.Add("inScience", subject.Science);
            parameters.Add("inEnglish", subject.English);
            parameters.Add("inHindi", subject.Hindi);
            parameters.Add("inComputer", subject.Computer);
            if(ModelState.IsValid)
            {
                _unitOfWork.SPCall.List<Subject>(SD.EditResult, parameters);
            }
            return RedirectToAction("Result", "Teacher", new { area = "Teacher" });
        }
        #region API CALLS
        [HttpGet]
        public IActionResult GetStudent()
        {
            var obj = _unitOfWork.SPCall.List<StudentUserDetails>(SD.GetStudentDetails, null);
            return Json(new { data = obj });
        }
        #endregion
    }
}

