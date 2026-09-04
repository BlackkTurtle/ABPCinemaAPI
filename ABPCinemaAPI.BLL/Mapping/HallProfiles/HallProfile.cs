using ABPCinemaAPI.DAL.DTOs.HallDTOs;
using ABPCinemaAPI.DAL.Entities;
using AutoMapper;

namespace ABPCinemaAPI.BLL.Mapping.HallProfiles
{
    public class HallProfile : Profile
    {
        public HallProfile()
        {
            CreateMap<CreateHallDTO, Hall>()
                .ForMember(dest => dest.Services, opt => opt.MapFrom(src => src.Services));

            CreateMap<Hall, GetHallDTO>();

            CreateMap<UpdateHallDTO, Hall>()
                .ForMember(dest => dest.Services, opt => opt.Ignore());
        }
    }
}
