using Server.DTOs.AdvertisementDTOs;
using Models.Entities;
using Models.Constants;

namespace Servers.Services.PropertyFactory
{
    public class PropertyFactory : IPropertyFactory
    {
        public Property GetFilledProperty(AdDTO dto, List<HouseBaseImagePath> imagePaths)
        {
            if (dto != null && dto.PropertyType != null)
            {
                switch (dto.PropertyType)
                {
                    case PropertyType.Building:
                        return GenerateFilledBuilding(dto, imagePaths);
                    case PropertyType.Villa:
                        return GenerateFilledVilla(dto, imagePaths);
                    case PropertyType.Apartment:
                        return GenerateFilledApartment(dto, imagePaths);
                    default:
                        return null;
                }
            }
            return null;
        }

        private Building GenerateFilledBuilding(AdDTO dto, List<HouseBaseImagePath> imagePaths)
        {
            var houseBase = new HouseBase
            {
                Address = dto.Address,
                City = dto.City,
                KitchensCount = dto.KitchensCount,
                Price = dto.Price,
                BathroomsCount = dto.BathroomsCount,
                RoomsCount = dto.RoomsCount,
                HouseBaseImagePaths = imagePaths
            };

            var advertisement = new Advertisement
            {
                Date = dto.Date,
                Description = dto.Description,
                Title = dto.Title,
                HouseBase = houseBase,
                PropertyType = dto.PropertyType
            };

            houseBase.Advertisement = advertisement;

            return new Building
            {
                HasElevator = dto.HasElevator ?? throw new NullReferenceException(),
                NumberOfFlats = dto.NumberOfFlats ?? throw new NullReferenceException(),
                NumberOfFloors = dto.NumberOfFloors ?? throw new NullReferenceException(),
                HouseBase = houseBase
            };
        }
        private Villa GenerateFilledVilla(AdDTO dto, List<HouseBaseImagePath> imagePaths)
        {
            var houseBase = new HouseBase
            {
                Address = dto.Address,
                City = dto.City,
                KitchensCount = dto.KitchensCount,
                Price = dto.Price,
                BathroomsCount = dto.BathroomsCount,
                RoomsCount = dto.RoomsCount,
                HouseBaseImagePaths = imagePaths
            };

            var advertisement = new Advertisement
            {
                Date = dto.Date,
                Description = dto.Description,
                Title = dto.Title,
                HouseBase = houseBase,
                PropertyType = dto.PropertyType
            };

            houseBase.Advertisement = advertisement;

            return new Villa
            {
                HasSwimmingPool = dto.HasSwimmingPool ?? throw new NullReferenceException(),
                HouseBase = houseBase
            };
        }
        private Apartment GenerateFilledApartment(AdDTO dto, List<HouseBaseImagePath> imagePaths)
        {
            var houseBase = new HouseBase
            {
                Address = dto.Address,
                City = dto.City,
                KitchensCount = dto.KitchensCount,
                Price = dto.Price,
                BathroomsCount = dto.BathroomsCount,
                RoomsCount = dto.RoomsCount,
                HouseBaseImagePaths = imagePaths
            };

            var advertisement = new Advertisement
            {
                Date = dto.Date,
                Description = dto.Description,
                Title = dto.Title,
                HouseBase = houseBase,
                PropertyType = dto.PropertyType
            };

            houseBase.Advertisement = advertisement;

            return new Apartment
            {
                HasElevator = dto.HasElevator ?? throw new NullReferenceException(),
                FloorNumber = dto.FloorNumber ?? throw new NullReferenceException(),
                IsVitalSite = dto.IsVitalSite ?? throw new NullReferenceException(),
                IsFurnished = dto.IsFurnished ?? throw new NullReferenceException(),
                HouseBase = houseBase
            };
        }
    }
}
