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
    }
}
