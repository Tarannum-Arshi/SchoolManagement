using Microsoft.AspNetCore.Mvc;
using SchoolManagement.DataAccess.Repository.IRepository;
using SchoolManagement.Models.ViewModels;
using SchoolManagement.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolManagement.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class FeeController : Controller
    {
        public readonly IUnitOfWork _unitOfWork;

        public FeeController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {

            var allfee = _unitOfWork.SPCall.List<DuesFee>(SD.GetAllDueFee, null);
            return View(allfee);
        }
    }
}
