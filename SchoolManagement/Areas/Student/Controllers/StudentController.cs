using Microsoft.AspNetCore.Mvc;
using SchoolManagement.DataAccess.Repository.IRepository;
using SchoolManagement.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolManagement.Areas.Student.Controllers
{
    [Area("Student")]
    public class StudentController : Controller
    {
        public readonly IUnitOfWork _unitOfWork;

        public StudentController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            return View();
        }
      
    }
        

}

