namespace InsuranceTgBot.Models
{
    public class UserData
    {
        public string Id { get; set; }
        public string UserId { get; set; }
        public User User { get; set; }
        public bool IsPaid { get; set; }
        public string? DriverLicenseId { get; set; }
        public DriverLicense? DriverLicense { get; set; } = null;
        public string? VehicleDocumentId { get; set; }
        public VehicleDocument? VehicleDocument { get; set; } = null;
    }
}
