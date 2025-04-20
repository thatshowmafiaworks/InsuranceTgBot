namespace InsuranceTgBot.Models
{
    public class HistoryRecord
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public long UserId { get; set; }
        public string Text { get; set; }
        public DateTime DateTime { get; set; }
    }
}
