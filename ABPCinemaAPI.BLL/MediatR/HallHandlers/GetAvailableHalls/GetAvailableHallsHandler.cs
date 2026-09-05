using ABPCinemaAPI.BLL.Services.Contracts;
using ABPCinemaAPI.BLL.Specifications.HallSpecifications;
using ABPCinemaAPI.DAL.DTOs.HallDTOs;
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

namespace ABPCinemaAPI.BLL.MediatR.HallHandlers.GetAvailableHalls
{
    public class GetAvailableHallsHandler : IRequestHandler<GetAvailbaleHallsQuery, Result<IEnumerable<GetHallDTO>>>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork unitOfWork;
        private readonly ILoggerService _logger;

        public GetAvailableHallsHandler(IUnitOfWork unitOfWork, ILoggerService logger, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<Result<IEnumerable<GetHallDTO>>> Handle(GetAvailbaleHallsQuery request, CancellationToken cancellationToken)
        {
            var entitiesFromDB = await unitOfWork.HallRepository.GetAllAsync(new HallWithServicesSpec(request.GetAvailableHallsDAO));

            if (entitiesFromDB is null)
            {
                string errorMsg = "Cannot find entities!";
                _logger.LogError(request, errorMsg);
                return new Error(errorMsg);
            }

            return Result.Ok(_mapper.Map<IEnumerable<GetHallDTO>>(entitiesFromDB));
        }
    }
}
