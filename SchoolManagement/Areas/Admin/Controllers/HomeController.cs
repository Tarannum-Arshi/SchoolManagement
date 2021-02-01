using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SchoolManagement.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolManagement.Area.Admin.Controllers
{
    [Area("Admin")]
    public class HomeController : Controller
    {

        public IActionResult Index()
        {
            return View();
        }
       // [HttpPost]
        //public IActionResult Register(Admin admin)
        //{
        //    admin.Category = "u";

        //    if (ModelState.IsValid)
        //    {
        //        _unitOfWork.Admin.Add(admin);

        //        _unitOfWork.Save();
        //        return RedirectToAction(nameof(Index));
        //    }
        //    return View(admin);
        //}

        
    }
}
