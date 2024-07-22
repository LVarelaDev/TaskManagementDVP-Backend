namespace TaskManagement.DataAccess.Paginator
{
    public class Paginator<T>
    {
        public IQueryable<T> Query { get; private set; } = null!;
        public int TotalRecords { get; private set; }
        public int TotalPages { get; private set; }

        public static Paginator<T> GetPagedResult<T>(
            IQueryable<T> query,
            int pageNumber,
            int pageSize)
        {
            int totalRecords = query.Count();

            IQueryable<T> queryPage = query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize);

            return new Paginator<T>
            {
                Query = queryPage,
                TotalRecords = totalRecords,
                TotalPages = (totalRecords + pageSize - 1) / pageSize,
            };
        }
    }
}

