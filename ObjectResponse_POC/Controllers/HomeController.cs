using Microsoft.AspNetCore.Mvc;
using ObjectResponse_POC.Model;
using ObjectResponse_POC.Service;

namespace ObjectResponse_POC.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HomeController : Controller
    {
        private readonly HomeService _service;
        public HomeController(HomeService service)
        {
            _service = service;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<Home> GetAll()
        {
            return _service.GetHome();
        }

        [HttpGet("getNotFound")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<Home> getNotFound()
        {
            return NotFound();
        }
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<Home> Create(Home obj)
        {
            if (!string.IsNullOrEmpty(obj.Name))
            {
                var problemDetails = new ValidationProblemDetails()
                {
                    Title = "Validation Error",
                    Status = StatusCodes.Status400BadRequest,
                    Detail = "The field is mandatory",
                    Instance = HttpContext.Request.Path
                };

                problemDetails.Errors.Add("Name", new[] { "The field Name is mandatory" });

                return BadRequest(obj);
            }

            return Ok();
        }

    }
}
