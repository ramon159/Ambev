using Microsoft.EntityFrameworkCore;

namespace Ambev.Infrastructure.Data
{
    public sealed class ReadDbContext : DbContext
    {
        public ReadDbContext(DbContextOptions options) : base(options)
        {
        }
    }
}
