using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Tebnabawe.Application.Authentication
{
    public class EmailService : IEmailService
    {
        public EmailService()
        {
        }

        public void SendEmail(string toEmail, string subject, string content)
        {
            var senderEmail = new MailAddress("atebnabawe@atebnabawe.com", "ATebnabawe Website");
            var receiverEmail = new MailAddress(toEmail, "Receiver");
            var password = "M8okaX!mSd2X#BRVi";

            var smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(senderEmail.Address, password)
            };
            using (var mess = new MailMessage(senderEmail, receiverEmail)
            {
                Subject = subject,
                Body = content
            })
            {
                mess.IsBodyHtml = true;
                smtp.Send(mess);
            }
        }
    }
}
