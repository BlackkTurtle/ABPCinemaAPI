using ABPCinemaAPI.DAL.Persistence;
using ABPCinemaAPI.DAL.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ABPCinemaAPI.DAL.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _dbContext;

        public UnitOfWork(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        private IAppointmentRepository appointmentRepository;
        private IHallRepository hallRepository;
        private IServiceRepository serviceRepository;

        public IAppointmentRepository AppointmentRepository
        {
            get
            {
                if (appointmentRepository is null)
                {
                    appointmentRepository = new AppointmentRepository(_dbContext);
                }

                return appointmentRepository;
            }
        }

        public IHallRepository HallRepository
        {   get
            {
                if (hallRepository is null)
                {
                    hallRepository = new HallRepository(_dbContext);
                }
                return hallRepository;
            }
        }

        public IServiceRepository ServiceRepository
        {
            get
            {
                if (serviceRepository is null)
                {
                    serviceRepository = new ServiceRepository(_dbContext);
                }
                return serviceRepository;
            }
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _dbContext.SaveChangesAsync();
        }
    }
}
