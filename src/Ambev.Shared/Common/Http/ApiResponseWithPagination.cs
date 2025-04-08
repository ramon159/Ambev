namespace Ambev.Shared.Common.Http
{
    public class ApiResponseWithPagination<T> : ApiResponseWithData<T>
    {
        public int TotalItems { get; set; }
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public bool HasPreviousPage { get; set; }
        public bool HasNextPage { get; set; }
    }
}
