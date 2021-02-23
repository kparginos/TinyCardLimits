using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

using TinyCardLimits.Core.Services.Interfaces;
using TinyCardLimits.Core.Services.Options;

namespace TinyCardLimits.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]

    public class CardLimitController : Controller
    {
        private readonly ICardLimitService _cardLimit;
        private readonly ILogger<ICardLimitService> _logger;

        public CardLimitController(ILogger<ICardLimitService> logger,
            ICardLimitService cardLimit)
        {
            _logger = logger;
            _cardLimit = cardLimit;
        }

        [HttpPost("Authorize")]
        public async Task<IActionResult> Authorize(
            [FromBody] RegisterCardLimitOptions options)
        {
            var result = await _cardLimit.RegisterAsync(options);

            return Json(result);
        }

        [HttpGet("{cardNumber}")]
        public async Task<IActionResult> GetCardTrans(string cardNumber)
        {
            var result = await _cardLimit.GetCardLimitsAsync(cardNumber);

            return Json(result);
        }
    }
}
