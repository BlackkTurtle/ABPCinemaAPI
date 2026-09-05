using ABPCinemaAPI.BLL.Exceptions;
using ABPCinemaAPI.BLL.Services.Contracts;
using ABPCinemaAPI.BLL.Specifications.HallSpecifications;
using ABPCinemaAPI.DAL.DTOs.AppointmentDTOs;
using ABPCinemaAPI.DAL.DTOs.HallDTOs;
using ABPCinemaAPI.DAL.DTOs.ServiceDTOs;
using ABPCinemaAPI.DAL.Entities;
using ABPCinemaAPI.DAL.Repositories.Contracts;
using AutoMapper;
using FluentResults;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ABPCinemaAPI.BLL.MediatR.AppointmentHandlers.CreateAppointment
{
    public class CreateAppointmentHandler : IRequestHandler<CreateAppointmentCommand, Result<GetAppointmentDTO>>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILoggerService _logger;

        public CreateAppointmentHandler(IUnitOfWork unitOfWork, ILoggerService logger, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<Result<GetAppointmentDTO>> Handle(CreateAppointmentCommand request, CancellationToken cancellationToken)
        {
            var hall = await _unitOfWork.HallRepository.GetFirstOrDefaultAsync(new HallWithServicesSpec(request.CreateAppointmentDTO.HallId));

            if (hall == null)
            {
                const string errorMsg = "Hall not found!";
                _logger.LogError(request, errorMsg);
                return Result.Fail(errorMsg);
            }

            // Check appointment validity
            await CheckAppointmentValidity(request);

            // Check if Hall has provided services
            CheckServices(hall, request);

            //Create Appointment Entity
            var appointment = new Appointment()
            {
                Id = Guid.NewGuid(),
                HallId = hall.Id,
                Hall = hall,
                StartTime = request.CreateAppointmentDTO.StartTime,
                EndTime = request.CreateAppointmentDTO.EndTime,
                TotalPrice = CalculateTotalPrice(hall, request.CreateAppointmentDTO),
                Services = hall.Services.Where(x => request.CreateAppointmentDTO.Services.Contains(x.Id)).ToList(),
            };

            var isSuccessResult = await _unitOfWork.SaveChangesAsync() > 0;

            if (!isSuccessResult)
            {
                const string errorMsg = "Cannot save changes in the database after entity update!";
                _logger.LogError(request, errorMsg);
                throw new InternalServerErrorException();
            }

            return Result.Ok(_mapper.Map<GetAppointmentDTO>(appointment));
        }

        private int CalculateTotalPrice(Hall hall, CreateAppointmentDTO createAppointmentDTO)
        {
            decimal totalHallPrice = 0;

            var current = createAppointmentDTO.StartTime;
            var endTime = createAppointmentDTO.EndTime;

            // Iterates 1 hour at a time
            while (current < endTime)
            {
                int hour = current.Hour;
                decimal multiplier = 1.0m;

                if (hour >= 6 && hour < 9)
                {
                    multiplier = 0.90m; // Morning hours (06:00 - 09:00)
                }
                else if (hour >= 12 && hour < 14)
                {
                    multiplier = 1.15m; // Peak hours (12:00 - 14:00)
                }
                else if (hour >= 18 && hour < 23)
                {
                    multiplier = 0.80m; // Evening hours (18:00 - 23:00)
                }

                totalHallPrice += hall.Price * multiplier;
                current = current.AddHours(1);
            }

            int servicesPrice = hall.Services
                .Where(s => createAppointmentDTO.Services.Contains(s.Id))
                .Sum(s => s.Price);

            return Convert.ToInt32(Math.Round(totalHallPrice)) + servicesPrice;
        }

        private void CheckServices(Hall hall, CreateAppointmentCommand request)
        {
            var validServiceIds = hall.Services.Select(s => s.Id).ToHashSet();

            // Check if any requested service ID does not exist in the hall's available services
            if (request.CreateAppointmentDTO.Services.Any(id => !validServiceIds.Contains(id)))
            {
                const string errorMsg = "Provided Hall does not offer one or more of the requested services.";
                _logger.LogError(request, errorMsg);
                throw new HalldoesNotHaveProvidedServicesException();
            }
        }

        private async Task CheckAppointmentValidity(CreateAppointmentCommand request)
        {
            var result = await _unitOfWork.AppointmentRepository.IsValidAppointment(request.CreateAppointmentDTO.HallId,
                request.CreateAppointmentDTO.StartTime,
                request.CreateAppointmentDTO.EndTime);

            if (!result)
            {
                const string errorMsg = "Appointment not valid. Someone already has the appointment for provided hours.";
                _logger.LogError(request, errorMsg);
                throw new AppointmentNotValidException();
            }
        }
    }
}