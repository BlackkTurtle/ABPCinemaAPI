using ABPCinemaAPI.DAL.DTOs.ServiceDTOs;
using ABPCinemaAPI.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ABPCinemaAPI.DAL.DTOs.AppointmentDTOs
{
    public class GetAppointmentDTO
    {
        public Guid Id { get; set; }
        public Guid? HallId { get; set; }
        public string HallName { get; set; } = null!;
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public int TotalPrice { get; set; }
        public List<GetServiceDTO> Services { get; set; } = new List<GetServiceDTO>();
    }
}
