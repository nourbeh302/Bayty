namespace Servers.DTOs.EnterpriseDTOs
{
    public class EnterpriseDTO
    {
        public string Name { get; set; }
        public string BrandLogoPath { get; set; }
        public string TaxRecordImagePath { get; set; }
        public string TaxRecordNumber { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Location { get; set; }
        public bool IsVerified { get; } = false;
    }
}
