using ABPCinemaAPI.DAL.DAOs.HallDAOs;
using ABPCinemaAPI.DAL.DTOs.HallDTOs;
using FluentResults;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ABPCinemaAPI.BLL.MediatR.HallHandlers.GetHallsRevenueReport
{
    public record GetHallsRevenueReportQuery(GetHallsRevenueReportDAO GetHallsRevenueReportDAO) : IRequest<Result<List<HallRevenueReportDTO>>>
    {
    }
}
