using InsuranceTgBot.Models;

namespace InsuranceTgBot.Services.Interfaces
{
    public interface IAIService
    {
        Task<string> GetCompletion(string text, UserProgress user);
        Task<string> ConfirmData(string text, DriverLicense license, VehicleDocument vehicleDoc, UserProgress progress);
        Task<string> ConfirmedMessage(string text);
    }
}
