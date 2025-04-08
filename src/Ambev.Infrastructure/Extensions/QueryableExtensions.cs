using Ambev.Shared.Common.Http;
using Ambev.Shared.Helpers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Globalization;
using System.Linq.Expressions;

namespace Ambev.Infrastructure.Extensions
{
    public static class QueryableExtensions
    {
        public static IQueryable<T> Paging<T>(this IQueryable<T> query, int? page, int? pageSize)
        {
            if (page != null && pageSize != null)
                query = query.Skip(pageSize.Value * (page.Value - 1)).Take(pageSize.Value);
            return query;
        }
        public static async Task<PaginedList<T>> PagingAsync<T>(this IQueryable<T> query, int pageNumber, int pageSize, string sortTerm, Dictionary<string, string> filters, CancellationToken cancellationToken = default)
        {
            query = query
                .Filtering(filters)
                .Sorting(sortTerm);

            var count = await query.CountAsync(cancellationToken);

            var items = await query
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

            var lambda = (dynamic)QueryParametersHelper.CreateExpression(typeof(T), key);

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

            var lambda = (dynamic)QueryParametersHelper.CreateExpression(typeof(T), key);

            return ascending
                ? Queryable.ThenBy(query, lambda)
                : Queryable.ThenByDescending(query, lambda);
        }
        /// <summary>
        /// Aplica filtros dinâmicos à query com base em um dicionário de parâmetros.
        /// 
        /// Regras:
        /// - Para filtros simples (ex.: field=value), aplica igualdade.
        /// - Para campos string, se o valor iniciar ou terminar com asterisco (*), usa starts/ends/contains.
        /// - Para campos numéricos ou datas, utiliza os prefixos _min e _max para filtrar intervalos.
        /// - Múltiplos filtros são combinados com AND.
        /// </summary>
        /// <typeparam name="T">Tipo do objeto da query</typeparam>
        /// <param name="query">Query original</param>
        /// <param name="filters">Dicionário com os filtros (chave = nome do campo, valor = critério)</param>
        /// <returns>Query com os filtros aplicados</returns>
        public static IQueryable<T> Filtering<T>(this IQueryable<T> query, IDictionary<string, string> filters)
        {
            if (filters == null || !filters.Any())
                return query;

            // <T> x =>
            ParameterExpression parameter = Expression.Parameter(typeof(T), "x");
            Expression predicate = null;
            foreach (var kvp in filters)
            {
                string key = kvp.Key.Trim('_');
                string value = kvp.Value;

                Expression filterExpression = null;
                Expression propertyExpression = null;

                if (key.StartsWith("min") || key.StartsWith("max"))
                {
                    bool isMin = key.StartsWith("min");
                    string propertyName = key.Substring(3);


                    try
                    {
                        propertyExpression = Expression.PropertyOrField(parameter, propertyName);
                    }
                    catch (Exception)
                    {
                        // Case is not property, ignore
                        continue;
                    }

                    object constantValue;
                    try
                    {
                        switch (propertyExpression.Type)
                        {
                            case Type t when t == typeof(string):
                                constantValue = value;
                                break;

                            case Type t when t == typeof(DateTimeOffset):
                                constantValue = new DateTimeOffset(DateTime.Parse(value), TimeSpan.Zero);
                                break;

                            case Type t when t == typeof(DateTime):
                                constantValue = DateTime.Parse(value);
                                break;
                            default:
                                constantValue = Convert.ChangeType(value, propertyExpression.Type);
                                break;
                        }
                    }
                    catch (Exception)
                    {
                        // Case conversion type failed, ignore the filter
                        continue;
                    }
                    Expression constant = Expression.Constant(constantValue, propertyExpression.Type);

                    filterExpression = isMin
                        ? Expression.GreaterThanOrEqual(propertyExpression, constant)
                        : Expression.LessThanOrEqual(propertyExpression, constant);
                }
                else
                {

                    try
                    {
                        propertyExpression = Expression.PropertyOrField(parameter, key);
                    }
                    catch (Exception ex)
                    {

                        throw new ArgumentException($"{key} is not a property of {typeof(T).Name}");
                    }

                    if (propertyExpression.Type == typeof(string))
                    {
                        if (value.StartsWith("*") || value.EndsWith("*"))
                        {
                            string pattern = value.Trim('*');

                            if (value.StartsWith("*") && value.EndsWith("*"))
                            {
                                Expression notNull = Expression.NotEqual(propertyExpression, Expression.Constant(null, typeof(string)));
                                var method = typeof(string).GetMethod("Contains", new[] { typeof(string) });
                                Expression containsCall = Expression.Call(propertyExpression, method, Expression.Constant(pattern));
                                filterExpression = Expression.AndAlso(notNull, containsCall);
                            }
                            else if (value.EndsWith("*"))
                            {
                                Expression notNull = Expression.NotEqual(propertyExpression, Expression.Constant(null, typeof(string)));
                                var method = typeof(string).GetMethod("StartsWith", new[] { typeof(string) });
                                Expression startsWithCall = Expression.Call(propertyExpression, method, Expression.Constant(pattern));
                                filterExpression = Expression.AndAlso(notNull, startsWithCall);
                            }
                            else if (value.StartsWith("*"))
                            {
                                Expression notNull = Expression.NotEqual(propertyExpression, Expression.Constant(null, typeof(string)));
                                var method = typeof(string).GetMethod("EndsWith", new[] { typeof(string) });
                                Expression endsWithCall = Expression.Call(propertyExpression, method, Expression.Constant(pattern));
                                filterExpression = Expression.AndAlso(notNull, endsWithCall);
                            }
                        }
                        else
                        {
                            filterExpression = Expression.Equal(propertyExpression, Expression.Constant(value));
                        }
                    }
                    else
                    {
                        object constantValue;
                        try
                        {
                            constantValue = Convert.ChangeType(value, propertyExpression.Type);
                        }
                        catch (Exception)
                        {
                            continue;
                        }
                        filterExpression = Expression.Equal(propertyExpression, Expression.Constant(constantValue, propertyExpression.Type));
                    }
                }

                if (filterExpression != null)
                {
                    predicate = predicate == null
                        ? filterExpression
                        : Expression.AndAlso(predicate, filterExpression);
                }
            }

            if (predicate == null)
                return query;

            var lambda = Expression.Lambda<Func<T, bool>>(predicate, parameter);
            return query.Where(lambda);
        }

    }
}
