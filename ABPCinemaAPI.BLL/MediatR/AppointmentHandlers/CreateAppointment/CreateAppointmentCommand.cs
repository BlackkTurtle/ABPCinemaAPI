using ABPCinemaAPI.DAL.DTOs.AppointmentDTOs;
using ABPCinemaAPI.DAL.DTOs.HallDTOs;
using FluentResults;
using MediatR;

namespace ABPCinemaAPI.BLL.MediatR.AppointmentHandlers.CreateAppointment
{
    public record CreateAppointmentCommand(CreateAppointmentDTO CreateAppointmentDTO) : IRequest<Result<GetAppointmentDTO>>
    {
    }
}