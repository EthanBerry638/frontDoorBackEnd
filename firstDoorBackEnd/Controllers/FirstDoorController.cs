using Microsoft.AspNetCore.Mvc;
using firstDoorBackEnd.Services;
using firstDoorBackEnd.Models;

namespace firstDoorBackEnd.Controllers
{
    [ApiController]
    [Route("api/{controller}")]
    public class FirstDoorController : ControllerBase
    {
        private readonly IFirstDoorService _firstDoorService;

        public FirstDoorController(IFirstDoorService firstDoorService)
        {
            _firstDoorService = firstDoorService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetJobByIDAsync(int id)
        {
            var job = await _firstDoorService.GetJobByIDAsync(id);
            return Ok(job); 
        }
    }
}
