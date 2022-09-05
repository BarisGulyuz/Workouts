using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Workouts.Mail
{
    public interface IMailSender
    {
        void SendMail(MailDto mailRequest);
    }
}
