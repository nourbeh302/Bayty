namespace Models.Entities
{
    public class RefreshToken
    {
        public int Id { get; set; }
        public DateTime ExpiresOn { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime RevokedOn { get; set; }
        public string Token { get; set; }
        public bool IsActive => DateTime.Now < ExpiresOn;
        public string UserId { get; set; }
        public User User { get; set; }
    }
}
