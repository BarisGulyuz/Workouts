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
                    BanknoteSelling = "23"
                },
                             new Currency
                {
                    Name = "b",
                    BanknoteSelling = "21"
                },
                                 new Currency
                {
                    Name = "b",
                    BanknoteSelling = "36"
                },
                                     new Currency
                {
                    Name = "b",
                    BanknoteSelling = "20"
                },
                                         new Currency
                {
                    Name = "b",
                    BanknoteSelling = "22"
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
                                                    new Currency
                {
                    Name = "a",
                    BanknoteSelling = "10"
                },
                 new Currency
                {
                    Name = "b",
                    BanknoteSelling = "21"
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
                }

            };
            return currencies;
        }

        public List<User> GetUserData()
        {
            List<User> users = new List<User>();
            users.Add(new User { Name = "Barış", Mail = "bar.77@windowslive.com" });
            users.Add(new User { Name = "Deneme", Mail = "deneme@gmail.com" });
            return users;
        }

        public User GetDefaultUser()
        {
            return new User { Name = "Barış", Mail = "bar.77@windowslive.com" };
        }
    }
}
