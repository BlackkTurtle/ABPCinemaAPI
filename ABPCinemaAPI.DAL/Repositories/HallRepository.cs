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
    public class HallRepository:GenericRepository<Hall>,IHallRepository
    {
        private readonly AppDbContext _appDbContext;
        public HallRepository(AppDbContext context)
            : base(context)
        {
            _appDbContext = context;
        }

        public async Task<List<Hall>> GetHallsWithAppointmentsAndServicesAsync(DateTime startDate, DateTime endDateExclusive)
        {
            return await _appDbContext.Halls
                .Include(h => h.Services)
                .Include(h => h.Appointments.Where(a => a.StartTime >= startDate && a.StartTime < endDateExclusive))
                    .ThenInclude(a => a.Services)
                .AsNoTracking()
                .ToListAsync();
        }
    }
}
