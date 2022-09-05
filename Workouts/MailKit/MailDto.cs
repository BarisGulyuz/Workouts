using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Workouts.Mail
{
    public class MailDto
    {
        public string From { get; set; } = MailConfiguration.EmailAdress;
        public string To { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }


    }
}
