using InsuranceTgBot.Services;
using Microsoft.AspNetCore.Mvc;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace InsuranceTgBot.Controllers
{
    [ApiController]
    public class BotController(
            IConfiguration config,
            ILogger<BotController> logger,
            ITelegramBotClient bot,
            UpdateHandler updateHandler
        ) : ControllerBase
    {

        [HttpGet("")]
        public async Task<string> SetWebHook(CancellationToken ct)
        {
            try
            {
                var webhookurl = config["BotWebHookUrl"];
                await bot.SetWebhook(
                    webhookurl,
                    allowedUpdates: [],
                    cancellationToken: ct);
                logger.LogInformation("Succesfully set webhook");
                return $"Webhook was set to {webhookurl}";
            }
            catch (Exception ex)
            {
                logger.LogWarning($"Exception:'{ex.Message}'\n with inner:{ex.InnerException?.Message}");
                return $"Something went wrong";
            }
        }

        [HttpPost("")]
        public async Task<IActionResult> Post([FromBody] Update update, CancellationToken ct)
        {
            try
            {
                await updateHandler.HandleUpdateAsync(bot, update, ct);
            }
            catch (Exception ex)
            {
                logger.LogWarning($"Exception:'{ex.Message}'\n with inner:{ex.InnerException?.Message}");
            }
            return Ok();
        }
    }
}
