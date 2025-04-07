using Ambev.Shared.Common.Http;
using Ambev.Shared.Helpers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
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
        public static async Task<PaginedList<T>> PagingAsync<T>(this IQueryable<T> query, int pageNumber, int pageSize, string sortTerm, CancellationToken cancellationToken = default)
        {
            var count = await query.CountAsync();

            var items = await query
                .Sorting(sortTerm)
                .Skip((pageNumber - 1) * pageSize).Take(pageSize)
                .ToListAsync();

            return new PaginedList<T>(items, count, pageNumber, pageSize);
        }

        private static IQueryable<T> Sorting<T>(this IQueryable<T> query, string sort)
        {
            var sortFields = QueryParametersHelper.SortingParser(sort);

            if (sortFields.Count > 0)
            {
                IOrderedQueryable<T> orderedQuery = query.OrderByNested(sortFields[0].Field, sortFields[0].Ascending);

                for (int i = 1; i < sortFields.Count; i++)
                {
                    orderedQuery = orderedQuery.ThenByNested(sortFields[i].Field, sortFields[i].Ascending);
                }
                return orderedQuery;
            }

            return query;
        }

        public static IOrderedQueryable<T> OrderByNested<T>(this IQueryable<T> query, string key, bool ascending = true)
        {

            var lambda = (dynamic)QueryParametersHelper.CreateSortExpression(typeof(T), key);

            return ascending
                ? Queryable.OrderBy(query, lambda)
                : Queryable.OrderByDescending(query, lambda);
        }
        public static IOrderedQueryable<T> ThenByNested<T>(this IOrderedQueryable<T> query, string key, bool ascending = true)
        {
            if (string.IsNullOrWhiteSpace(key))
            {
                return query;
            }

            var lambda = (dynamic)QueryParametersHelper.CreateSortExpression(typeof(T), key);

            return ascending
                ? Queryable.ThenBy(query, lambda)
                : Queryable.ThenByDescending(query, lambda);
        }
        //public static IQueryable<T> Filtering<T>(this IQueryable<T> query, Expression<Func<T, bool>>? filter)
        //{
        //    if (filter != null) query = query.Where(filter);
        //    return query;
        //}
        //public static IQueryable<T> Ordering<T>(this IQueryable<T> query, Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy)
        //{
        //    if (orderBy != null) query = orderBy(query);
        //    return query;
        //}
        //public static IQueryable<T> Including<T>(this IQueryable<T> query, Func<IQueryable<T>, IIncludableQueryable<T, object>>? include)
        //{
        //    if (include != null) query = include(query);
        //    return query;
        //}

        //public static IQueryable<T> Selecting<T>(this IQueryable<T> query, Expression<Func<T, T>>? selector = null)
        //{
        //    if (selector != null) query = query.Select(selector);
        //    return query;
        //}
    }
}
