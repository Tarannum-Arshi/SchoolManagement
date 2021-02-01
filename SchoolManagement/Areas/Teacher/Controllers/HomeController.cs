﻿using Microsoft.AspNetCore.Mvc;
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
        public readonly IUnitOfWork _unitOfWork;
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
                public IActionResult Register(UserModel UserModel)
                {
                    //UserModel.Role = "u";
                    
                        _unitOfWork.UserModel.Add(UserModel);

                        _unitOfWork.Save();
                        return RedirectToAction("Register", "Home", new { area = "Admin" });
                    
                }
        public IActionResult Register()
        {
            UserModel Index = new UserModel();
            return View(Index);
        }

       
                
            
           
        }
        

    }
