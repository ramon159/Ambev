
using Ambev.Infrastructure.Data;
using Ardalis.GuardClauses;
using Microsoft.EntityFrameworkCore.Storage;

namespace Ambev.Api.Middlewares
{
    /// <summary>
    /// middleware that manages transactions
    /// </summary>
    public class TransactionMiddleware : IMiddleware
    {
        private readonly WriteDbContext _dbContext;
        private readonly ILogger<TransactionMiddleware> _logger;
        private IDbContextTransaction? _transaction;

        /// <summary>
        /// TransactionMiddleware constructor
        /// </summary>
        /// <param name="dbContext"></param>
        /// <param name="logger"></param>
        public TransactionMiddleware(WriteDbContext dbContext, ILogger<TransactionMiddleware> logger)
        {
            _dbContext = dbContext;
            _logger=logger;
        }

        /// <summary>
        /// method that manages transactions
        /// </summary>
        /// <param name="context"></param>
        /// <param name="next"></param>
        /// <returns></returns>
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                _transaction = await _dbContext.Database.BeginTransactionAsync();
                _logger.LogInformation("Transaction id: [{transactionId}]", _transaction.TransactionId);
                await next(context);
                Guard.Against.Null(_transaction);
                await _transaction.CommitAsync();
            }
            catch (Exception ex)
            {
                if (_transaction != null)
                {
                    await _transaction.RollbackAsync();
                    _logger.LogInformation("Rollback: {ex}", ex.Message);
                }
                throw;
            }
        }
    }
}
