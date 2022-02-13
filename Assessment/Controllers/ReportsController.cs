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

        [HttpGet("GetLocationCountOrderByDescending")]
        public async Task<IActionResult> GetLocationCountOrderByDescending()
        {
            var locationCountList = await _reportService.GetLocationCount();

            if (locationCountList != null)
            {
                return Ok(locationCountList);
            }

            return NotFound();
        }
        [HttpPost("GetPersonCountByLocation")]
        public async Task<IActionResult> GetPersonCountByLocation(string location)
        {
            var personCountList = await _reportService.GetPersonCountByLocation(location);

            if (personCountList != null)
            {
                return Ok(personCountList.Count);
            }

            return NotFound();

        }
        [HttpPost("GetPhoneCountByLocation")]
        public async Task<IActionResult> GetPhoneCountByLocation(string location)
        {
            var phoneCountList = await _reportService.GetPhoneCountByLocation(location);

            if (phoneCountList != null)
            {
                return Ok(phoneCountList.Count);

            }

            return NotFound();

        }
    }
}
