namespace InsuranceTgBot.Models
{
    public class User
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public long TgId { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string UserName { get; set; }
        public long ChatId { get; set; }
    }
}
