using ABPCinemaAPI.Api.Controllers.Base;
using ABPCinemaAPI.BLL.MediatR.HallHandlers.CreateHall;
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
    }
}
