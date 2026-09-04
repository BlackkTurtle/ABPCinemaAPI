using ABPCinemaAPI.DAL.DTOs.ServiceDTOs;
using ABPCinemaAPI.DAL.Entities;
using AutoMapper;

namespace ABPCinemaAPI.BLL.Mapping.ServiceProfiles
{
    public class ServiceProfile : Profile
    {
        public ServiceProfile()
        {
            CreateMap<CreateServiceDTO, Service>();

            CreateMap<Service, GetServiceDTO>();
        }
    }
}
