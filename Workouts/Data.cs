using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Workouts.ApplicationModels;

namespace Workouts
{
    public class Data
    {

        public List<Currency> GetCurrenyData()
        {
            List<Currency> currencies = new List<Currency>
            {
                new Currency
                {
                    Name = "a",
                    BanknoteSelling = "10"
                },
                 new Currency
                {
                    Name = "b",
                    BanknoteSelling = "20"
                },
                     new Currency
                {
                    Name = "b",
                    BanknoteSelling = "20"
                },
                         new Currency
                {
                    Name = "b",
                    BanknoteSelling = "20"
                },
                             new Currency
                {
                    Name = "b",
                    BanknoteSelling = "20"
                },
                                 new Currency
                {
                    Name = "b",
                    BanknoteSelling = "20"
                },
                                     new Currency
                {
                    Name = "b",
                    BanknoteSelling = "20"
                },
                                         new Currency
                {
                    Name = "b",
                    BanknoteSelling = "20"
                },
                                             new Currency
                {
                    Name = "b",
                    BanknoteSelling = "20"
                },
                                                 new Currency
                {
                    Name = "b",
                    BanknoteSelling = "40"
                },

            };
            return currencies;
        }
    }
}
