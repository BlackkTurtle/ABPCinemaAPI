using ABPCinemaAPI.DAL.Entities;
using ABPCinemaAPI.DAL.Persistence;
using ABPCinemaAPI.DAL.Repositories.Contracts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ABPCinemaAPI.DAL.Repositories
{
    public class AppointmentRepository:GenericRepository<Appointment>,IAppointmentRepository
    {
        private readonly AppDbContext appDbContext;
        public AppointmentRepository(AppDbContext context)
            : base(context)
        {
            appDbContext = context;
        }

        public async Task<bool> IsValidAppointment(Guid hallId, DateTime startDate, DateTime endDate)
        {
            return !await appDbContext.Appointments
                .Where(a => a.HallId == hallId)
                .AnyAsync(a => (a.StartTime < endDate && a.StartTime > startDate) || (a.EndTime < endDate && a.EndTime > startDate));
        }
    }
}
