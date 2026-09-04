using System;
using System.Collections.Generic;
using System.Text;

namespace ABPCinemaAPI.DAL.Entities
{
    public class Hall
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public int Capacity { get; set; }
        public int Price { get; set; }
        public List<Service> Services { get; set; }
        public List<Appointment> Appointments { get; set; }
    }
}
