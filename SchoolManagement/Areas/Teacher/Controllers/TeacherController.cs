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
        public IActionResult TimeTable()
        {
            return View();
        }

        public IActionResult Routine()
        {
            return View();
        }
        public IActionResult Result()
        {
            return View();
        }
        public IActionResult ResultSearch()
        {
            return View();
        }
        public IActionResult Message()
        {
            return View();
        }
        public IActionResult UploadResult()
        {
            //StudentUserDetails student = new StudentUserDetails();
            //return View(student);
            var obj = _unitOfWork.SPCall.List<StudentUserDetails>(SD.GetStudentDetails, null);
            _unitOfWork.Save();
            return View(obj);
        }
        public IActionResult EditResult()
        {
            Subject subject = new Subject();
            if(subject==null)
            {
                return NotFound();
            }
            return View(subject);
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
        public IActionResult Result(Subject subject)
        {
            if(ModelState.IsValid)
            {
                _unitOfWork.Subject.Add(subject);
                _unitOfWork.Save();
                return RedirectToAction("Message", "Teacher", new { area="Teacher"});
            }
            return View(subject);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditResult(int id, Subject subject)
        {
            var parameters = new DynamicParameters();
            parameters.Add("subjectId", id);
            parameters.Add("firstName", subject.FirstName);
            parameters.Add("email", subject.Email);
            parameters.Add("class", subject.Class);
            parameters.Add("maths", subject.Maths);
            parameters.Add("science", subject.Science);
            parameters.Add("english", subject.English);
            parameters.Add("hindi", subject.English);
            parameters.Add("computer", subject.Computer);
            _unitOfWork.SPCall.List<Subject>(SD.EditResult, parameters);
            return RedirectToAction("ResultSearch", "Teacher", new { area = "Teacher" });
        }

        #region API CALLS
        [HttpGet]
        public IActionResult GetAll()
        {
            var obj = _unitOfWork.SPCall.List<Subject>(SD.GetResult, null);
            _unitOfWork.Save();
            return Json(new { data = obj });
        }
        #endregion
    }


}

