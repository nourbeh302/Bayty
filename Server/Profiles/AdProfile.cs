using Server.DTOs.AdvertisementDTOs;
using AutoMapper;
using Models.Entities;

namespace Server.Profiles
{
    public class AdProfile : Profile
    {
        public AdProfile()
        {
            //CreateMap<Villa, AdWithVillaDTO>()
            //    .ForMember(dest => dest.RoomsCount,      opt => opt.MapFrom(v => v.VillaFeatures.RoomsCount))
            //    .ForMember(dest => dest.Title,           opt => opt.MapFrom(v => v.VillaFeatures.Advertisement.Title))
            //    .ForMember(dest => dest.HasSwimmingPool, opt => opt.MapFrom(v => v.HasSwimmingPool));

            CreateMap<AdDTO, Advertisement>();
        }
    }
}
