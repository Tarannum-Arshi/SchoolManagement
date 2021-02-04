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

        public IActionResult Leave()
        {

             TeacherModel teacher = new TeacherModel();
            string claimvalue = User.FindFirst("id").Value;
            int UserId = Convert.ToInt32(claimvalue);
            teacher = _unitOfWork.TeacherModel.Get(UserId);

            return View(teacher);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public IActionResult Leave(TeacherModel teacher)
        {
            TeacherModel teachers = new TeacherModel();
            string claimvalue = User.FindFirst("id").Value;
            int UserId = Convert.ToInt32(claimvalue);
            teachers = _unitOfWork.TeacherModel.Get(UserId);
            var parameters = new DynamicParameters();
            parameters.Add("inTeacherId", teachers.TeacherId);
            parameters.Add("inLeaveDays", teacher.LeaveDays);
            parameters.Add("dtStartDate", teacher.StartDate);

                _unitOfWork.SPCall.Execute(SD.ApplyForLeave, parameters);

                _unitOfWork.Save();
                return RedirectToAction("Index","Teacher",new { area = "Teacher" });
        }

    }
        

}

