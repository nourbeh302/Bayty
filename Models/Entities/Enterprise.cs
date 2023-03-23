namespace Models.Entities
{
    public class Enterprise
    {
        public int Id { get; set; }
        public string TaxRecordImagePath { get; set; }
        public string TaxRecordNumber { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Location { get; set; }
        public bool IsVerified { get; set; }
        public List<User> Users { get; set; } = new List<User>();
    }
}
