using Microsoft.AspNetCore.Mvc;
using firstDoorBackEnd.Services;
using firstDoorBackEnd.Models;

namespace firstDoorBackEnd.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FirstDoorController : ControllerBase
    {
        private readonly IFirstDoorService _firstDoorService;

        public FirstDoorController(IFirstDoorService firstDoorService)
        {
            _firstDoorService = firstDoorService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllSavedJobsAsync()
        {
            var jobs = await _firstDoorService.GetAllSavedJobsAsync();

            return Ok(jobs);
        }
            
        [HttpGet("{id}")]
        public async Task<IActionResult> GetJobByIDAsync(int id)
        {
            var job = await _firstDoorService.GetJobByIDAsync(id);
            return Ok(job); 
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> UpdateJobStatusAsync(int id)
        {
            return BadRequest();
        }
    }
}
