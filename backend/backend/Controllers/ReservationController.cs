using backend.Dtos.Reservation;
using backend.Services.ReservationService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace backend.Controllers
{
    [ApiController]
    [Route("api/reservation")]
    [Produces("application/json")]
    public class ReservationController : Controller
    {
        private readonly IReservationService _reservationService;

        public ReservationController(IReservationService reservationService)
        {
            _reservationService = reservationService;
        }

        [Authorize(Roles = "admin, customer, affiliate, employee")]
        [HttpPost("make-reservation")]
        [ProducesResponseType(typeof(JsonResult), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(JsonResult), (int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult> MakeReservation([FromBody] CreateReservationDto createReservationDto)
        {
            try
            {
                var response = await _reservationService.MakeReservation(createReservationDto);
                return Json(Ok(response));
            }
            catch (Exception ex)
            {
                return Json(Conflict(ex.Message));
            }
        }

        [Authorize(Roles = "admin, customer, affiliate, employee")]
        [HttpGet("reservations-list")]
        [ProducesResponseType(typeof(JsonResult), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(JsonResult), (int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult> GetAllCustomerReservations(int customerId)
        {
            try
            {
                var response = await _reservationService.GetAllCustomerReservations(customerId);
                return Json(Ok(response));
            }
            catch (Exception ex)
            {
                return Json(Conflict(ex.Message));
            }
        }

        [Authorize(Roles = "admin, customer, affiliate, employee")]
        [HttpGet("reservation-details")]
        [ProducesResponseType(typeof(JsonResult), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(JsonResult), (int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult> GetCustomerReservationDetails(int customerId, int reservationId)
        {
            try
            {
                var response = await _reservationService.GetCustomerReservationDetails(customerId, reservationId);
                return Json(Ok(response));
            }
            catch (Exception ex)
            {
                return Json(Conflict(ex.Message));
            }
        }

        [Authorize(Roles = "admin, affiliate, employee")]
        [HttpPut("confirm-customer-reservation")]
        [ProducesResponseType(typeof(JsonResult), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(JsonResult), (int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult> ConfirmReservation(int reservationId, int salonOwnerId)
        {
            try
            {
                var response = await _reservationService.ConfirmReservation(reservationId, salonOwnerId);
                return Json(Ok(response));
            }
            catch (Exception ex)
            {
                return Json(BadRequest(ex.Message));
            }
        }

    }
}
