using ABPCinemaAPI.DAL.Entities;
using ABPCinemaAPI.DAL.Persistence;
using ABPCinemaAPI.DAL.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ABPCinemaAPI.DAL.Repositories
{
    public class ServiceRepository:GenericRepository<Service>,IServiceRepository
    {
        public ServiceRepository(AppDbContext context)
            : base(context)
        {
        }
    }
}
