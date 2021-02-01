using Microsoft.AspNetCore.Mvc;
using SchoolManagement.DataAccess.Repository.IRepository;
using SchoolManagement.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolManagement.Areas.Student
{
    [Area("Student")]
    public class StudentController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public StudentController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SRegister(string FirstName, string LastName, string Gender, DateTime DOB, string Email, string Password)
        {
            StudentModel studentmodel = new StudentModel();
            if (ModelState.IsValid)
            {
                _unitOfWork.StudentModel.Add(studentmodel);

                _unitOfWork.Save();
                return RedirectToAction(nameof(Index));
            }
            return View(studentmodel);
        }
        public IActionResult SRegister()
        {
            StudentModel studentModel = new StudentModel();
            return View();
        }
    }
}
