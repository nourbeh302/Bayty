using Models.Constants;

namespace Server.DTOs.AuthenticationDTOs
{
    public class RegisterDTO
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public int Age { get; set; }
        public string? ImagePath { get; set; }
        public IFormFile? PersonalImage { get; set; }
        public AccountType AccountType { set; get; }
    }
}
