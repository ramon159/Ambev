namespace Ambev.Shared.Common.Http
{
    public class PaginedList<T> : ApiResponseWithData<List<T>>
    {
        public int TotalItems { get; }
        public int CurrentPage { get; }
        public int TotalPages { get; }

        public PaginedList(List<T> items, int count, int currentPage, int pageSize)
        {
            Data = items;
            TotalItems = count;
            CurrentPage = currentPage;
            TotalPages = (int)Math.Ceiling(count / (double)pageSize);
        }

        public bool HasPreviousPage => CurrentPage > 1;

        public bool HasNextPage => CurrentPage < TotalPages;
    }
}
