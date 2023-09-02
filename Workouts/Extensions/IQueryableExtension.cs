using System.Linq.Expressions;

namespace Workouts.Extensions
{
    public static class IQueryableExtension
    {
        public static PaginateModel<T> Paginate<T>(this IQueryable<T> values,
                                                            int size, int page)
        {
            int totalCount = values.Count();
            int totalPages = Convert.ToInt32(Math.Ceiling(totalCount / (double)size));

            List<T> returnItems = values
                                .Skip(size * (page - 1))
                                .Take(size)
                                .ToList();

            PaginateModel<T> paginateModel = new PaginateModel<T>()
            {
                TotalCount = totalCount,
                CurrentPage = page,
                Size = size,
                TotalPage = totalPages,
                HasNextPage = !(page == totalPages),
                IsFırstPage = page == 1,
                Value = returnItems
            };

            return paginateModel;

        }

        /// <summary>
        /// Verilen Condition'a göre IQueryable objeye expression'ı ekler
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="query"></param>
        /// <param name="predicate"></param>
        /// <param name="condition"></param>
        /// <returns></returns>
        public static IQueryable<T> WhereIf<T>(this IQueryable<T> query, Expression<Func<T, bool>> predicate, bool condition)
        {
            if (condition)
            {
                query = query.Where(predicate);
            }
            return query;
        }
    }

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
