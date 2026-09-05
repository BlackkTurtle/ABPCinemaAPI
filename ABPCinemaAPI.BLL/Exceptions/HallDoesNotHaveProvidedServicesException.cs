using Microsoft.AspNetCore.Http;

namespace ABPCinemaAPI.BLL.Exceptions
{
    public class HalldoesNotHaveProvidedServicesException : HttpException
    {
        public override int StatusCode => StatusCodes.Status400BadRequest;

        public override string Message => "Provided Hall does not offer one or more of the requested services.";
    }
}