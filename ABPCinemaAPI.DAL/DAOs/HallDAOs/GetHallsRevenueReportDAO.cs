using ABPCinemaAPI.DAL.Validators;
using System;
using System.ComponentModel.DataAnnotations;

namespace ABPCinemaAPI.DAL.DAOs.HallDAOs
{
    public class GetHallsRevenueReportDAO
    {
        [Required]
        [DataType(DataType.Date)]
        public DateTime FromDate { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [GreaterThanProperty(nameof(FromDate))]
        public DateTime ToDate { get; set; }

        [Range(1, 100)]
        public int Top { get; set; } = 5;
    }
}