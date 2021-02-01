using Microsoft.AspNetCore.Mvc;
using SchoolManagement.DataAccess.Repository.IRepository;
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
                
            
           
    }
        

}

