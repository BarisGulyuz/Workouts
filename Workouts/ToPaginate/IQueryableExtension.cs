using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Workouts.ToPaginate
{
    public static class IQueryableExtension
    {
        public static PaginateModel<T> Paginate<T>(this IQueryable<T> values,
                                                            int size, int page)
        {
            int totalCount = values.Count();

            List<T> returnItems = values
                                .Skip(size * (page - 1))
                                .Take(size)
                                .ToList();

            int totalPages = Convert.ToInt32(Math.Ceiling(totalCount / (double)size));

            PaginateModel<T> paginateModel = new PaginateModel<T>()
            {
                TotalCount = totalCount,
                CurrentPage = page,
                Size = size,
                TotalPage = totalPages,
                HasNextPage = !(page == totalPages),
                IsFırstPage = (page == 1),
                Value = returnItems
            };

            return paginateModel;

        }
    }
}
