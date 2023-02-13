using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Workouts.ListToHtmlTable.ListToHtmlExtension;

namespace Workouts.ApplicationModels
{
    public class Currency
    {
        public Currency()
        {
            Date = DateTime.Now;
        }

        [HtmlTableColumnInfo(Order =1, ColumnName = "Kur ismi")]
        public string Name { get; set; }

        [HtmlTableColumnInfo(Order = 2, ColumnName = "Satış", MoneyPattern = "C")]
        public string BanknoteSelling { get; set; }

        [HtmlTableColumnInfo(Order = 2, ColumnName = "Tarih", DatePattern = "MM/dd/yyyy")]
        public DateTime Date { get; set; }

    }

    public class CurrencyDto
    {
        public string Name { get; set; }
        public string BanknoteSelling { get; set; }

    }
}