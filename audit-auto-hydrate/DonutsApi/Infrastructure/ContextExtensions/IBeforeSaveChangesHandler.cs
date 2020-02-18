using System.Threading.Tasks;

namespace DonutsApi.Infrastructure.ContextExtensions
{
    public interface IBeforeSaveChangesHandler
    {
        Task Handle(DonutContext context);
    }
}
