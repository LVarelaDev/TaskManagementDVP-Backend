
namespace TaskManagement.Business.Models.Responses
{
    public class PaginatorResponse<T>
    {
        public int CurrentPage { get; set; }
        public int TotalRecords { get; set; }
        public int TotalPages { get; set; }
        public int PageSize { get; set; }
        public List<T> DataList { get; set; } = new List<T>();
    }
}
