using Microsoft.AspNetCore.Mvc;

namespace ZinCorp.WebAPI.Controllers
{
    [Route("api/common")]
    [ApiController]
    public class CommonController : ControllerBase
    {

        [HttpGet]
        public IActionResult Get()
        {
            return Ok();
        }
    }
}
