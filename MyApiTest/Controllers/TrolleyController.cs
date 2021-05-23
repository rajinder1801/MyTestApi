using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MyApiTest.Interfaces;
using MyApiTest.Models;
using System;
using System.Net;

namespace MyApiTest.Controllers
{
    [ApiController]
    [Route("api/answers")]
    public class TrolleyController : ControllerBase
    {
        private readonly ITrolleyService _trolleyService;
        private readonly ILogger<TrolleyController> _logger;

        public TrolleyController(ITrolleyService trolleyService, ILogger<TrolleyController> logger)
        {
            _trolleyService = trolleyService;
            _logger = logger;
        }


        /// <summary>
        /// Calculates the minimum total for the trolley request.
        /// </summary>
        /// <param name="trolleyRequest">The trolley request object.</param>
        /// <returns>Minimum trolley amount.</returns>
        [HttpPost("trolleyTotal")]
        public IActionResult MinimumTotal([FromBody] TrolleyRequest trolleyRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var lowestTotal = _trolleyService.CalculateMinimumTotal(trolleyRequest);
                return Ok(lowestTotal);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }

    }
}
