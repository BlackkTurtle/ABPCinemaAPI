using ABPCinemaAPI.DAL.DAOs.HallDAOs;
using ABPCinemaAPI.DAL.Entities;
using ABPCinemaAPI.DAL.Specification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ABPCinemaAPI.BLL.Specifications.HallSpecifications
{
    public class HallWithServicesSpec : BaseSpecification<Hall, Hall>
    {
        public HallWithServicesSpec(Guid id) : base(x => x.Id == id)
        {
            AddInclude(x => x.Services);
        }

        public HallWithServicesSpec(GetAvailableHallsDAO dao)
            : base(x => x.Capacity >= dao.MinCapacity &&
                        !x.Appointments.Any(a => a.StartTime < dao.EndTime &&
                                                 a.EndTime > dao.StartTime))
        {
            AddInclude(x => x.Services);
        }
    }
}
