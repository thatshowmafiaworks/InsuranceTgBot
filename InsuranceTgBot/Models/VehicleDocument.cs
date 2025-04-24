namespace InsuranceTgBot.Models
{
    public class VehicleDocument
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string VehicleIdNumber { get; set; } = "";
        public string Manufacturer { get; set; } = "";
        public string Model { get; set; } = "";
        public DateTime Issued { get; set; } = default;
        public DateTime Manufactured { get; set; } = default;
        public UserData UserData { get; set; }
    }
}
