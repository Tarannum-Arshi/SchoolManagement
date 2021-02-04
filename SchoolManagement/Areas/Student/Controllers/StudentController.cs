using Dapper;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SchoolManagement.DataAccess.Repository.IRepository;
using SchoolManagement.Models.ViewModels;
using SchoolManagement.Utility;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace SchoolManagement.Areas.Student.Controllers
{
    [Area("Student")]
    public class StudentController : Controller
    {
        public readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _host;

        public StudentController(IUnitOfWork unitOfWork, IWebHostEnvironment host)
        {
            _unitOfWork = unitOfWork;
            _host = host;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Fee()
        {
            string claimvalue = User.FindFirst("id").Value;
            var parameters = new DynamicParameters();
            parameters.Add("inUserId", claimvalue);
            var fee = _unitOfWork.SPCall.List<Fee>(SD.GetFee ,parameters);

            //var identity = new ClaimsIdentity(new[] {
            //new Claim("fee", Fee.FeeCharge.ToString()) 
            //}, CookieAuthenticationDefaults.AuthenticationScheme);

            //var principal = new ClaimsPrincipal(identity);
            //var login = HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);


            //Hardcoded values
            string name = "Vaibhav";
            string email = "vbhvsngh07@gmail.com";
            string month = "3";
            string fee_total = "1500";

            return View(fee);
            
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


            }
            return str;

        }

        #endregion


    }
}

