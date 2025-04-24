using InsuranceTgBot.Models;
using InsuranceTgBot.Models.DTOs;

namespace InsuranceTgBot.Services.Interfaces
{
    public interface IUserRepository
    {
        Task<bool> IsActual(User user);
        Task<User> Create(Telegram.Bot.Types.User user, long chatId);
        Task DeleteById(string id);
        Task DeleteByTgId(long id);
        Task<User> GetByChatId(long chatId);
        Task<User> GetById(string id);
        Task<User> GetByTgId(long tgId);
        Task<User> GetByUsername(string username);
        Task Update(User user);
        Task<UserProgress> GetProgress(User user);
        Task<UserProgress> GetProgress(long tgId);
        Task<UserProgress> GetProgress(string userId);

        Task UpdateProgress(UserProgress progress);
        Task AddLicense(DriverLicenseDto dto, string userId);
        Task AddVehicleId(VehicleDocumentDto dto, string userId);
    }
}
