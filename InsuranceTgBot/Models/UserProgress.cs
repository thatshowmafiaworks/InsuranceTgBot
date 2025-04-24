namespace InsuranceTgBot.Models
{
    public class UserProgress
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string UserId { get; set; }
        public long UserTgId { get; set; }
        public bool ProvidedDriverLicense { get; set; } = false;
        public bool ProvidedVehicleIdentificationDocument { get; set; } = false;
        public bool IsPaid { get; set; } = false;
        public string LastMessage { get; set; } = "";
    }
}
