using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Workouts.ApplicationModels
{
    public class User
    {
        public int Id { get; set; } = new Random().Next(1, 100);
        public string Name { get; set; }
        public string Mail { get; set; }

       
        public User ShallowCopy()
        {
            return this.MemberwiseClone() as User;
        }

        public override bool Equals(object? obj)
        {
            if (obj == null || obj.GetType() != typeof(User))
            {
                return false;
            }
            if (object.ReferenceEquals(this, obj))
            {
                return true;
            }

            User user = (User)obj;

            if (user.Id != this.Id || user.Mail != this.Mail || user.Name != this.Name)
            {
                return false;
            }
            else
            {
                return true;
            }
        }



    }
}
