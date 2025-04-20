using System.ComponentModel.DataAnnotations;

namespace InsuranceTgBot.Models
{
    public class User
    {
        public long Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        [Required]
        public string UserName { get; set; }
        public long ChatId { get; set; }
    }
}
