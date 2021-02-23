using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TinyCardLimits.Core.Services.Interfaces;
using TinyCardLimits.Core.Services.Options;

namespace TinyCardLimits.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CardController : Controller
    {
        private readonly ICardService _card;
        private readonly ILogger<ICardService> _logger;

        public CardController(ILogger<ICardService> logger,
            ICardService card)
        {
            _card = card;
            _logger = logger;
        }

        [HttpPost]
        public async Task<IActionResult> RegisterCard(
            [FromBody] RegisterCardOptions options)
        {
            var result = await _card.RegisterAsync(options);

            return Json(result);
        }
    }
}
