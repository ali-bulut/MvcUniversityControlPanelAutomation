using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace MailSendLayer
{
    public class Mail
    {
        public void MailSender(string body, string toEmail, string title)
        {
            string fromEmail = "muhammetalibulut01@gmail.com";
            string fromPassword = "2001114ali";
            var fromAddress = new MailAddress(fromEmail);
            var toAddress = new MailAddress(toEmail);

            using (var smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(fromAddress.Address, fromPassword)
                
            })
            {
                using (var message = new MailMessage(fromAddress, toAddress) { Subject = title, Body = body })
                {
                    message.IsBodyHtml = true;
                    smtp.Send(message);
                }
            }
        }
    }
}
