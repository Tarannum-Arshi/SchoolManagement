using Dapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using SchoolManagement.DataAccess.Repository.IRepository;
using SchoolManagement.Models.ViewModels;
using SchoolManagement.Utility;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolManagement.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class LeaveController : Controller
    {
        public readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _host;

        public LeaveController(IUnitOfWork unitOfWork, IWebHostEnvironment host)
        {
            _unitOfWork = unitOfWork;
            _host = host;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Approved(int id)
        {
            ///var id
            var parameters = new DynamicParameters();
            parameters.Add("inTeacherId", id);
            _unitOfWork.SPCall.List<Leaves>(SD.ApprovedLeaves, parameters);
            var teacher= _unitOfWork.SPCall.List<Leaves>(SD.GetTeacherName, parameters);
            foreach (var i in teacher)
            {
                string emailbody = GetBody("leaveapproved", i.Name);
                EmailConfig.SendMail(i.Email, "Leave Approved", emailbody);
            }
                return View();
        }


        #region GetEmailBody

        public string GetBody(string type, string Name = " ", string UserId = " ", string Password = " ", string Notice = " ", string Month = " ", string Amount = " ", string Status = " ", string Assignment = " ")
        {
            string str = null;


            switch (type)
            {
                case "welcome":
                    using (StreamReader reader = new StreamReader(Path.Combine(_host.WebRootPath, "EmailTemplates/Welcome.html")))
                    {
                        str = reader.ReadToEnd();
                    }

                    str = str.Replace("{Name}", Name);
                    str = str.Replace("{UserId}", UserId);
                    str = str.Replace("{Password}", Password);


                    return str;

                    break;

                case "notice":
                    using (StreamReader reader = new StreamReader(Path.Combine(_host.WebRootPath, "EmailTemplates/Notice.html")))
                    {
                        str = reader.ReadToEnd();
                    }

                    str = str.Replace("{Name}", Name);
                    str = str.Replace("{Notice}", Notice);


                    return str;
                    break;

                case "feepayment":
                    using (StreamReader reader = new StreamReader(Path.Combine(_host.WebRootPath, "EmailTemplates/FeePayment.html")))
                    {
                        str = reader.ReadToEnd();
                    }

                    str = str.Replace("{Name}", Name);
                    str = str.Replace("{Month}", Month);
                    str = str.Replace("{Amount}", Amount);
                    str = str.Replace("{Status}", Status);

                    return str;
                    break;

                case "feereminder":
                    using (StreamReader reader = new StreamReader(Path.Combine(_host.WebRootPath, "EmailTemplates/FeeReminder.html")))
                    {
                        str = reader.ReadToEnd();
                    }

                    str = str.Replace("{Name}", Name);
                    str = str.Replace("{Month}", Month);

                    return str;
                    break;

                case "assignment":
                    using (StreamReader reader = new StreamReader(Path.Combine(_host.WebRootPath, "EmailTemplates/Assignment.html")))
                    {
                        str = reader.ReadToEnd();
                    }

                    str = str.Replace("{Name}", Name);
                    str = str.Replace("{Assignment}", Assignment);

                    return str;
                    break;

                case "leaveapproved":
                    using (StreamReader reader = new StreamReader(Path.Combine(_host.WebRootPath, "EmailTemplates/LeaveApproved.html")))
                    {
                        str = reader.ReadToEnd();
                    }

                    str = str.Replace("{Name}", Name);
                   

                    return str;
                    break;


            }
            return str;

        }

        #endregion




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
