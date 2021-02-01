using Microsoft.AspNetCore.Mvc;
using SchoolManagement.DataAccess.Data.Repository.IRepository;
using SchoolManagement.Models.ViewModels;
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
        [HttpPost]
        public IActionResult Register(UserModel UserModel)
        {
            UserModel.Role = "u";

            if (ModelState.IsValid)
            {
                _unitOfWork.UserModel.Add(UserModel);

                _unitOfWork.Save();
                return RedirectToAction(nameof(Index));
            }
            return View(UserModel);
        }

    }
}
