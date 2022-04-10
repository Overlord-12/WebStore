using Microsoft.AspNetCore.Mvc;

namespace WebStore.WebApi.Controllers
{
    [Route("aspi/[controller]")]
    [ApiController]
    public class TestController : Controller
    {
        [HttpGet("getString")]
        public IActionResult GetString(int id)
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
