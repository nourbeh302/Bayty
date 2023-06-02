using Server.DTOs.AdvertisementDTOs;
using Models.Constants;
using Models.Entities;

namespace Servers.Services.PropertyFactory
{
    public interface IPropertyFactory
    {
        Property GetFilledProperty(AdDTO dto, List<HouseBaseImagePath> imagePaths);
    }
}
