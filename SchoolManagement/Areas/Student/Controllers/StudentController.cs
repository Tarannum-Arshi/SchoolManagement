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
using SchoolManagement.Utility.Razorpay;
using Microsoft.Extensions.Options;

namespace SchoolManagement.Areas.Student.Controllers
{
    [Area("Student")]
    public class StudentController : Controller
    {
        public readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _host;
        private readonly Keys _keys;

        public StudentController(IUnitOfWork unitOfWork, IWebHostEnvironment host, IOptions<Keys> options)
        {
            _unitOfWork = unitOfWork;
            _host = host;
            _keys = options.Value;
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
            //var fee = _unitOfWork.SPCall.List<Fee>(SD.GetFee ,parameters);

            //var fee = new DynamicParameters();
            var dataset = _unitOfWork.SPCall.List<FeeDetails>(SD.FeeDetails, parameters);
            Payments payment = new Payments();
            foreach(var data in dataset)
            {
                payment.name = data.Name;
                payment.email = data.Email;
                payment.contactNumber = "9931159589";
                payment.address = "Ranchi";
                payment.amount = data.FeeCharge;
                payment.UserId = Convert.ToInt32(claimvalue);
               // payment.User = user;
            }
            //payments.amount=fee.


            //var identity = new ClaimsIdentity(new[] {
            //new Claim("fee", Fee.FeeCharge.ToString()) 
            //}, CookieAuthenticationDefaults.AuthenticationScheme);

            //var principal = new ClaimsPrincipal(identity);
            //var login = HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);


            //Hardcoded values
            /*string name = "Vaibhav";
            string email = "vbhvsngh07@gmail.com";
            string month = "3";
            string fee_total = "1500";*/

            return View( payment);
            
        }



        public IActionResult RazorpayPayment()
        {
            string claimvalue = User.FindFirst("id").Value;
            var parameters = new DynamicParameters();
            parameters.Add("inUserId", claimvalue);
            //var fee = _unitOfWork.SPCall.List<Fee>(SD.GetFee ,parameters);

            //var fee = new DynamicParameters();
            var dataset = _unitOfWork.SPCall.List<FeeDetails>(SD.FeeDetails, parameters);
            Payments payment = new Payments();
            foreach (var data in dataset)
            {
                payment.name = data.Name;
                payment.email = data.Email;
                payment.contactNumber = "9931159589";
                payment.address = "Ranchi";
                payment.amount = data.FeeCharge;
                payment.UserId = Convert.ToInt32(claimvalue);
                // payment.User = user;
            }
            //payments.amount=fee.


            //var identity = new ClaimsIdentity(new[] {
            //new Claim("fee", Fee.FeeCharge.ToString()) 
            //}, CookieAuthenticationDefaults.AuthenticationScheme);

            //var principal = new ClaimsPrincipal(identity);
            //var login = HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);


            //Hardcoded values
            /*string name = "Vaibhav";
            string email = "vbhvsngh07@gmail.com";
            string month = "3";

            string fee_total = "1500";*/
            var paymentParam = new DynamicParameters();

            paymentParam.Add("stName", payment.name);
            paymentParam.Add("stEmail", payment.email);
            paymentParam.Add("stContactNumber", payment.contactNumber);
            paymentParam.Add("stAddress", payment.address);
            paymentParam.Add("inAmount", payment.amount);
            paymentParam.Add("inUserId", payment.UserId);
            _unitOfWork.SPCall.List<StudentModel>(SD.SaveFeeDetails, paymentParam);

            _unitOfWork.Save();

            return View(payment);

            //return View();

        }
        



        [HttpPost]
        public ActionResult CreateOrder(Payments _requestData)
        {
            // Generate random receipt number for order
            Random randomObj = new Random();
            string transactionId = randomObj.Next(10000000, 100000000).ToString();

            Razorpay.Api.RazorpayClient client = new Razorpay.Api.RazorpayClient(_keys.SecretKey, _keys.PublishKey);
            Dictionary<string, object> options = new Dictionary<string, object>();
            options.Add("amount", _requestData.amount * 100);  // Amount will in paise
            options.Add("receipt", transactionId);
            options.Add("currency", "INR");
            options.Add("payment_capture", "1"); // 1 - automatic  , 0 - manual
                                                 //options.Add("notes", "-- You can put any notes here --");
            Razorpay.Api.Order orderResponse = client.Order.Create(options);
            //string orderId = orderResponse["id"].ToString();

            // Create order model for return on view
            OrderModel orderModel = new OrderModel
            {
                //orderId = orderResponse.Attributes["id"],
                razorpayKey = _keys.SecretKey,
                amount = _requestData.amount * 100,
                currency = "INR",
                name = _requestData.name,
                email = _requestData.email,
                contactNumber = _requestData.contactNumber,
                address = _requestData.address,
                description = "Testing description"
            };

            // Return on PaymentPage with Order data
            return View("PaymentPage", orderModel);
        }

        public class OrderModel
        {
            //public string orderId { get; set; }
            public string razorpayKey { get; set; }
            public int amount { get; set; }
            public string currency { get; set; }
            public string name { get; set; }
            public string email { get; set; }
            public string contactNumber { get; set; }
            public string address { get; set; }
            public string description { get; set; }
        }


        [HttpPost]
        public ActionResult Complete()
        {
            // Payment data comes in url so we have to get it from url

            // This id is razorpay unique payment id which can be use to get the payment details from razorpay server

            string paymentId = Request.Form["rzp_paymentid"];
            //string paymentId = paymentid;
            // This is orderId
            string orderId = Request.Form["rzp_orderid"];


            Razorpay.Api.RazorpayClient client = new Razorpay.Api.RazorpayClient(_keys.SecretKey, _keys.PublishKey);

            Razorpay.Api.Payment payment = client.Payment.Fetch(paymentId);

            // This code is for capture the payment 
            Dictionary<string, object> options = new Dictionary<string, object>();
            options.Add("amount", payment.Attributes["amount"]);
            Razorpay.Api.Payment paymentCaptured = payment.Capture(options);
            string amt = paymentCaptured.Attributes["amount"];

            //// Check payment made successfully

            if (paymentCaptured.Attributes["status"] == "captured")
            {
                string id =User.FindFirst("id").Value;
                var paymentParam = new DynamicParameters();
                paymentParam.Add("inUserId", id);
                var dataset = _unitOfWork.SPCall.List<FeeDetails>(SD.FeeDetails, paymentParam);

                foreach (var data in dataset)
                {

                    string emailbody = GetBody("feepayment", data.Name, " " ," ", " ",data.Month.ToString(),data.FeeCharge.ToString());
                    EmailConfig.SendMail(data.Email, "Welcome", emailbody);
                    /*payment.name = data.Name;
                    payment.email = data.Email;
                    payment.contactNumber = "9931159589";
                    payment.address = "Ranchi";
                    payment.amount = data.FeeCharge;
                    payment.UserId = Convert.ToInt32(claimvalue);*/
                    // payment.User = user;
                }
                // Create these action method
                // string emailbody = GetBody("feepayment", FirstName, Email, Password);
                //EmailConfig.SendMail(Email, "Welcome", emailbody);
                return RedirectToAction("Success");
            }
            else
            {
                return RedirectToAction("Failed");
            }
        }

        public ActionResult Success()
        {
            return View();
        }

        public ActionResult Failed()
        {
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


            }
            return str;

        }

        #endregion


    }
}

