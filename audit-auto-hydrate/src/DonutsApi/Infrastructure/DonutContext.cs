using System.Threading.Tasks;
using DonutsApi.Application;
using DonutsApi.Infrastructure.ContextExtensions;
using Microsoft.EntityFrameworkCore;

namespace DonutsApi.Infrastructure
{
    public class DonutContext : DbContext, IUnitOfWork
    {
        public DbSet<Donut> Donuts { get; set; }

        private readonly ISaveChangesProcessor _saveChangesProcessor;

        private DonutContext(DbContextOptions<DonutContext> options) : base(options)
        {
        }

        public DonutContext(DbContextOptions<DonutContext> options, ISaveChangesProcessor saveChangesProcessor) : base(options)
        {
            _saveChangesProcessor = saveChangesProcessor;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Donut>(entity =>
            {
                entity.ToTable("Donuts");
                entity.HasKey(x => x.Id);
                entity.Property(e => e.Id).UseIdentityColumn();
                
                const int totalArbitraryLength = 127;
                entity.Property(a => a.Flavor).HasMaxLength(totalArbitraryLength).IsRequired();
                entity.Property(b => b.Price).HasColumnType("decimal(10,2)").IsRequired();
                entity.Property(c => c.CreatedOn).IsRequired();
                entity.Property(c => c.LastModifiedOn).IsRequired();
            });
        }

        public async Task Complete()
        {
            await _saveChangesProcessor.RunAllBeforeHandlers(this);
            await SaveChangesAsync(true);
            await _saveChangesProcessor.RunAllAfterHandlers(this);
        }
    }
}
