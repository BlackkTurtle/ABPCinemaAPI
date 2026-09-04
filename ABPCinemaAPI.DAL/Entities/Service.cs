using System;
using System.Collections.Generic;
using System.Text;

namespace ABPCinemaAPI.DAL.Entities
{
    public class Service
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public int Price { get; set; }
        public Guid HallId { get; set; }
        public Hall Hall { get; set; }
        public List<Appointment> Appointments { get; set; }
    }
}
