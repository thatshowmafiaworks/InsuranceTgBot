using InsuranceTgBot.Models.DTOs;
using InsuranceTgBot.Services.Interfaces;
using Telegram.Bot.Types;

namespace InsuranceTgBot.Services
{
    public class MockFormatter : IPhotoFormatter
    {
        public async Task<DriverLicenseDto> FormatDriverLicence(TGFile photo)
            => new DriverLicenseDto()
            {
                CountryCode = "CountryCode",
                State = "State",
                IdentificationNumber = "IdentificationNumber",
                Category = "Category",
                FirstName = "FirstName",
                LastName = "LastName",
                DateOfBirth = DateTime.UtcNow,
                Issued = DateTime.UtcNow,
                Expires = DateTime.UtcNow,
                DDNumber = "DDNumber"
            };

        public async Task<VehicleDocumentDto> FormatVehicleDocument(TGFile photo)
            => new VehicleDocumentDto()
            {
                VehicleIdNumber = "VehicleIdNumber",
                Manufacturer = "Manufacturer",
                Model = "Model",
                Issued = DateTime.UtcNow,
                Manufactured = DateTime.UtcNow
            };
    }
}
