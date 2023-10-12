﻿using backend.Dtos.Salon;
using backend.Dtos.SalonService;
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

        [Authorize(Roles = "admin, customer, affiliate")]
        [HttpPut("update")]
        [ProducesResponseType(typeof(JsonResult), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(JsonResult), (int)HttpStatusCode.NotModified)]
        public async Task<ActionResult> UpdateSalon([FromBody] UpdateSalonDto updatedSalonDto)
        {
            try
            {
                var response = await _salonService.UpdateSalon(updatedSalonDto);
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
        public async Task<ActionResult> GetAllSalons()
        {
            try
            {
                var response = await _salonService.GetAllSalons();
                return Json(Ok(response));
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
                return Json(Ok(response));
            }
            catch (Exception ex)
            {
                return Json(Conflict(ex));
            }
        }

        #region SalonService
        [Authorize(Roles = "admin, customer, affiliate")]
        [HttpPost("create-service-for-salon")]
        [ProducesResponseType(typeof(JsonResult), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(JsonResult), (int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult> CreateNewSalonService([FromBody] CreateSalonServiceDto createSalonServiceDto)
        {
            try
            {
                var response = await _salonService.CreateNewSalonService(createSalonServiceDto);
                return Json(Ok(response));
            }
            catch (Exception ex)
            {
                return Json(BadRequest(ex.Message));
            }
        }
        #endregion
    }
}
