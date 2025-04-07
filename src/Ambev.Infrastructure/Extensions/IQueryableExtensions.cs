using Ambev.Shared.Common.Http;
using Ambev.Shared.Helpers;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Ambev.Infrastructure.Extensions
{
    public static class IQueryableExtensions
    {
        public static IQueryable<T> Paging<T>(this IQueryable<T> query, int? page, int? pageSize)
        {
            if (page != null && pageSize != null)
                query = query.Skip(pageSize.Value * (page.Value - 1)).Take(pageSize.Value);
            return query;
        }
        public static async Task<PaginedList<T>> PagingAsync<T>(this IQueryable<T> query, int pageNumber, int pageSize, string sortTerm, Dictionary<string, string> filters = null, CancellationToken cancellationToken = default)
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

            // Parâmetro de expressão: x =>
            ParameterExpression parameter = Expression.Parameter(typeof(T), "x");
            Expression predicate = null;

            foreach (var kvp in filters)
            {
                string key = kvp.Key;
                string value = kvp.Value;

                Expression filterExpression = null;

                // Filtragem para intervalos numéricos ou datas (_min ou _max)
                if (key.StartsWith("_min") || key.StartsWith("_max"))
                {
                    bool isMin = key.StartsWith("_min");
                    // Extrai o nome da propriedade removendo o prefixo (_min ou _max)
                    string propertyName = key.Substring(4);

                    // Cria acesso à propriedade: x.PropertyName
                    Expression propertyExpression = Expression.PropertyOrField(parameter, propertyName);

                    // Tenta converter o valor para o tipo da propriedade
                    object constantValue;
                    try
                    {
                        constantValue = Convert.ChangeType(value, propertyExpression.Type);
                    }
                    catch (Exception)
                    {
                        // Caso a conversão falhe, ignora este filtro
                        continue;
                    }
                    Expression constant = Expression.Constant(constantValue, propertyExpression.Type);

                    // Cria expressão para comparação: >= para _min e <= para _max
                    filterExpression = isMin
                        ? Expression.GreaterThanOrEqual(propertyExpression, constant)
                        : Expression.LessThanOrEqual(propertyExpression, constant);
                }
                else
                {
                    // Filtro para igualdade ou para campos string com asteriscos
                    Expression propertyExpression = Expression.PropertyOrField(parameter, key);

                    if (propertyExpression.Type == typeof(string))
                    {
                        // Se o valor contiver asteriscos, utiliza startsWith, endsWith ou contains
                        if (value.StartsWith("*") || value.EndsWith("*"))
                        {
                            // Remove os asteriscos para obter o padrão
                            string pattern = value.Trim('*');

                            // Verifica qual operador aplicar
                            if (value.StartsWith("*") && value.EndsWith("*"))
                            {
                                // Contém: x.Field != null && x.Field.Contains(pattern)
                                Expression notNull = Expression.NotEqual(propertyExpression, Expression.Constant(null, typeof(string)));
                                var method = typeof(string).GetMethod("Contains", new[] { typeof(string) });
                                Expression containsCall = Expression.Call(propertyExpression, method, Expression.Constant(pattern));
                                filterExpression = Expression.AndAlso(notNull, containsCall);
                            }
                            else if (value.EndsWith("*"))
                            {
                                // Inicia com: x.Field != null && x.Field.StartsWith(pattern)
                                Expression notNull = Expression.NotEqual(propertyExpression, Expression.Constant(null, typeof(string)));
                                var method = typeof(string).GetMethod("StartsWith", new[] { typeof(string) });
                                Expression startsWithCall = Expression.Call(propertyExpression, method, Expression.Constant(pattern));
                                filterExpression = Expression.AndAlso(notNull, startsWithCall);
                            }
                            else if (value.StartsWith("*"))
                            {
                                // Termina com: x.Field != null && x.Field.EndsWith(pattern)
                                Expression notNull = Expression.NotEqual(propertyExpression, Expression.Constant(null, typeof(string)));
                                var method = typeof(string).GetMethod("EndsWith", new[] { typeof(string) });
                                Expression endsWithCall = Expression.Call(propertyExpression, method, Expression.Constant(pattern));
                                filterExpression = Expression.AndAlso(notNull, endsWithCall);
                            }
                        }
                        else
                        {
                            // Filtro de igualdade para string sem asteriscos
                            filterExpression = Expression.Equal(propertyExpression, Expression.Constant(value));
                        }
                    }
                    else
                    {
                        // Para campos não-string, aplica igualdade, convertendo o valor conforme o tipo da propriedade
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

                // Combina o filtro atual com os anteriores usando AND
                if (filterExpression != null)
                {
                    predicate = predicate == null
                        ? filterExpression
                        : Expression.AndAlso(predicate, filterExpression);
                }
            }

            if (predicate == null)
                return query;

            // Cria a lambda (x => [predicate]) e aplica o Where
            var lambda = Expression.Lambda<Func<T, bool>>(predicate, parameter);
            return query.Where(lambda);
        }

    }
}
