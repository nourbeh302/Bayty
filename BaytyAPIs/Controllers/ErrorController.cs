using Microsoft.AspNetCore.Mvc;

namespace BaytyAPIs.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ErrorController : ControllerBase
    {
        [HttpGet("Error")]
        public ActionResult<string> Exception() => "Exception Happened";
    }
}
