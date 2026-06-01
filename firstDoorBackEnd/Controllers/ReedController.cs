using Microsoft.AspNetCore.Mvc;
using firstDoorBackEnd.Services;
using firstDoorBackEnd.Repositories;
using firstDoorBackEnd.Models;
using System.Collections.Generic;

namespace firstDoorBackEnd.Controllers

{
    [ApiController]
    [Route("[controller]")]
    public class ReedController : ControllerBase
    {
        private readonly IReedService _reedService;

        public ReedController(IReedService reedService)
        {
            _reedService = reedService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllJobsAsync(string keyword, string location)
        {

            var jobs = await _reedService.GetJobsAsync("junior developer", "london");
            return Ok(jobs);
        }
    }
}
