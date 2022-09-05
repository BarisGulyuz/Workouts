using MailKit.Net.Smtp;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Workouts.Mail
{
    public class MailSender : IMailSender
    {
        public void SendMail(MailDto mailRequest)
        {
            MimeMessage emailMessage = new MimeMessage();

            emailMessage.From.Add(MailboxAddress.Parse(mailRequest.From));
            emailMessage.To.Add(MailboxAddress.Parse(mailRequest.To));
            emailMessage.Subject = mailRequest.Subject;
            emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html) { Text = mailRequest.Body };

            using (SmtpClient smtp = new SmtpClient())
            {
                smtp.Connect(MailConfiguration.Host, MailConfiguration.Port, MailKit.Security.SecureSocketOptions.StartTls);
                smtp.Authenticate(MailConfiguration.EmailAdress, MailConfiguration.Password);
                smtp.Send(emailMessage);
                smtp.Disconnect(true);
            }
        }
    }
}
