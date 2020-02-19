using DonutsApi.Application;
using DonutsApi.Infrastructure.ContextExtensions;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

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
                entity.Property(e => e.Id).ValueGeneratedNever();

                const int totalArbitraryLength = 127;
                entity.Property(a => a.Flavor).HasMaxLength(totalArbitraryLength).IsRequired();
                entity.Property(b => b.Price).HasColumnType("decimal(10,2)").IsRequired();
                entity.Property(c => c.CreatedOn).IsRequired();
                entity.Property(c => c.LastModifiedOn).IsRequired();
            });
            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("Users");
                entity.HasKey(x => x.Id);
                entity.Property(e => e.Id).ValueGeneratedNever();
                entity.Property(a => a.Name).HasMaxLength(255).IsRequired();
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
