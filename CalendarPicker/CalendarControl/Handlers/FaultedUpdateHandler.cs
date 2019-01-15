using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Telegram.Bot.Framework.Abstractions;

namespace CalendarPicker.CalendarControl.Handlers
{
    public class FaultedUpdateHandler : IUpdateHandler
    {
        private readonly ILogger<FaultedUpdateHandler> _logger;

        public FaultedUpdateHandler(ILogger<FaultedUpdateHandler> logger)
        {
            _logger = logger;
        }

        public async Task HandleAsync(IUpdateContext context, UpdateDelegate next, CancellationToken cancellationToken)
        {
            try
            {
                await next(context, cancellationToken).ConfigureAwait(false);
            }
            catch (Exception e)
            {
                _logger.LogError("Exception occured in handling update of type `{0}`: {1}", context.Update.Type, e.Message);
            }
        }
    }
}
