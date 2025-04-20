using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace InsuranceTgBot.Services
{
    public class UpdateHandler(
        ITelegramBotClient bot,
        ILogger<UpdateHandler> logger,
        IUserRepository users,
        IHistoryService history
        ) : IUpdateHandler
    {
        public async Task HandleErrorAsync(ITelegramBotClient botClient, Exception exception, HandleErrorSource source, CancellationToken ct)
        {
            logger.LogWarning($"Exception:'{exception.Message}'\n with inner:{exception.InnerException?.Message}");
            if (exception is RequestException)
            {
                await Task.Delay(TimeSpan.FromSeconds(2), ct);
            }
        }

        public async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken ct)
        {
            try
            {
                ct.ThrowIfCancellationRequested();
                await (update switch
                {
                    { Message: { } message } => OnMessage(message)
                });
            }
            catch (Exception ex)
            {
                logger.LogWarning($"Exception:'{ex.Message}'\n with inner:{ex.InnerException?.Message}");
            }
        }

        private async Task OnMessage(Message message)
        {
            logger.LogInformation($"Received message:'{message.Text}' from '{message.Chat.Username}'");

            // if message is empty do nothing
            if (string.IsNullOrEmpty(message.Text)) return;

            // check if user exists in DB, if not add new user
            var user = await users.GetByTgId(message.From.Id);
            if (user is null)
                user = await users.Create(message.From, message.Chat.Id);

            // handle message basic

            Message sentMessage = await (message.Text.Split(' ')[0] switch
            {
                "/start" => StartMessage(message),
                _ => throw new NotImplementedException()
            });
            await history.CreateNew(message);
        }

        private async Task<Message> StartMessage(Message message)
        {
            var text = """
                Привіт, я допоможу оформити страхування, для цього відправ мені фото документу особи!
                """;
            return await bot.SendMessage(message.Chat.Id, text, ParseMode.Html);
        }
    }
}
