using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace NZWalksAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetAllSudents()
        {
            string[] StudentsName = new string[] { "peter", "ramy", "amir", "jerry" };
            return Ok(StudentsName);
        }
    }
}
