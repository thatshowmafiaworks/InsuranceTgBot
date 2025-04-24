using InsuranceTgBot.Data;
using InsuranceTgBot.Models;
using InsuranceTgBot.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Telegram.Bot.Types;

namespace InsuranceTgBot.Services
{
    public class HistoryService(
            AppDbContext context,
            ILogger<HistoryService> logger
        ) : IHistoryService
    {
        public async Task<HistoryRecord> CreateNew(Message message)
        {
            if (string.IsNullOrWhiteSpace(message.Text))
            {
                logger.LogWarning($"Message is null from user:{message.From.Username} with userId:{message.From.Id} from chatId:{message.Chat.Id}");
                return null;
            }
            try
            {
                var record = new HistoryRecord()
                {
                    Id = Guid.NewGuid().ToString(),
                    UserId = message.From.Id,
                    Text = message.Text,
                    DateTime = DateTime.UtcNow
                };
                await context.HistoryRecords.AddAsync(record);
                await context.SaveChangesAsync();
                return record;
            }
            catch (Exception ex)
            {
                logger.LogWarning($"Exception:'{ex.Message}'\n with inner:{ex.InnerException?.Message}");
                return null;
            }
        }

        public async Task<HistoryRecord> GetById(string id)
        {
            try
            {
                var record = await context.HistoryRecords.Where(x => x.Id.Equals(id)).FirstOrDefaultAsync();
                return record;
            }
            catch (Exception ex)
            {
                logger.LogWarning($"Exception:'{ex.Message}'\n with inner:{ex.InnerException?.Message}");
                return null;
            }
        }

        public async Task<List<HistoryRecord>> GetByUserId(long userId)
        {
            try
            {
                var records = await context.HistoryRecords.Where(x => x.UserId == userId).ToListAsync();
                return records;
            }
            catch (Exception ex)
            {
                logger.LogWarning($"Exception:'{ex.Message}'\n with inner:{ex.InnerException?.Message}");
                return null;
            }
        }
    }
}