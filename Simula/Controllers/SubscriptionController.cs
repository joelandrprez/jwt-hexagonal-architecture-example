using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Simulador.Backend.Application.Auth;
using Simulador.Backend.Application.Subscriptions;
using Simulador.Backend.Domain.Auth.Dto;
using Simulador.Backend.Domain.Subscriptions.Dto;
using Simulador.Backend.Shared;
using System.Globalization;
using System.Security.Claims;


namespace Simulador.Backend.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    [Authorize]
    public class SubscriptionController : ControllerBase
    {
        private readonly SubscriptionApp _subscriptionApp;

        public SubscriptionController(SubscriptionApp subscriptionApp)
        {
            _subscriptionApp = subscriptionApp;
            Thread.CurrentThread.CurrentCulture = new CultureInfo("es-PE");
            Thread.CurrentThread.CurrentUICulture = CultureInfo.CreateSpecificCulture("es-PE");
            Thread.CurrentThread.CurrentUICulture.DateTimeFormat.FirstDayOfWeek = DayOfWeek.Monday;
        }

        [HttpGet]
        public async Task<ActionResult> GetByUser()
        {
            string userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            StatusResponse<List<SubscriptionResponse>> subscription = await _subscriptionApp.GetByUserId(userId);
            if (!subscription.Success)
            {
                return StatusCode(subscription.Code, subscription);
            }
            return Ok(subscription);
        }

        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
