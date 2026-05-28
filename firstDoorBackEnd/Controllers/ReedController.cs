using Microsoft.AspNetCore.Mvc;
using firstDoorBackEnd.Services;
using firstDoorBackEnd.Repositories;
using firstDoorBackEnd.Models;
using System.Collections.Generic;

namespace firstDoorBackEnd.Controllers

{
    [ApiController]
    [Route("api/reed")]
    public class ReedController : ControllerBase
    {
        private readonly IReedService _reedService;

        public ReedController(IReedService reedService)
        {
            _reedService = reedService;
        }

        [HttpGet]
        public async Task<IActionResult> GetJobsAsync(
     [FromQuery] string keyword,
     [FromQuery] string location)
        {
            var jobs = await _reedService.GetJobsAsync(keyword, location);
            return Ok(jobs);
        }

    }
}
