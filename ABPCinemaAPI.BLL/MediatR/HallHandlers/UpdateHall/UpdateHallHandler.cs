using ABPCinemaAPI.BLL.Exceptions;
using ABPCinemaAPI.BLL.Services.Contracts;
using ABPCinemaAPI.BLL.Specifications.HallSpecifications;
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

namespace ABPCinemaAPI.BLL.MediatR.HallHandlers.UpdateHall
{
    public class UpdateHallHandler : IRequestHandler<UpdateHallCommand, Result<GetHallDTO>>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILoggerService _logger;

        public UpdateHallHandler(IUnitOfWork unitOfWork, ILoggerService logger, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<Result<GetHallDTO>> Handle(UpdateHallCommand request, CancellationToken cancellationToken)
        {
            var hall = await _unitOfWork.HallRepository.GetFirstOrDefaultAsync(new HallWithServicesSpec(request.UpdateHallDto.Id));

            if (hall == null)
            {
                const string errorMsg = "Hall not found!";
                _logger.LogError(request, errorMsg);
                return Result.Fail(errorMsg);
            }

            // Update hall properties
            _mapper.Map(request.UpdateHallDto, hall);

            // Handle services synchronization
            await SynchronizeServices(hall, request.UpdateHallDto.Services);

            _unitOfWork.HallRepository.Update(hall);

            var isSuccessResult = await _unitOfWork.SaveChangesAsync() > 0;

            if (!isSuccessResult)
            {
                const string errorMsg = "Cannot save changes in the database after entity update!";
                _logger.LogError(request, errorMsg);
                throw new InternalServerErrorException();
            }

            return Result.Ok(_mapper.Map<GetHallDTO>(hall));
        }

        private async Task SynchronizeServices(Hall hall, List<UpdateServiceDTO> updateServiceDtos)
        {
            var existingServices = hall.Services ?? new List<Service>();
            var updateServiceIds = updateServiceDtos
                .Where(s => s.Id != Guid.Empty)
                .Select(s => s.Id)
                .ToList();

            // Delete services that are not in the update list
            var servicesToDelete = existingServices
                .Where(s => !updateServiceIds.Contains(s.Id) && updateServiceDtos.All(u => u.Id != s.Id))
                .ToList();

            foreach (var serviceToDelete in servicesToDelete)
            {
                _unitOfWork.ServiceRepository.Delete(serviceToDelete);
            }

            // Update or create services
            foreach (var updateServiceDto in updateServiceDtos)
            {
                if (updateServiceDto.Id != Guid.Empty)
                {
                    // Update existing service
                    var existingService = existingServices.FirstOrDefault(s => s.Id == updateServiceDto.Id);
                    if (existingService != null)
                    {
                        _mapper.Map(updateServiceDto, existingService);
                        _unitOfWork.ServiceRepository.Update(existingService);
                    }
                    else
                    {
                        string errorMsg = $"Service with ID {updateServiceDto.Id} not found for update!";
                        _logger.LogError(updateServiceDto, errorMsg);
                        throw new ServiceInUpdateHallNotFoundException();
                    }
                }
                else
                {
                    // Create new service
                    var newService = _mapper.Map<Service>(updateServiceDto);
                    newService.HallId = hall.Id;
                    _unitOfWork.ServiceRepository.Create(newService);
                }
            }
        }
    }
}