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
    }
}
