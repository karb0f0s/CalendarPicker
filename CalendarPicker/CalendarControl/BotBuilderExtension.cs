using CalendarPicker.CalendarControl.Handlers;
using Telegram.Bot.Framework;
using static Microsoft.Extensions.DependencyInjection.TelegramBotFrameworkIServiceCollectionExtensions;

namespace CalendarPicker.CalendarControl
{
    public static class BotBuilderExtension
    {
        public static ITelegramBotFrameworkBuilder<TBot> AddCalendarHandlers<TBot>(this ITelegramBotFrameworkBuilder<TBot> botBuilder) 
            where TBot : BotBase<TBot>
        {
            return botBuilder
                .AddUpdateHandler<CalendarCommand>()
                .AddUpdateHandler<ChangeToHandler>()
                .AddUpdateHandler<PickDateHandler>();
        }
    }
}
