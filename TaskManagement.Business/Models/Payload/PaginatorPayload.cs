namespace TaskManagement.Business.Models.Payload
{
    public record PaginatorPayload<T>
    {
        public int PageIndex { get; init; }
        public int PageSize { get; init; }
        public T? Filters { get; init; }
    }
}
