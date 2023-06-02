using Models.Constants;

namespace Servers.DTOs.AdvertisementDTOs
{
    public class AdApartmentDTO
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public double Price { get; set; }
        public ushort RoomsCount { get; set; }
        public ushort KitchensCount { get; set; }
        public ushort BathroomsCount { get; set; }
        public string UserId { get; set; }
        public DateTime Date { get; } = DateTime.UtcNow;
        public PropertyType PropertyType { get; set; }
        public int FloorNumber { get; set; }
        public bool IsFurnished { get; set; }
        public bool IsVitalSite { get; set; }
        public bool HasElevator { get; set; }
        public List<string> ImagesPaths { get; set; } = new List<string>();

    }
}
