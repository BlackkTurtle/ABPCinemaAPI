using ABPCinemaAPI.Api.Controllers.Base;
using Microsoft.AspNetCore.Mvc;

namespace ABPCinemaAPI.Api.Controllers
{
    public class Appointmentcontroller : BaseApiController
    {
        [HttpGet]
        public async Task<IActionResult> GetAllOrderedAdvertisements()
        {
            //return HandleResult(await Mediator.Send(new GetOrderedAdvertisementsQuery()));
            return Ok();
        }
    }
}
