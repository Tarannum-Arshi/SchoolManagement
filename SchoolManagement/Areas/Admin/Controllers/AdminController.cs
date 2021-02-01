using Microsoft.AspNetCore.Mvc;
using SchoolManagement.DataAccess.Repository.IRepository;
using SchoolManagement.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolManagement.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AdminController : Controller
    {
        public readonly IUnitOfWork _unitOfWork;

        public AdminController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Register(UserModel usermodel)
        {
            usermodel.Role = "a";

            if (ModelState.IsValid)
            {
                _unitOfWork.UserModel.Add(usermodel);

                _unitOfWork.Save();
                return RedirectToAction(nameof(Index));
            }
            return View(usermodel);
        }
        public IActionResult Register()
        {
            UserModel Index = new UserModel();
            return View(Index);
        }

       
                
            
           
        }
        

    }

