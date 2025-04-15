using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Ambev.Infrastructure.Extensions
{
    public static class DbContextExtensions
    {

        public static async Task SynchronizeAsync<T>(
            this DbContext context,
            IEnumerable<T> sourceEntities,
            Expression<Func<T, bool>> filterPredicate,
            Func<T, object> matchKeySelector,
            Action<T, T> updateAction,
            CancellationToken cancellationToken = default
            ) where T : class
        {
            var dbSet = context.Set<T>();

            var existingEntities = await dbSet
                .Where(filterPredicate)
                .ToListAsync(cancellationToken);

            var existingDict = existingEntities.ToDictionary(matchKeySelector);
            var incomingDict = sourceEntities.ToDictionary(matchKeySelector);

            var toUpdate = sourceEntities
                .Where(e => existingDict.ContainsKey(matchKeySelector(e)))
                .ToList();

            var toAdd = sourceEntities
                .Where(e => !existingDict.ContainsKey(matchKeySelector(e)))
                .ToList();

            var toRemove = existingEntities
                .Where(e => !incomingDict.ContainsKey(matchKeySelector(e)))
                .ToList();

            foreach (var item in toUpdate)
            {
                var existing = existingDict[matchKeySelector(item)];
                updateAction(existing, item);
            }

            dbSet.RemoveRange(toRemove);

            await dbSet.AddRangeAsync(toAdd, cancellationToken);
        }

    }

}
