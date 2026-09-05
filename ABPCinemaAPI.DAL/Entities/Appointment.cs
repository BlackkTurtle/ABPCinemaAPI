using System;
using System.Collections.Generic;
using System.Text;

namespace ABPCinemaAPI.DAL.Entities
{
    public class Appointment
    {
        public Guid Id { get; set; }
        public Guid? HallId { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public int TotalPrice { get; set; }
        public Hall? Hall { get; set; } = null!;
        public List<Service> Services { get; set; } = new List<Service>();
    }
}
