using ABPCinemaAPI.Api.Controllers.Base;
using ABPCinemaAPI.BLL.MediatR.HallHandlers.CreateHall;
using ABPCinemaAPI.BLL.MediatR.HallHandlers.DeleteHall;
using ABPCinemaAPI.BLL.MediatR.HallHandlers.UpdateHall;
using ABPCinemaAPI.DAL.DTOs.HallDTOs;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ABPCinemaAPI.Api.Controllers
{
    public class HallController : BaseApiController
    {
        [HttpPost]
        public async Task<IActionResult> CreateHall([FromBody] CreateHallDTO createHallDTO)
        {
            return HandleResult(await Mediator.Send(new CreateHallCommand(createHallDTO)));
        }

        [HttpPut]
        public async Task<IActionResult> UpdateHall([FromBody] UpdateHallDTO updateHallDTO)
        {
            return HandleResult(await Mediator.Send(new UpdateHallCommand(updateHallDTO)));
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteHall([FromRoute] Guid id)
        {
            return HandleResult(await Mediator.Send(new DeleteHallCommand(id)));
        }
    }
}
