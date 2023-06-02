using System.ComponentModel.DataAnnotations.Schema;

namespace Models.Entities
{
    public class Card
    {
        public string Id { get; set; } = string.Empty;
        public string CVV { get; set; } = string.Empty;
        public DateTime ExpirationDate { get; set; }
        public string CardNumber { get; set; } = string.Empty;
        public bool IsActive => ExpirationDate > DateTime.Now;
        public string UserId { get; set; } = string.Empty;
        public User? User { get; set; }
    }
}

