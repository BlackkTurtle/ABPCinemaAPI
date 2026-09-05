using ABPCinemaAPI.DAL.Entities;
using ABPCinemaAPI.DAL.Validators;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ABPCinemaAPI.DAL.DTOs.AppointmentDTOs
{
    public class CreateAppointmentDTO
    {
        [Required]
        public Guid HallId { get; set; }
        [Required]
        [OperatingHour(MinHour = 6, MaxHour = 23)]
        public DateTime StartTime { get; set; }
        [Required]
        [OperatingHour(MinHour = 6, MaxHour = 23)]
        [GreaterThanProperty(nameof(StartTime))]
        public DateTime EndTime { get; set; }
        public List<Guid> Services { get; set; } = new List<Guid>();
    }
}
