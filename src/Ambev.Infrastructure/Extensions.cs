using Ambev.Shared.Common.Http;
using Microsoft.EntityFrameworkCore.Query;
using MongoDB.Driver.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Ambev.Infrastructure
{
    public static class IQueryableExtensions
    {
        public static IQueryable<T> Paging<T>(this IQueryable<T> query, int? page, int? pageSize)
        {
            if (page != null && pageSize != null)
                query = query.Skip(pageSize.Value * (page.Value - 1)).Take(pageSize.Value);
            return query;
        }
        public static async Task<PaginedList<T>> PagingAsync<T>(this IQueryable<T> query, int pageNumber, int pageSize, CancellationToken cancellationToken = default)
        {
            var count = await query.CountAsync(cancellationToken);
            var items = await query.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync(cancellationToken);

            return new PaginedList<T>(items, count, pageNumber, pageSize);
        }
        public static IQueryable<T> Filtering<T>(this IQueryable<T> query, Expression<Func<T, bool>>? filter)
        {
            if (filter != null) query = query.Where(filter);
            return query;
        }
        public static IQueryable<T> Ordering<T>(this IQueryable<T> query, Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy)
        {
            if (orderBy != null) query = orderBy(query);
            return query;
        }
        public static IQueryable<T> Including<T>(this IQueryable<T> query, Func<IQueryable<T>, IIncludableQueryable<T, object>>? include)
        {
            if (include != null) query = include(query);
            return query;
        }

        public static IQueryable<T> Selecting<T>(this IQueryable<T> query, Expression<Func<T, T>>? selector = null)
        {
            if (selector != null) query = query.Select(selector);
            return query;
        }
    }
}
