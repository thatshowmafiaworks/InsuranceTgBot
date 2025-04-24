using InsuranceTgBot.Models;
using Telegram.Bot.Types;

namespace InsuranceTgBot.Services.Interfaces
{
    public interface IHistoryService
    {
        Task<HistoryRecord> GetById(string id);
        Task<List<HistoryRecord>> GetByUserId(long userId);
        Task<HistoryRecord> CreateNew(Message message);
    }
}