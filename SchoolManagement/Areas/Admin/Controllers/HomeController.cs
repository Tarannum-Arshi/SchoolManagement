using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolManagement.Areas.Admin.Controllers
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
