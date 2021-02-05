using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Utility
{
    public class EmailConfig
    {
        public static void SendMail(string to, string subject, string body)
        {


            try
            {
                MailMessage message = new MailMessage();
                SmtpClient smtp = new SmtpClient();
                message.From = new MailAddress("schoolproject010221@gmail.com");
                message.To.Add(to);
                message.Subject = subject;
                message.IsBodyHtml = true; //to make message body as html 
                message.Body = body;
                smtp.Port = 587;
                smtp.Host = "smtp.gmail.com"; //for gmail host  
                smtp.EnableSsl = true;
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = new NetworkCredential("schoolproject010221@gmail.com", "Vaibhav@18");
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtp.Send(message);
            }
            catch (Exception e)
            {


            }

        }


    }
}
