using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace backend.Controllers
{
    [ApiController]
    [Route("api/salon")]
    [Produces("application/json")]
    public class SalonController : Controller
    {
        [HttpPost("create-new-salon")]
        [ProducesResponseType(typeof(JsonResult), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(JsonResult), (int)HttpStatusCode.Conflict)]
        public async Task<ActionResult> CreateNewSalon([FromBody] string newSalon)
        {
            // await _
            try
            {
                return Json(Ok(newSalon));
            }
            catch (Exception ex)
            {
                return Json(Conflict(ex));
            }
        }

        [HttpGet("salons")]
        [ProducesResponseType(typeof(JsonResult), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(JsonResult), (int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult> GetSalons([FromBody] string name, string person, string city)
        {
            // await _
            try
            {
                return Json(Ok("Lista cu saloanele in functie de argumente"));
            }
            catch (Exception ex)
            {
                return Json(Conflict(ex));
            }
        }
    }
}
