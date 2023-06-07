using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace NZwalksDpAPI.Controllers
{
    
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetAllStudents ()
        {
            string[] students = new string[] { "dan", "john", "marius" };
            return Ok(students);
        }
    }
}
