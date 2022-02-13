using Assessment.Cache;
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

        [HttpPost("GetLocationCountOrderByDescending")]
        [Cached(600)]

        public async Task<IActionResult> GetLocationCountOrderByDescending(int typeId)
        {
            var locationCountList = await _reportService.GetLocationCount(typeId);

            if (locationCountList != null)
            {
                return Ok(locationCountList);
            }

            return NotFound();
        }
        [HttpPost("GetPersonCountByLocation")]
        [Cached(600)]

        public async Task<IActionResult> GetPersonCountByLocation(string location,int typeId)
        {
            var personCountList = await _reportService.GetPersonCountByLocation(location,typeId);

            if (personCountList != null)
            {
                return Ok(personCountList.Count);
            }

            return NotFound();

        }
        [HttpPost("GetPhoneCountByLocation")]
        [Cached(600)]

        public async Task<IActionResult> GetPhoneCountByLocation(string location,int typeId)
        {
            var phoneCountList = await _reportService.GetPhoneCountByLocation(location,typeId);

            if (phoneCountList != null)
            {
                return Ok(phoneCountList.Count);

            }

            return NotFound();

        }
    }
}
