using System.Threading.Tasks;

namespace DonutsApi.Infrastructure.ContextExtensions
{
    public interface ISaveChangesProcessor
    {
        Task RunAllBeforeHandlers(DonutContext context);
        Task RunAllAfterHandlers(DonutContext context);
    }
}
