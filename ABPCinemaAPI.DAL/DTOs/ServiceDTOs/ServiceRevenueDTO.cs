using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ABPCinemaAPI.DAL.DTOs.ServiceDTOs
{
    public class ServiceRevenueDTO
    {
        public Guid ServiceId { get; set; }
        public string ServiceName { get; set; } = null!;
        public int TotalAmount { get; set; }
        public int TotalPrice { get; set; }
    }
}
