namespace InsuranceTgBot.Models
{
    public class DriverLicense
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string CountryCode { get; set; } = "";
        public string State { get; set; } = "";
        public string IdentificationNumber { get; set; } = "";
        public string Category { get; set; } = "";
        public string FirstName { get; set; } = "";
        public string LastName { get; set; } = "";
        public DateTime DateOfBirth { get; set; } = default;
        public DateTime Issued { get; set; } = default;
        public DateTime Expires { get; set; } = default;
        public string DDNumber { get; set; } = "";
        public UserData UserData { get; set; }
    }
}
