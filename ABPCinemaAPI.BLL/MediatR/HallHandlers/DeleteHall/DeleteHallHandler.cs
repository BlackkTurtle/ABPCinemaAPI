using ABPCinemaAPI.BLL.Exceptions;
using ABPCinemaAPI.BLL.Services.Contracts;
using ABPCinemaAPI.BLL.Specifications.HallSpecifications;
using ABPCinemaAPI.DAL.Repositories.Contracts;
using FluentResults;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ABPCinemaAPI.BLL.MediatR.HallHandlers.DeleteHall
{
    public class DeleteHallHandler : IRequestHandler<DeleteHallCommand, Result<object>>
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly ILoggerService _logger;

        public DeleteHallHandler(IUnitOfWork unitOfWork, ILoggerService logger)
        {
            this.unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task<Result<object>> Handle(DeleteHallCommand request, CancellationToken cancellationToken)
        {
            var entityFromDB = await unitOfWork.HallRepository.GetFirstOrDefaultAsync(new HallWithServicesSpec(request.id));

            if (entityFromDB is null)
            {
                string errorMsg = $"Hall with id: {request.id} does not exist!";
                _logger.LogError(request, errorMsg);
                throw new EntityNotFoundException();
            }

            unitOfWork.HallRepository.Delete(entityFromDB);

            var isSuccessResult = await unitOfWork.SaveChangesAsync() > 0;

            if (!isSuccessResult)
            {
                const string errorMsg = "Cannot save changes in the database after entity delete!";
                _logger.LogError(request, errorMsg);
                throw new InternalServerErrorException();
            }

            return Result.Ok(new { result =  true });
        }
    }
}
