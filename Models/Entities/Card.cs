using System.ComponentModel.DataAnnotations.Schema;

namespace Models.Entities
{
    public class Card
    {
        public string Id { get; set; }
        public string CVV { get; set; }
        public DateTime ExpirationDate { get; set; }
        public string CardNumber { get; set; }
        public bool IsActive => ExpirationDate > DateTime.Now;
        public string UserId { get; set; }
        public User User { get; set; }
    }
}

