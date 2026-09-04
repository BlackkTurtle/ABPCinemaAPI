using FluentResults;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ABPCinemaAPI.BLL.MediatR.HallHandlers.DeleteHall
{
    public record DeleteHallCommand(Guid id) : IRequest<Result<object>>
    {
    }
}
