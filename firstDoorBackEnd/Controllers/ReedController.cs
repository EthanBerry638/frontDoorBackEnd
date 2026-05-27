using Microsoft.AspNetCore.Mvc;
using firstDoorBackEnd.Services;
using firstDoorBackEnd.Repositories;
using firstDoorBackEnd.Models;
using System.Collections.Generic;

namespace firstDoorBackEnd.Controllers

{
    [ApiController]
    [Route("api/[controller]")]
    public class ReedController : ControllerBase
    {
        private readonly IReedService _reedService;

        public ReedController(IReedService reedService)
        {
            _reedService = reedService;
        }

        [HttpGet]

        public async Task<IActionResult> GetJobsAsync()

        {

            var jobs = await _reedService.GetJobsAsync();

            return Ok(jobs);

        }

    }
}
