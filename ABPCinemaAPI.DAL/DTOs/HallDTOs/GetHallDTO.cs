using ABPCinemaAPI.DAL.DTOs.ServiceDTOs;
using ABPCinemaAPI.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ABPCinemaAPI.DAL.DTOs.HallDTOs
{
    public class GetHallDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public int Capacity { get; set; }
        public int Price { get; set; }
        public List<GetServiceDTO> Services { get; set; } = new List<GetServiceDTO>();
    }
}
