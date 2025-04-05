using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ambev.Shared.Common.Http
{
    public class PaginedList<T> : ApiResponseWithData<IReadOnlyCollection<T>>
    {
        public int TotalItems { get; }
        public int CurrentPage { get; }
        public int TotalPages { get; }

        public PaginedList(IReadOnlyCollection<T> items, int count, int currentPage, int pageSize)
        {
            Data = items;
            TotalItems = count;
            CurrentPage = currentPage;
            TotalPages = (int)Math.Ceiling(count / (double)pageSize);
        }

        public bool HasPreviousPage => CurrentPage > 1;

        public bool HasNextPage => CurrentPage < TotalPages;

        public static async Task<PaginedList<T>> CreateAsync(IQueryable<T> source, int pageNumber, int pageSize)
        {
            var count = await source.CountAsync();
            var items = await source.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();

            return new PaginedList<T>(items, count, pageNumber, pageSize);
        }
    }
}
