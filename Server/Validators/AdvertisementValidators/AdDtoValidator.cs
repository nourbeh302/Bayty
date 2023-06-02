using Server.DTOs.AdvertisementDTOs;
using FluentValidation;
using Models.Constants;
namespace Servers.Validators.AdvertisementValidators
{
    public class AdDtoValidator : AbstractValidator<AdDTO>
    {
        public AdDtoValidator()
        {
            RuleFor(a => a).Must(ValidatePropertyTypeThroughInput)
                           .WithMessage("Property type doesn't match the dto object");

            RuleFor(a => a.Address).MinimumLength(10)
                                   .WithMessage("Address with few characters leads to unuseful ad.")
                                   .MaximumLength(200)
                                   .WithMessage("Address length is too much.");


            RuleFor(a => a.Title).MinimumLength(15)
                                 .WithMessage("Title is not descriptive enough.")
                                 .MaximumLength(80)
                                 .WithMessage("Title length is too much.");

            RuleFor(a => a.KitchensCount).GreaterThan((ushort)0).WithMessage("No home has no kitchens");
            RuleFor(a => a.BathroomsCount).GreaterThan((ushort)0).WithMessage("No home has no bathrooms");
            RuleFor(a => a.RoomsCount).GreaterThan((ushort)0).WithMessage("No home has no rooms");

        }

        private bool ValidatePropertyTypeThroughInput(AdDTO ad)
        {
            if (ad.PropertyType == PropertyType.Villa)
                return ad.HasSwimmingPool.HasValue &&
                       !ad.HasElevator.HasValue &&
                       !ad.IsFurnished.HasValue &&
                       !ad.FloorNumber.HasValue &&
                       !ad.NumberOfFlats.HasValue &&
                       !ad.NumberOfFloors.HasValue &&
                       !ad.IsVitalSite.HasValue;

            
            else if (ad.PropertyType == PropertyType.Building)
                return ad.HasElevator.HasValue &&
                       ad.NumberOfFlats.HasValue &&
                       ad.NumberOfFloors.HasValue &&
                       !ad.HasSwimmingPool.HasValue &&
                       !ad.IsFurnished.HasValue &&
                       !ad.FloorNumber.HasValue &&
                       !ad.IsVitalSite.HasValue;
            
            else if (ad.PropertyType == PropertyType.Apartment)
                return ad.HasElevator.HasValue &&
                       ad.IsFurnished.HasValue &&
                       ad.FloorNumber.HasValue &&
                       ad.IsVitalSite.HasValue &&
                       !ad.NumberOfFlats.HasValue &&
                       !ad.NumberOfFloors.HasValue &&
                       !ad.HasSwimmingPool.HasValue;

            return false;
        }
    }
}
