using System;
using DonutsApi.Infrastructure.ContextExtensions;

namespace DonutsApi.Application
{
    public class Donut : IAuditableEntity
    {
        public int Id { get; set; }
        public string Flavor { get; set; }
        public decimal Price { get; set; }
        public DateTimeOffset CreatedOn { get; set; }
        public DateTimeOffset LastModifiedOn { get; set; }
    }
}