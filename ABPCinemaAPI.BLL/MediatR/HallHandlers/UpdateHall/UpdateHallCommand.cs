using ABPCinemaAPI.DAL.DTOs.HallDTOs;
using FluentResults;
using MediatR;

namespace ABPCinemaAPI.BLL.MediatR.HallHandlers.UpdateHall
{
    public record UpdateHallCommand(UpdateHallDTO UpdateHallDto) : IRequest<Result<GetHallDTO>>
    {
    }
}