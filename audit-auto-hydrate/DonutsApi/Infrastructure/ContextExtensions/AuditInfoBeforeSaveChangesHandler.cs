using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace DonutsApi.Infrastructure.ContextExtensions
{
    public class AuditInfoBeforeSaveChangesHandler : IBeforeSaveChangesHandler
    {
        // On its own this is fairly useless (you can just use default constraints) but it becomes more useful
        // when also setting the id of the user who created/edited the entry
        public Task Handle(DonutContext context)
        {
            var addedEntities = context.ChangeTracker.Entries()
                .Where(ch => ch.State == EntityState.Added)
                .Select(ch => ch.Entity)
                .OfType<IAuditableEntity>()
                .ToList();

            foreach (var entity in addedEntities)
            {
                entity.CreatedOn = DateTimeOffset.UtcNow;
                entity.LastModifiedOn = DateTimeOffset.UtcNow;
            }

            var updatedEntities = context.ChangeTracker.Entries()
                .Where(ch => ch.State == EntityState.Modified)
                .Select(ch => ch.Entity)
                .OfType<IAuditableEntity>()
                .ToList();

            foreach (var entity in updatedEntities)
            {
                entity.LastModifiedOn = DateTimeOffset.UtcNow;
            }

            return Task.CompletedTask;
        }
    }
}
