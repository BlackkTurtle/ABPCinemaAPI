using ABPCinemaAPI.DAL.Validators;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ABPCinemaAPI.DAL.DAOs.HallDAOs
{
    public class GetAvailableHallsDAO
    {
        [Required]
        [OperatingHour(MinHour = 6, MaxHour = 23)]
        public DateTime StartTime { get; set; }

        [Required]
        [OperatingHour(MinHour = 6, MaxHour = 23)]
        [GreaterThanProperty(nameof(StartTime))]
        public DateTime EndTime { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Capacity must be a positive number greater than 0.")]
        public int MinCapacity { get; set; }
    }
}
