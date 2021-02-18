using Dapper;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SchoolManagement.DataAccess.Repository.IRepository;
using SchoolManagement.Models;
using SchoolManagement.Utility;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using SchoolManagement.Utility.Razorpay;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Authorization;
using SchoolManagement.Models.ViewModels;
using paytm;

namespace SchoolManagement.Areas.Student.Controllers
{
    [Area("Student")]
    [Authorize(Roles = SD.Student)]
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
        public IActionResult Result()
        {
            Subject subject = new Subject();
            return View(subject);
        }

        public IActionResult Holiday()
        {
            return View();
        }
        public IActionResult TimeTable()
        {
            return View();
        }
        public IActionResult TimeTableRoutine()
        {
            return View();
        }
        public IActionResult TimeTableRoutine1()
        {
            return View();
        }
        public IActionResult TimeTableRoutine2()
        {
            return View();
        }
        public IActionResult TimeTableRoutine3()
        {
            return View();
        }
        public IActionResult TimeTableRoutine4()
        {
            return View();
        }
        public IActionResult TimeTableRoutine5()
        {
            return View();
        }
        public IActionResult TimeTableRoutine6()
        {
            return View();
        }

        public IActionResult TimeTableRoutine7()
        {
            return View();
        }
        public IActionResult ResultTable()
        {
            var obj = _unitOfWork.SPCall.List<StudentUserDetails>(SD.GetStudentDetails, null);
            return View(obj);
        }
        public IActionResult ViewResult()
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
            ViewBag.FeeAmount = payment.amount;
            _unitOfWork.Save();

            return View(payment);

            //return View();

        }


        #region payment

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
                _unitOfWork.SPCall.List<FeeDetails>(SD.UpdateFeeDate, paymentParam);
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
        #endregion


        #region PaytmPayment

        public IActionResult PaytmPayment()
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
                payment.contactNumber = "7631167103";
                payment.address = "Ranchi";
                payment.amount = data.FeeCharge;
                payment.UserId = Convert.ToInt32(claimvalue);
                // payment.User = user;
            }



            return View(payment);
        }


        [HttpPost]
        public ContentResult PaytmPayment(string Order_Id, string User_Id, string Email, string Contact_No, string Amount)
        {
           
            String merchantKey = "M4bjjBIoF96_Jvzw";
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("MID", "nZOopB58096118599177");
            parameters.Add("CHANNEL_ID", "WEB");
            parameters.Add("INDUSTRY_TYPE_ID", "Retail");
            parameters.Add("WEBSITE", "WEBSTAGING");
            parameters.Add("EMAIL", Email);
            parameters.Add("MOBILE_NO", Contact_No);
            parameters.Add("CUST_ID", User_Id);
            parameters.Add("ORDER_ID", Order_Id);
            parameters.Add("TXN_AMOUNT", Amount);
            parameters.Add("CALLBACK_URL", "https://localhost:44305/Student/Student/PaytmPaymentCallBack"); //This parameter is not mandatory. Use this to pass the callback url dynamically.
            string checksum = CheckSum.generateCheckSum(merchantKey, parameters);
            string paytmURL = "https://securegw-stage.paytm.in/order/process?orderid=" + Order_Id;
            string outputHTML = "<html>";
            outputHTML += "<head>";
            outputHTML += "<title>Merchant Check Out Page</title>";
            outputHTML += "</head>";
            outputHTML += "<body>";
            outputHTML += "<center>Please do not refresh this page...</center>"; //you can put h1 tag here
            outputHTML += "<form method='post' action='" + paytmURL + "' name='f1'>";
            outputHTML += "<table border='1'>";
            outputHTML += "<tbody>";
            foreach (string key in parameters.Keys)
            {
                outputHTML += "<input type='hidden' name='" + key + "' value='" + parameters[key] + "'>";
            }
            outputHTML += "<input type='hidden' name='CHECKSUMHASH' value='" + checksum + "'>";
            outputHTML += "</tbody>";
            outputHTML += "</table>";
            outputHTML += "<script type='text/javascript'>";
            outputHTML += "document.f1.submit();";
            outputHTML += "</script>";
            outputHTML += "</form>";
            outputHTML += "</body>";
            outputHTML += "</html>";
            return base.Content(outputHTML, "text/html");
        }
        //[HttpPost]
        public IActionResult PaytmPaymentCallBack()
        {
            String merchantKey = "M4bjjBIoF96_Jvzw";
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            string paytmChecksum = "";
            foreach (string key in Request.Form.Keys)
            {
                string key_trim = Request.Form[key];
                parameters.Add(key.Trim(), key_trim.Trim());
                // parameters.Add(key.Trim(), Request.Form[key].Trim());
            }
            if (parameters.ContainsKey("CHECKSUMHASH"))
            {
                paytmChecksum = parameters["CHECKSUMHASH"];
                parameters.Remove("CHECKSUMHASH");
            }
            if (CheckSum.verifyCheckSum(merchantKey, parameters, paytmChecksum))
            {
                string paytmStatus = parameters["STATUS"];
                string txnId = parameters["TXNID"];
                string traxid = "Transaction Id : " + txnId;
                if (paytmStatus == "TXN_SUCCESS")
                {
                    string id = User.FindFirst("id").Value;
                   // string id = parameters["RESPCODE"];
                    var paymentParam = new DynamicParameters();
                    paymentParam.Add("inUserId", id);
                    var dataset = _unitOfWork.SPCall.List<FeeDetails>(SD.FeeDetails, paymentParam);

                    foreach (var data in dataset)
                    {

                        string emailbody = GetBody("feepayment", data.Name, " ", " ", " ", data.Month.ToString(), data.FeeCharge.ToString());
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
                    _unitOfWork.SPCall.List<FeeDetails>(SD.UpdateFeeDate, paymentParam);
                    return Content("Payment Successful!!- "+ traxid);
                }
                else if (paytmStatus == "PENDING")
                {
                    return Content("Payment Pending!!");
                }
                else if (paytmStatus == "TXN_FAILURE")
                {
                    return Content("Payment Failed!!");
                }
                return Content("Checksum Matched");
            }
            else
            {
                return Content("Checksum MisMatch");
            }
        }

        #endregion

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


                    break;

                case "notice":
                    using (StreamReader reader = new StreamReader(Path.Combine(_host.WebRootPath, "EmailTemplates/Notice.html")))
                    {
                        str = reader.ReadToEnd();
                    }

                    str = str.Replace("{Name}", Name);
                    str = str.Replace("{Notice}", Notice);


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

                    break;

                case "feereminder":
                    using (StreamReader reader = new StreamReader(Path.Combine(_host.WebRootPath, "EmailTemplates/FeeReminder.html")))
                    {
                        str = reader.ReadToEnd();
                    }

                    str = str.Replace("{Name}", Name);
                    str = str.Replace("{Month}", Month);

                    break;

                case "assignment":
                    using (StreamReader reader = new StreamReader(Path.Combine(_host.WebRootPath, "EmailTemplates/Assignment.html")))
                    {
                        str = reader.ReadToEnd();
                    }

                    str = str.Replace("{Name}", Name);
                    str = str.Replace("{Assignment}", Assignment);

                    break;


            }
            return str;

        }

        #endregion

        #region API CALLS
        [HttpGet]


        public IActionResult GetResult()
        {
            var obj = _unitOfWork.SPCall.List<Subject>(SD.GetResult, null);
            return Json(new { data = obj });
        }
        #endregion

    }
}

