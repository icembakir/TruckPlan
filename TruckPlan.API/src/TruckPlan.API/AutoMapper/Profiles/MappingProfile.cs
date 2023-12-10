using AutoMapper;
using TruckPlan.DataAccess.Models;
using TruckPlan.ExternalAPI.Dtos;

namespace TruckPlan.API.AutoMapper.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<LocationResponseDto, Location>()
                .ForMember(dest => dest.Country, action => action.MapFrom(src => src.Data[0].Country))
                .ForMember(dest => dest.Region, action => action.MapFrom(src => src.Data[0].Region))
                .ForMember(dest => dest.Detail, action => action.MapFrom(src => src.Data[0].Label));
        }

    }
}
