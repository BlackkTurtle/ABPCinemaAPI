using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ABPCinemaAPI.DAL.Repositories.Contracts
{
    public interface IUnitOfWork
    {
        IAppointmentRepository AppointmentRepository { get; }
        IHallRepository HallRepository { get; }
        IServiceRepository ServiceRepository { get; }
        public Task<int> SaveChangesAsync();
    }
}
