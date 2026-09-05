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
    public class HallWithServicesAndAppointmentsSpec : BaseSpecification<Hall, Hall>
    {
        public HallWithServicesAndAppointmentsSpec(GetHallsRevenueReportDAO dao)
            : base(h => h.Appointments.Any(a => a.StartTime >= dao.FromDate.Date &&
                                                a.StartTime < dao.ToDate.Date.AddDays(1)))
        {
            AddInclude(h => h.Services);
            AddInclude(h => h.Appointments);
        }
    }
}
