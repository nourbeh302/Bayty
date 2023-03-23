using Microsoft.AspNetCore.Identity;
using Models.Constants;

namespace Models.Entities
{
    public class User : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public AccountType type { set; get; }
        public bool isPhoneNumberVerified { get; set; }
        public string ImagePath { get; set; }
        public int CardId { get; set; }
        public Card Card { get; set; }
        public List<Advertisement> Advertisements { get; set; } = new List<Advertisement>();
        public List<RefreshToken> RefreshTokens { get; set; } = new List<RefreshToken>();
    }
}