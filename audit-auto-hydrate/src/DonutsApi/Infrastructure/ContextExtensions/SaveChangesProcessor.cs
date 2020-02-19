using System.Collections.Generic;
using System.Threading.Tasks;

namespace DonutsApi.Infrastructure.ContextExtensions
{
    public class SaveChangesProcessor : ISaveChangesProcessor
    {
        private readonly IEnumerable<IBeforeSaveChangesHandler> _beforeHandlers;
        private readonly IEnumerable<IAfterSaveChangesHandler> _afterHandlers;

        public SaveChangesProcessor(IEnumerable<IBeforeSaveChangesHandler> beforeHandlers, IEnumerable<IAfterSaveChangesHandler> afterHandlers)
        {
            _beforeHandlers = beforeHandlers;
            _afterHandlers = afterHandlers;
        }

        public async Task RunAllBeforeHandlers(DonutContext context)
        {
            foreach (var handler in _beforeHandlers)
            {
                await handler.Handle(context);
            }
        }

        public async Task RunAllAfterHandlers(DonutContext context)
        {
            foreach (var handler in _afterHandlers)
            {
                await handler.Handle(context);
            }
        }
    }
}
