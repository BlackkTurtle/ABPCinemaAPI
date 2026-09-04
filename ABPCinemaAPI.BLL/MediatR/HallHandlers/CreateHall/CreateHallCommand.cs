using ABPCinemaAPI.DAL.DTOs.HallDTOs;
using FluentResults;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ABPCinemaAPI.BLL.MediatR.HallHandlers.CreateHall
{
    public record CreateHallCommand(CreateHallDTO createHallDto) : IRequest<Result<GetHallDTO>>
    {
    }
}
