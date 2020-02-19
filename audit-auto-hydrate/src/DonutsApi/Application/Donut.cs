using DonutsApi.Infrastructure.ContextExtensions;
using System;

namespace DonutsApi.Application
{
    public class Donut : IAuditableEntity
    {
        public Guid Id { get; set; }
        public string Flavor { get; set; }
        public decimal Price { get; set; }
        public DateTimeOffset CreatedOn { get; set; }
        public Guid CreatedBy { get; set; }
        public DateTimeOffset LastModifiedOn { get; set; }
        public Guid LastModifiedBy { get; set; }
    }
}