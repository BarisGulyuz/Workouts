using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Workouts.ToPaginate
{
    public class PaginateModel<T>
    {
        public PaginateModel()
        {
            Value = new List<T>();
        }
        public int Size { get; set; }
        public int CurrentPage { get; set; }
        public int TotalCount { get; set; }
        public int TotalPage { get; set; }
        public List<T> Value { get; set; }

        public bool HasNextPage { get; set; }
        public bool IsFırstPage { get; set; }
    }
}
