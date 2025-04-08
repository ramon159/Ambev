using Ambev.Shared.Models;
using System.Linq.Expressions;

namespace Ambev.Shared.Helpers
{
    public static class QueryParametersHelper
    {

        /// <summary>
        /// Parses a sort query string into a list of <see cref="SortField"/> objects.
        /// </summary>
        /// <param name="sortFields">
        /// A string of fields to sort by, optionally followed by "asc" or "desc". 
        /// For example: "price desc, title asc"
        /// </param>
        /// <returns>
        /// A list of <see cref="SortField"/> objects where each field is marked as ascending or descending. 
        /// If no direction is provided, ascending is assumed by default.
        /// </returns>
        public static List<SortField> SortingParser(string sortFields)
        {
            return sortFields.ToLower().Trim('"').Split(',', StringSplitOptions.RemoveEmptyEntries)
                        .Select(o => o.Trim().Split(' ', StringSplitOptions.RemoveEmptyEntries))
                        .Where(parts => parts.Length >= 1)
                        .Select(parts => new SortField
                        {
                            Field = parts[0],
                            Ascending = parts.Length > 1 ?
                                parts[1].Equals("asc", StringComparison.OrdinalIgnoreCase) : true
                        })
                        .ToList();
        }
        public static LambdaExpression CreateExpression(Type type, string propertyName)
        {
            var param = Expression.Parameter(type, "x");

            Expression body = param;
            foreach (var member in propertyName.Split('.'))
            {
                try
                {
                    body = Expression.PropertyOrField(body, member);
                }
                catch (Exception)
                {
                    continue;
                }
            }

            return Expression.Lambda(body, param);
        }
    }

}
