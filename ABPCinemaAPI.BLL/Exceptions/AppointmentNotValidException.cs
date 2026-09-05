using Microsoft.AspNetCore.Http;

namespace ABPCinemaAPI.BLL.Exceptions
{
    public class AppointmentNotValidException : HttpException
    {
        public override int StatusCode => StatusCodes.Status400BadRequest;

        public override string Message => "Appointment not valid. Someone already has the appointment for provided hours.";
    }
}