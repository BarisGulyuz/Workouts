using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Workouts.ApplicationModels
{
    public class Currency
    {
        public Currency()
        {
            Date = DateTime.Now;
        }
        public string Name { get; set; }
        public string BanknoteSelling { get; set; }
        public DateTime Date { get; set; }

    }

    public class CurrencyDto
    {
        public string Name { get; set; }
        public string BanknoteSelling { get; set; }

    }
}