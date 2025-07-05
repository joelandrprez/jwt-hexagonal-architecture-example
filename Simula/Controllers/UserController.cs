using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Simulador.Backend.Application.Auth;
using Simulador.Backend.Domain.Auth.Dto;
using Simulador.Backend.Shared;
using System.Globalization;

namespace Simulador.Backend.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserApp _userApp;
        public UserController(UserApp userApp)
        {
            _userApp = userApp;
            Thread.CurrentThread.CurrentCulture = new CultureInfo("es-PE");
            Thread.CurrentThread.CurrentUICulture = CultureInfo.CreateSpecificCulture("es-PE");
            Thread.CurrentThread.CurrentUICulture.DateTimeFormat.FirstDayOfWeek = DayOfWeek.Monday;
        }

        [HttpPost("Auth")]
        public async Task<ActionResult> Login([FromBody] AuthUserRequest auth)
        {
            StatusResponse<UserResponse> login = await _userApp.Login(auth, this.GetClientIpAddress());
            if (!login.Success)
            {
                return StatusCode(login.Code,login);
            }
            return Ok(login);
        }

        [HttpPost("")]
        public async Task<ActionResult> Save([FromBody] UserRequest user)
        {
            string dataSession = "Joel Perez";
            StatusSimpleResponse login = await _userApp.Save(user, dataSession);
            if (login.Success)
            {
                return StatusCode(login.Code, login);
            }
            return Ok(login);
        }

        private string? GetClientIpAddress()
        {
            var ip = HttpContext.Request.Headers["X-Forwarded-For"].FirstOrDefault();

            if (string.IsNullOrEmpty(ip))
            {
                ip = HttpContext.Connection.RemoteIpAddress?.ToString();
            }

            return ip;
        }


    }
}
