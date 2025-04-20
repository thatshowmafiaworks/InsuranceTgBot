namespace InsuranceTgBot.Models
{
    public class UserData
    {
        public long Id { get; set; }
        public long UserId { get; set; }
        public bool IsPaid { get; set; }
        public string Text { get; set; }
    }
}
