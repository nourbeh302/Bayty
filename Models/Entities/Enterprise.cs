namespace Models.Entities
{
    public class Enterprise
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string BrandLogoPath { get; set; } = string.Empty;
        public string TaxRecordImagePath { get; set; } = string.Empty;
        public string TaxRecordNumber { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public string Location { get; set; } = string.Empty;
        public bool IsVerified { get; set; }
        public List<User> Users { get; set; } = new List<User>();
    }
}
