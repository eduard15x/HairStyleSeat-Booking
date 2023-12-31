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

        #region Salon
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
        public async Task<ActionResult> GetAllSalons(int page, int pageSize, string search = "", string selectedCities = "")
        {
            try
            {
                var response = await _salonService.GetAllSalons(page, pageSize, search, selectedCities);
                return Json(Ok(response));
            }
            catch (Exception ex)
            {
                return Json(BadRequest(ex));
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
                return Json(BadRequest(ex));
            }
        }

        [HttpGet("userSalon")]
        [ProducesResponseType(typeof(JsonResult), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(JsonResult), (int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult> GetSingleSalonDetailsForUser()
        {
            try
            {
                var response = await _salonService.GetSingleSalonDetailsForUser();
                return Json(Ok(response));
            }
            catch (Exception ex)
            {
                return Json(BadRequest(ex));
            }
        }

        [Authorize(Roles = "admin, customer, affiliate")]
        [HttpPut("set-work-days")]
        [ProducesResponseType(typeof(JsonResult), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(JsonResult), (int)HttpStatusCode.NotModified)]
        public async Task<ActionResult> SetWorkDays([FromBody] SetWorkDaysDto workDaysDto)
        {
            try
            {
                var response = await _salonService.SetWorkDays(workDaysDto);
                return Json(Ok(response));
            }
            catch (Exception ex)
            {
                return Json(Conflict(ex.Message));
            }
        }

        [Authorize(Roles = "admin, employee")]
        [HttpPut("modify-salon-status")]
        [ProducesResponseType(typeof(JsonResult), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(JsonResult), (int)HttpStatusCode.NotModified)]
        public async Task<ActionResult> ModifySalonStatus([FromBody] ModifySalonStatusDto modifySalonStatusDto)
        {
            try
            {
                var response = await _salonService.ModifySalonStatus(modifySalonStatusDto);
                return Json(Ok(response));
            }
            catch (Exception ex)
            {
                return Json(Conflict(ex.Message));
            }
        }


        [Authorize(Roles = "admin, employee, customer, affiliate")]
        [HttpPut("review-salon")]
        [ProducesResponseType(typeof(JsonResult), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(JsonResult), (int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult> ReviewSalon([FromBody] ReviewSalonDto reviewSalonDto)
        {
            try
            {
                var response = await _salonService.ReviewSalon(reviewSalonDto);
                return Json(Ok(response));
            }
            catch (Exception ex)
            {
                return Json(BadRequest(ex.Message));
            }
        }

        [Authorize(Roles = "admin, employee, customer, affiliate")]
        [HttpPut("report-user")]
        [ProducesResponseType(typeof(JsonResult), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(JsonResult), (int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult> ReportUser([FromBody] ReportUserDto reportUserDto)
        {
            try
            {
                var response = await _salonService.ReportUser(reportUserDto);
                return Json(Ok(response));
            }
            catch (Exception ex)
            {
                return Json(BadRequest(ex.Message));
            }
        }

        #endregion
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

        [HttpGet("/{salonId}/services/{salonServiceName}")]
        [ProducesResponseType(typeof(JsonResult), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(JsonResult), (int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult> GetSingleSalonService([FromRoute] string salonServiceName, [FromRoute] int salonId)
        {
            try
            {
                var response = await _salonService.GetSingleSalonService(salonServiceName, salonId);
                return Json(Ok(response));
            }
            catch (Exception ex)
            {
                return Json(BadRequest(ex.Message));
            }
        }

        [HttpGet("{salonId}/services")]
        [ProducesResponseType(typeof(JsonResult), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(JsonResult), (int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult> GetAllSalonServices([FromRoute] int salonId)
        {
            try
            {
                var response = await _salonService.GetAllSalonServices(salonId);
                return Json(Ok(response));
            }
            catch (Exception ex)
            {
                return Json(BadRequest(ex.Message));
            }
        }

        [Authorize(Roles = "admin, customer, affiliate")]
        [HttpPut("update-service")]
        [ProducesResponseType(typeof(JsonResult), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(JsonResult), (int)HttpStatusCode.Conflict)]
        public async Task<ActionResult> UpdateSalonService(UpdateSalonServiceDto updateSalonServiceDto)
        {
            try
            {
                var response = await _salonService.UpdateSalonService(updateSalonServiceDto);
                return Json(Ok(response));
            }
            catch (Exception ex)
            {
                return Json(Conflict(ex.Message));
            }
        }

        [Authorize(Roles = "admin, customer, affiliate")]
        [HttpDelete("delete-service")]
        [ProducesResponseType(typeof(JsonResult), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(JsonResult), (int)HttpStatusCode.Conflict)]
        public async Task<ActionResult> DeleteSalonService(DeleteSalonServiceDto deleteSalonServiceDto)
        {
            try
            {
                var response = await _salonService.DeleteSalonService(deleteSalonServiceDto);
                return Json(Ok(response));
            }
            catch (Exception ex)
            {
                return Json(Conflict(ex.Message));
            }
        }
        #endregion
    }
}
