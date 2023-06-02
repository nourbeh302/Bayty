namespace Models.Entities
{
    public class RefreshToken
    {
        public int Id { get; set; }
        public DateTime ExpiresOn { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime RevokedOn { get; set; }
        public string Token { get; set; } = string.Empty;
        public bool IsActive => DateTime.Now < ExpiresOn;
        public string UserId { get; set; } = string.Empty;
        public User? User { get; set; }
    }
}


/*
    access 1hr
    refresh 3months
 */