using InsuranceTgBot.Models;

namespace InsuranceTgBot.Services
{
    public interface IUserRepository
    {
        Task<User> GetById(string id);
        Task<User> GetByChatId(long chatId);
        Task<User> GetByTgId(long id);
        Task<User> GetByUsername(string username);
        Task<User> Create(Telegram.Bot.Types.User user, long chatId);
        Task<bool> IsActual(User user);
        Task DeleteById(string id);
        Task DeleteByTgId(long id);
        Task Update(User user);
    }
}
