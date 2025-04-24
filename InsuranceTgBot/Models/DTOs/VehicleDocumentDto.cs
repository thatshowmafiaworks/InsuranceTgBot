namespace InsuranceTgBot.Models.DTOs
{
    public class VehicleDocumentDto
    {
        public string VehicleIdNumber { get; set; } = "";
        public string Manufacturer { get; set; } = "";
        public string Model { get; set; } = "";
        public DateTime Issued { get; set; } = default;
        public DateTime Manufactured { get; set; } = default;
    }
}
