namespace Ambev.Shared.Common.Http
{
    public class PaginedList<T>
    {
        public PaginedList(IReadOnlyCollection<T> items, int totalItems, int currentPage, int pageSize)
        {
            Items=items;
            TotalItems=totalItems;
            CurrentPage=currentPage;
            TotalPages = (int)Math.Ceiling(totalItems / (double)pageSize); ;
        }

        public IReadOnlyCollection<T> Items { get; set; }
        public int TotalItems { get; set; }
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public bool HasPreviousPage => CurrentPage > 1;
        public bool HasNextPage => CurrentPage < TotalPages;

    }
}
