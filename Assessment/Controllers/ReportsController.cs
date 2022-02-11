using Assessment.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Assessment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportsController : ControllerBase
    {
        private readonly IReportService _reportService;

        public ReportsController(IReportService reportService)
        {
            _reportService = reportService;
        }

        [HttpGet("GetLocationCount")]
        public async Task<IActionResult> GetLocationCount()
        {
            var locationCountList = await _reportService.GetLocationCount();

            if (locationCountList.Any())
            {
                return Ok(locationCountList);
            }

            return BadRequest();
        }
        [HttpPost("GetPersonCountByLocation")]
        public async Task<IActionResult> GetPersonCountByLocation(string location)
        {
            var personCountList = await _reportService.GetPersonCountByLocation(location);

            return Ok(personCountList);
        }
        [HttpPost("GetPhoneCountByLocation")]
        public async Task<IActionResult> GetPhoneCountByLocation(string location)
        {
            var phoneCountList = await _reportService.GetPhoneCountByLocation(location);

            return Ok(phoneCountList);

        }
    }
}
