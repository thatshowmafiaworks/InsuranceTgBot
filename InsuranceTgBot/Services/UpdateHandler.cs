using InsuranceTgBot.Services.Interfaces;
using Microsoft.IdentityModel.Tokens;
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
        IHistoryService history,
        IAIService aIService,
        IPhotoFormatter photoFormatter
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
            // check if user exists in DB, if not add new user
            var user = await users.GetByTgId(message.From.Id);
            if (user is null)
                user = await users.Create(message.From, message.Chat.Id);

            // logging message for development
            if (!string.IsNullOrWhiteSpace(message.Text)) logger.LogInformation($"Received message:'{message.Text}' from '{message.Chat.Username}'");
            if (!message.Photo.IsNullOrEmpty()) logger.LogInformation($"Received Photo from '{message.Chat.Username}'");


            // if Message.Text and Message.Photo is empty do nothing
            if (string.IsNullOrEmpty(message.Text) && message.Photo.IsNullOrEmpty()) return;


            // handle message basic
            if (!message.Photo.IsNullOrEmpty())
            {
                Message sentPhoto = await ProcessPhoto(message);
            }
            else if (!string.IsNullOrWhiteSpace(message.Text))
            {
                Message sentMessage = await (message.Text.Split(' ')[0] switch
                {
                    "/start" => StartMessage(message),
                    "/restart" => RestartInteraction(message),
                    _ => Usage(message)
                });
            }
            await history.CreateNew(message);
        }

        private async Task<Message> StartMessage(Message message)
        {
            var text = """
                Привіт, я допоможу оформити страхування, для цього відправ мені фото документу особи або задай запитання!
                """;
            return await bot.SendMessage(message.Chat.Id, text, ParseMode.Html);
        }

        private async Task<Message> Usage(Message message)
        {
            var progress = await users.GetProgress(message.From.Id);
            progress.LastMessage = message.Text;
            await users.UpdateProgress(progress);

            var response = await aIService.GetCompletion(message.Text, progress);
            return await bot.SendMessage(message.Chat.Id, response, ParseMode.Html);
        }

        private async Task<Message> RestartInteraction(Message message)
        {
            throw new NotImplementedException();
        }

        private async Task<Message> ProcessPhoto(Message message)
        {
            // check progress on filling data
            var progress = await users.GetProgress(message.From.Id);
            var tgFile = await bot.GetFile(message.Photo[message.Photo.Length - 1].FileId);
            var userId = (await users.GetByTgId(message.From.Id)).Id;

            // first check to fill is a Driver License
            if (!progress.ProvidedDriverLicense)
            {
                var driverLicense = await photoFormatter.FormatDriverLicence(tgFile);

                progress.ProvidedDriverLicense = true;
                await users.AddLicense(driverLicense, userId);
                await users.UpdateProgress(progress);
                var text = await aIService.GetCompletion(progress.LastMessage, progress);
                return await bot.SendMessage(message.Chat.Id, text);
            }

            //second check to fill is a Vehicle Document
            if (!progress.ProvidedVehicleIdentificationDocument)
            {
                var vehicleId = await photoFormatter.FormatVehicleDocument(tgFile);


                progress.ProvidedVehicleIdentificationDocument = true;
                await users.AddVehicleId(vehicleId, userId);
                await users.UpdateProgress(progress);
                var text = await aIService.GetCompletion(progress.LastMessage, progress);
                return await bot.SendMessage(message.Chat.Id, text);
            }
            return await bot.SendMessage(message.Chat.Id, "Something went wrong try again please");
        }
    }
}
