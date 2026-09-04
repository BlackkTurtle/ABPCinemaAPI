using ABPCinemaAPI.BLL.Exceptions;
using ABPCinemaAPI.BLL.MediatR.HallHandlers.CreateHall;
using ABPCinemaAPI.BLL.Services.Contracts;
using ABPCinemaAPI.DAL.DTOs.HallDTOs;
using ABPCinemaAPI.DAL.Entities;
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

namespace PCStore.BLL.MediatR.CommentHandlers.CreateCommentHandler
{
    public class CreateHallHandler : IRequestHandler<CreateHallCommand, Result<GetHallDTO>>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork unitOfWork;
        private readonly ILoggerService _logger;

        public CreateHallHandler(IUnitOfWork unitOfWork, ILoggerService logger, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<Result<GetHallDTO>> Handle(CreateHallCommand request, CancellationToken cancellationToken)
        {
            var hallEntity = _mapper.Map<Hall>(request.createHallDto);

            var createdhall = unitOfWork.HallRepository.Create(hallEntity);

            var isSuccessResult = await unitOfWork.SaveChangesAsync() > 0;

            if (!isSuccessResult)
            {
                const string errorMsg = "Cannot save changes in the database after entity creation!";
                _logger.LogError(request, errorMsg);
                throw new InternalServerErrorException();
            }

            return Result.Ok(_mapper.Map<GetHallDTO>(createdhall));
        }
    }
}
