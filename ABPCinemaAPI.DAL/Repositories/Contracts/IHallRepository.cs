using ABPCinemaAPI.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ABPCinemaAPI.DAL.Repositories.Contracts
{
    public interface IHallRepository:IGenericRepository<Hall>
    {
        Task<List<Hall>> GetHallsWithAppointmentsAndServicesAsync(DateTime startDate, DateTime endDateExclusive);
    }
}
