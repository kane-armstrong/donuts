using System;
using System.Linq;
using System.Threading.Tasks;
using DonutsApi.Services;
using Microsoft.EntityFrameworkCore;

namespace DonutsApi.Infrastructure.ContextExtensions
{
    public class AuditInfoBeforeSaveChangesHandler : IBeforeSaveChangesHandler
    {
        private readonly ICurrentUserProfile _currentUser;

        public AuditInfoBeforeSaveChangesHandler(ICurrentUserProfile currentUser)
        {
            _currentUser = currentUser;
        }

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
                entity.CreatedBy = _currentUser.UserId;
                entity.LastModifiedOn = DateTimeOffset.UtcNow;
                entity.LastModifiedBy = _currentUser.UserId;
            }

            var updatedEntities = context.ChangeTracker.Entries()
                .Where(ch => ch.State == EntityState.Modified)
                .Select(ch => ch.Entity)
                .OfType<IAuditableEntity>()
                .ToList();

            foreach (var entity in updatedEntities)
            {
                entity.LastModifiedOn = DateTimeOffset.UtcNow;
                entity.LastModifiedBy = _currentUser.UserId;
            }

            return Task.CompletedTask;
        }
    }
}
