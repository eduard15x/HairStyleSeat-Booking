using backend.Dtos.Salon;
using backend.Services.SalonService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace backend.Controllers
{
    [ApiController]
    [Route("api/salons")]
    [Produces("application/json")]
    public class SalonController : Controller
    {
        private readonly ISalonService _salonService;

        public SalonController(ISalonService salonService)
        {
            _salonService = salonService;
        }
        [Authorize(Roles = "admin, customer, affiliate")]
        [HttpPost("create")]
        [ProducesResponseType(typeof(JsonResult), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(JsonResult), (int)HttpStatusCode.Conflict)]
        public async Task<ActionResult> CreateNewSalon([FromBody] CreateNewSalonDto newSalonDetails)
        {
            try
            {
                var response = await _salonService.CreateNewSalon(newSalonDetails);
                return Json(Ok(response));
            }
            catch (Exception ex)
            {
                return Json(Conflict(ex.Message));
            }
        }

        [HttpGet("list")]
        [ProducesResponseType(typeof(JsonResult), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(JsonResult), (int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult> GetAllSalons([FromBody] FilterSalonDto filterSalonDto)
        {
            try
            {
                var response = await _salonService.GetAllSalons(filterSalonDto);
                return Json(Ok("Lista cu saloanele in functie de argumente"));
            }
            catch (Exception ex)
            {
                return Json(Conflict(ex));
            }
        }

        [HttpGet("{salonId}")]
        [ProducesResponseType(typeof(JsonResult), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(JsonResult), (int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult> GetSingleSalonDetails([FromRoute] int salonId)
        {
            try
            {
                var response = await _salonService.GetSingleSalonDetails(salonId);
                return Json(Ok("Detaliile salonului"));
            }
            catch (Exception ex)
            {
                return Json(Conflict(ex));
            }
        }
    }
}
