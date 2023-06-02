using Microsoft.AspNetCore.Identity;
using Models.Constants;

namespace Models.Entities
{
    public class User : IdentityUser
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public AccountType type { set; get; }
        public bool isPhoneNumberVerified { get; set; }
        public string ImagePath { get; set; } = string.Empty;
        public int Age { get; set; }
        public int CardId { get; set; }
        public Card? Card { get; set; }
        public List<Advertisement> Advertisements { get; set; } = new List<Advertisement>();
        public List<FavoriteProperties> FavoriteProperties { get; set; } = new List<FavoriteProperties>();
        public List<RefreshToken> RefreshTokens { get; set; } = new List<RefreshToken>();
    }
}