using System;

namespace DonutsApi.Infrastructure.ContextExtensions
{
    public interface IAuditableEntity
    {
        DateTimeOffset CreatedOn { get; set; }
        Guid CreatedBy { get; set; }
        DateTimeOffset LastModifiedOn { get; set; }
        Guid LastModifiedBy { get; set; }
    }
}
