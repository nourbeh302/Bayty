namespace Server.DTOs.EntitiesDTOs
{
    public class UserDTO
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Age { get; set; }
        public IFormFile Image { get; set; }
    }
}