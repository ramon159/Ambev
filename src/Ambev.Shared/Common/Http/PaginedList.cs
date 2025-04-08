namespace Ambev.Shared.Common.Http
{
    public class PaginedList<T>
    {
        public PaginedList(IReadOnlyCollection<T> items, int count, int currentPage, int pageSize)
        {
            Items=items;
            TotalItems=count;
            CurrentPage=currentPage;
            TotalPages = (int)Math.Ceiling(count / (double)pageSize); ;
        }

        public IReadOnlyCollection<T> Items { get; }
        public int TotalItems { get; }
        public int CurrentPage { get; }
        public int TotalPages { get; }
        public bool HasPreviousPage => CurrentPage > 1;
        public bool HasNextPage => CurrentPage < TotalPages;

    }
}
