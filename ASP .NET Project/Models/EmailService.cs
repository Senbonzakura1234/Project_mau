using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNet.Identity;

//pass: ihqvmkbgoakpczjk
namespace ASP.NET_Project.Models
{
    public class EmailService : IIdentityMessageService
    {
        public Task SendAsync(IdentityMessage message)
        {
            var mail = new MailMessage
            {
                From = new MailAddress("projectphpsellinggame@gmail.com"),
                Subject = message.Subject,
                Body = message.Body,
                IsBodyHtml = true
            };
            mail.To.Add(message.Destination);


            var smtpServer = new SmtpClient("smtp.gmail.com")
            {
                Credentials = new System.Net.NetworkCredential(
                    "projectphpsellinggame@gmail.com", 
                    "ihqvmkbgoakpczjk"),
                Port = 587,
                EnableSsl = true
            };

            smtpServer.Send(mail);
            return Task.FromResult(0);
        }
    }
}