using Microsoft.AspNetCore.Mvc;
using SchoolManagement.DataAccess.Data.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolManagement.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class HomeController : Controller
    {
        public readonly IUnitOfWork _unitOfWork;
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Register(Admin admin)
        {
            admin.Category = "u";

            if (ModelState.IsValid)
            {
                _unitOfWork.Admin.Add(admin);

                _unitOfWork.Save();
                return RedirectToAction(nameof(Index));
            }
            return View(admin);
        }

    }
}
