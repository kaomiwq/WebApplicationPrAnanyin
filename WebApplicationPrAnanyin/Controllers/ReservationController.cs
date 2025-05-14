using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApplicationPrAnanyin.Interface;
using WebApplicationPrAnanyin.ActionClass.Reservation;
using WebApplicationPrAnanyin.ActionClass.HelperClass.DTO;
using WebApplicationPrAnanyin.Models;

namespace WebApplicationPrAnanyin.Controllers
{
    [Route("api")]
    [ApiController]
    public class ReservationController : ControllerBase
    {
        private readonly IReservation _IReservation;

        public ReservationController(IReservation iReservation)
        {
            _IReservation = iReservation;
        }
        [HttpPost("reservation/addReservation")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<ActionResult<List<string>>> Post(ReservationCreate reservationData) => await Task.FromResult(_IReservation.AddReservation(reservationData));
        [HttpGet("reservations")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<ReservationDTO>>> Get() => await Task.FromResult(_IReservation.GetReservations());
        [HttpGet("reservation")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<ReservationDTO>>> Get(int Userid) => await Task.FromResult(_IReservation.GetReservation(Userid));
        [HttpDelete("reservation/{id}")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult<List<string>>> Delete(int id) => await Task.FromResult(_IReservation.DeleteReservation(id));
        [HttpPatch("reservation/updateReservation")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<List<string>>> Patch(int Id, ReservationUpdateDTO reservation) => await Task.FromResult(_IReservation.UpdateReservation( Id, reservation));
    }
}
