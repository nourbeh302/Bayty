using Servers.Constants;
using Models.Constants;

namespace Servers.DTOs.AdvertisementDTOs
{
    public class AdSearchDTO
    {
        public string? City { get; set; }
        public double? Price { get; set; }
        public PropertyType? PropertyType { get; set; }
        public bool? isRent { get; set; }
        public string? NormalizedText { get; set; }
        public OrderDirection Order { get; set; }
    }
}
