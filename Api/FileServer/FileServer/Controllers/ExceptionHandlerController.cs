using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FileServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExceptionHandlerController : ControllerBase
    {
        [HttpGet("NoException")]
        public ActionResult NoException()
        {
            return Ok("Beispiel Exception");
        }

        [HttpGet("NotImplementedException")]
        public ActionResult GetNotImplementedException()
        {
            throw new NotImplementedException();

            // return Ok();
        }

        [HttpGet("TimeoutException")]
        public ActionResult GetTimeoutException()
        {
            throw new TimeoutException();

            // return Ok();
        }

	} // end of class

} // end of namespace
