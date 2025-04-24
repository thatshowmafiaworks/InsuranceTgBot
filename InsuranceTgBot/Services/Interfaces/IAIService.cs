using InsuranceTgBot.Models;

namespace InsuranceTgBot.Services.Interfaces
{
    public interface IAIService
    {
        Task<string> GetCompletion(string text, UserProgress user);
    }
}
