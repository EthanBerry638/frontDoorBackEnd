using firstDoorBackEnd.Services;
using Microsoft.AspNetCore.Mvc;

namespace firstDoorBackEnd.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CareerJetController : ControllerBase
    {
        private readonly ICareerJetService _careerJetService;

        public CareerJetController(ICareerJetService careerJetService)
        {
            _careerJetService = careerJetService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllJobsAsync()
        {
            string userIp = HttpContext.Connection.RemoteIpAddress?.ToString() ?? string.Empty;
            string userAgent = HttpContext.Request.Headers.UserAgent.ToString() ?? string.Empty;

            var jobs = await _careerJetService.GetAllJobsAsync(userIp, userAgent);
            return Ok(jobs);
        }
    }
}
