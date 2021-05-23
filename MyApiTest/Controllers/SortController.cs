using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MyApiTest.Interfaces;
using MyApiTest.Models;
using System;
using System.Net;
using System.Threading.Tasks;

namespace MyApiTest.Controllers
{
    [ApiController]
    [Route("api/answers")]
    public class SortController : ControllerBase
    {
        private readonly ISortingService _sortingService;
        private readonly ILogger<SortController> _logger;

        public SortController(ISortingService sortingService, ILogger<SortController> logger)
        {
            _sortingService = sortingService;
            _logger = logger;
        }


        /// <summary>
        /// Sort the products based on sortOption.
        /// </summary>
        /// <param name="sortOption">sort Option like Low, High</param>
        /// <returns></returns>
        [HttpGet("sort")]
        public async Task<IActionResult> Sort([FromQuery] SortOption sortOption)
        {
            try
            {
                var products = await _sortingService.GetSortedProducts(sortOption);
                return Ok(products);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }
    }
}
