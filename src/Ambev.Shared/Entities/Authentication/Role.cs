using Ambev.Shared.Common.Entities;
using Microsoft.AspNetCore.Identity;

namespace Ambev.Shared.Entities.Authentication
{
    public class Role : IdentityRole<Guid>, IBaseEntity
    {
        public DateTimeOffset Created { get; set; }
        public string? CreatedBy { get; set; }
        public DateTimeOffset? Updated { get; set; }
        public string? UpdatedBy { get; set; }
        public DateTimeOffset? Deleted { get; set; }
        public string? DeletedBy { get; set; }
        public bool IsDeleted { get; set; }
    }
}
