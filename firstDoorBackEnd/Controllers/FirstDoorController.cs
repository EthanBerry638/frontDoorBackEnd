using Microsoft.AspNetCore.Mvc;
using firstDoorBackEnd.Services;

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
    }
}
