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

        public FirstDoorController (IFirstDoorService firstDoorService)
        {
            _firstDoorService = firstDoorService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllSavedJobsAsync()
        {
            await _firstDoorService.GetAllSavedJobsAsync();
            return Ok(new List<SavedJob>());
        }
    }
}
