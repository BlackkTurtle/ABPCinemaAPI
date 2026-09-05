using ABPCinemaAPI.DAL.DTOs.AppointmentDTOs;
using ABPCinemaAPI.DAL.DTOs.HallDTOs;
using ABPCinemaAPI.DAL.Entities;
using AutoMapper;

namespace ABPCinemaAPI.BLL.Mapping.AppointmentProfiles
{
    public class AppointmentProfile : Profile
    {
        public AppointmentProfile()
        {
            CreateMap<Appointment, GetAppointmentDTO>()
                .ForMember(dest => dest.HallName, opt => opt.MapFrom(src => src.Hall != null ? src.Hall.Name : string.Empty))
                .ForMember(dest => dest.Services, opt => opt.MapFrom(src => src.Services));
        }
    }
}
