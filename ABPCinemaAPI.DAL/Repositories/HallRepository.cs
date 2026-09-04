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
    public class HallRepository:GenericRepository<Hall>,IHallRepository
    {
        public HallRepository(AppDbContext context)
            : base(context)
        {
        }
    }
}
