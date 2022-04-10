using Microsoft.AspNetCore.Mvc;

namespace WebStore.WebApi.Controllers
{
    [Route("api/Test")]
    [ApiController]
    public class TestController : Controller
    {
        [HttpGet("getString/{id}")]
        public IActionResult GetString(string id)
        {
            return Ok($"Hello {id}");
        }
        [HttpPost("postString")]
        public IActionResult PostString([FromBody] string id)
        {
            return Ok(id);
        }
    }
}
