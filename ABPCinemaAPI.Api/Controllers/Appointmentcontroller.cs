using ABPCinemaAPI.Api.Controllers.Base;
using ABPCinemaAPI.BLL.MediatR.AppointmentHandlers.CreateAppointment;
using ABPCinemaAPI.DAL.DTOs.AppointmentDTOs;
using Microsoft.AspNetCore.Mvc;

namespace ABPCinemaAPI.Api.Controllers
{
    public class Appointmentcontroller : BaseApiController
    {
        [HttpPost]
        public async Task<IActionResult> CreateAppointment([FromBody] CreateAppointmentDTO createAppointmentDTO)
        {
            return HandleResult(await Mediator.Send(new CreateAppointmentCommand(createAppointmentDTO)));
        }
    }
}
