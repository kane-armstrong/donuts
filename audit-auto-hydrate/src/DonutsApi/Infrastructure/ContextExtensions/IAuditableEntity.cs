using System;

namespace DonutsApi.Infrastructure.ContextExtensions
{
    public interface IAuditableEntity
    {
        DateTimeOffset CreatedOn { get; set; }
        DateTimeOffset LastModifiedOn { get; set; }
    }
}
