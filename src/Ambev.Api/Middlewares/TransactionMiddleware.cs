
using Ambev.Infrastructure.Data;
using Ardalis.GuardClauses;
using Microsoft.EntityFrameworkCore.Storage;

namespace Ambev.Api.Middlewares
{
    public class TransactionMiddleware : IMiddleware
    {
        private readonly WriteDbContext _dbContext;
        private readonly ILogger<TransactionMiddleware> _logger;
        private IDbContextTransaction? _transaction;

        public TransactionMiddleware(WriteDbContext dbContext, ILogger<TransactionMiddleware> logger)
        {
            _dbContext = dbContext;
            _logger=logger;
        }

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
