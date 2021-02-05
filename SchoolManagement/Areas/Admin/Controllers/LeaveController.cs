using Dapper;
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
    public class LeaveController : Controller
    {
        public readonly IUnitOfWork _unitOfWork;
        
        public LeaveController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Approved(int id)
        {
            var parameters = new DynamicParameters();
            parameters.Add("inTeacherId", id);
            _unitOfWork.SPCall.List<Leaves>(SD.ApprovedLeaves, parameters);
            return View();
        }

        #region API CALLS
        [HttpGet]
        public IActionResult GetAll()
        {
            var allleave = _unitOfWork.SPCall.List<Leaves>(SD.PendingLeaves, null);
            return Json(new { data = allleave });
        }

        #endregion
    }
}
