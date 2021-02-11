using Dapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using SchoolManagement.DataAccess.Repository.IRepository;
using SchoolManagement.Models;
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
    [Authorize(Roles = SD.Admin)]
    public class EditController : Controller
    {
        public readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _host;
        public EditController(IUnitOfWork unitOfWork, IWebHostEnvironment host)
        {
            _unitOfWork = unitOfWork;
            _host = host;
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
        public IActionResult EditStudent(StudentDetails studentuser)
        {
            var parameters = new DynamicParameters();
            parameters.Add("inUserId", studentuser.UserId);
            parameters.Add("stFirstName", studentuser.FirstName);
            parameters.Add("stLastName", studentuser.LastName);
            parameters.Add("stGender", studentuser.Gender);
            parameters.Add("dtDOB", studentuser.DOB);
            parameters.Add("stEmail", studentuser.Email);
            parameters.Add("inClass", studentuser.Class);
            if (ModelState.IsValid)
            {
                string webRootPath = _host.WebRootPath;
                var files = HttpContext.Request.Form.Files;
                if (files.Count > 0)
                {
                    string fileName = Guid.NewGuid().ToString();
                    var uploads = Path.Combine(webRootPath, @"images");
                    var extension = Path.GetExtension(files[0].FileName);

                    if (studentuser.ImageUrl != null)
                    {
                        var imagePath = Path.Combine(webRootPath, studentuser.ImageUrl.TrimStart('\\'));
                        if (System.IO.File.Exists(imagePath))
                        {
                            System.IO.File.Delete(imagePath);
                        }
                    }
                    using (var fileStreams = new FileStream(Path.Combine(uploads, fileName + extension), FileMode.Create))
                    {
                        files[0].CopyTo(fileStreams);
                    }
                    studentuser.ImageUrl =fileName + extension;
                    parameters.Add("stImageUrl", studentuser.ImageUrl);
                }
                else
                {
                    if (id != 0)
                    {
                        UserModel objFromDb = _unitOfWork.UserModel.Get(id);
                        studentuser.ImageUrl = objFromDb.ImageUrl;
                        parameters.Add("stImageUrl", studentuser.ImageUrl);
                    }
                }
                _unitOfWork.SPCall.List<StudentDetails>(SD.EditStudentDetails, parameters);
                _unitOfWork.Save();
            }
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
            if (ModelState.IsValid)
            {
                string webRootPath = _host.WebRootPath;
                var files = HttpContext.Request.Form.Files;
                if (files.Count > 0)
                {
                    string fileName = Guid.NewGuid().ToString();
                    var uploads = Path.Combine(webRootPath, @"images");
                    var extension = Path.GetExtension(files[0].FileName);

                    if (teacheruser.ImageUrl != null)
                    {
                        var imagePath = Path.Combine(webRootPath, teacheruser.ImageUrl.TrimStart('\\'));
                        if (System.IO.File.Exists(imagePath))
                        {
                            System.IO.File.Delete(imagePath);
                        }
                    }
                    using (var fileStreams = new FileStream(Path.Combine(uploads, fileName + extension), FileMode.Create))
                    {
                        files[0].CopyTo(fileStreams);
                    }
                    teacheruser.ImageUrl =fileName + extension;

                    parameters.Add("stImageUrl", teacheruser.ImageUrl);
                }
                else
                {
                    if (id != 0)
                    {
                        UserModel objFromDb = _unitOfWork.UserModel.Get(id);
                        teacheruser.ImageUrl = objFromDb.ImageUrl;
                        parameters.Add("stImageUrl", teacheruser.ImageUrl);
                    }
                }
                _unitOfWork.SPCall.List<TeacherDetails>(SD.EditTeacherDetails, parameters);
            }
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
