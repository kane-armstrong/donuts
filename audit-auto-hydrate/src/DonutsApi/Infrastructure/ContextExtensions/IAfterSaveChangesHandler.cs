using System.Threading.Tasks;

namespace DonutsApi.Infrastructure.ContextExtensions
{
    public interface IAfterSaveChangesHandler
    {
        Task Handle(DonutContext context);
    }
}
