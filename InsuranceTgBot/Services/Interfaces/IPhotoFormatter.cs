using InsuranceTgBot.Models.DTOs;
using Telegram.Bot.Types;

namespace InsuranceTgBot.Services.Interfaces
{
    public interface IPhotoFormatter
    {
        Task<DriverLicenseDto> FormatDriverLicence(TGFile photo);
        Task<VehicleDocumentDto> FormatVehicleDocument(TGFile photo);
    }
}
