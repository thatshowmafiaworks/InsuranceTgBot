using InsuranceTgBot.Models;

namespace InsuranceTgBot.Services.Interfaces
{
    public interface IDocumentGenerator
    {
        string GenerateDocument(DriverLicense license, VehicleDocument vehicleDoc, User user);
    }
}
