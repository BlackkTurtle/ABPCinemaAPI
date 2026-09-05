using ABPCinemaAPI.DAL.DTOs.ServiceDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ABPCinemaAPI.DAL.DTOs.HallDTOs
{
    public class HallRevenueReportDTO
    {
        public Guid HallId { get; set; }
        public string HallName { get; set; } = null!;
        public int TotalRevenue { get; set; }
        public double TotalHours { get; set; }
        public int TotalAppointments { get; set; }
        public List<ServiceRevenueDTO> Services { get; set; } = new();
    }
}
