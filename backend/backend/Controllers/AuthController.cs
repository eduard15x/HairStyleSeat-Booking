using backend.Dtos.Auth;
using backend.Services.AuthService;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace backend.Controllers
{
    [ApiController]
    [Route("api/user")]
    [Produces("application/json")]
    public class AuthController : Controller
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("register")]
        [ProducesResponseType(typeof(JsonResult), (int)HttpStatusCode.Created)]
        [ProducesResponseType(typeof(JsonResult), (int)HttpStatusCode.Conflict)]
        public async Task<ActionResult> Register([FromBody] RegisterUserDto registerUserDto)
        {
            try
            {
                var response = await _authService.Register(registerUserDto);
                return Json(CreatedAtAction(nameof(Register), response));
            } 
            catch (Exception ex)
            {
                return Json(Conflict(ex.Message));
            }
        }
    }
}
