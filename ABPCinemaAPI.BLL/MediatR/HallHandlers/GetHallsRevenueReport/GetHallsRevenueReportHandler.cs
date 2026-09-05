using ABPCinemaAPI.BLL.Services.Contracts;
using ABPCinemaAPI.BLL.Specifications.HallSpecifications;
using ABPCinemaAPI.DAL.DTOs.HallDTOs;
using ABPCinemaAPI.DAL.DTOs.ServiceDTOs;
using ABPCinemaAPI.DAL.Repositories;
using ABPCinemaAPI.DAL.Repositories.Contracts;
using AutoMapper;
using FluentResults;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ABPCinemaAPI.BLL.MediatR.HallHandlers.GetHallsRevenueReport
{
    public class GetHallsRevenueReportHandler : IRequestHandler<GetHallsRevenueReportQuery, Result<List<HallRevenueReportDTO>>>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork unitOfWork;
        private readonly ILoggerService _logger;

        public GetHallsRevenueReportHandler(IUnitOfWork unitOfWork, ILoggerService logger, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<Result<List<HallRevenueReportDTO>>> Handle(GetHallsRevenueReportQuery request, CancellationToken cancellationToken)
        {
            var dao = request.GetHallsRevenueReportDAO;

            var startDate = dao.FromDate.Date;
            var endDateExclusive = dao.ToDate.Date.AddDays(1);

            // Call the dedicated repository method
            var halls = await unitOfWork.HallRepository
                .GetHallsWithAppointmentsAndServicesAsync(startDate, endDateExclusive);

            var result = halls.Select(hall =>
            {
                // Appointments are already filtered by the DB query
                var rangeAppointments = hall.Appointments.ToList();

                var totalRevenue = rangeAppointments.Sum(a => a.TotalPrice);
                var totalHours = rangeAppointments.Sum(a => (a.EndTime - a.StartTime).TotalHours);
                var totalAppointments = rangeAppointments.Count;

                // Group services ordered within these appointments
                var serviceStats = rangeAppointments
                    .SelectMany(a => a.Services)
                    .GroupBy(s => s.Id)
                    .Select(g => new ServiceRevenueDTO
                    {
                        ServiceId = g.Key,
                        ServiceName = g.First().Name,
                        TotalAmount = g.Count(),
                        TotalPrice = g.Sum(s => s.Price)
                    })
                    .ToList();

                return new HallRevenueReportDTO
                {
                    HallId = hall.Id,
                    HallName = hall.Name,
                    TotalRevenue = totalRevenue,
                    TotalHours = totalHours,
                    TotalAppointments = totalAppointments,
                    Services = serviceStats
                };
            })
            .OrderByDescending(h => h.TotalRevenue)
            .Take(dao.Top)
            .ToList();

            return Result.Ok(result);
        }
    }
}
