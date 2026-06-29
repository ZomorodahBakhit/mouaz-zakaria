using AutoWrapper.Wrappers;
using Microsoft.AspNetCore.Mvc;
using University.Api.Filters;
using University.Core.Forms;
using University.Core.Services;

namespace University.API.Controllers
{
    [ApiController]
    [TypeFilter(typeof(ApiExceptionFilter))]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class StudentsController : ControllerBase
    {
        private readonly IStudentService _service;
        private readonly ILogger<StudentsController> _logger;

        public StudentsController(IStudentService studentService, ILogger<StudentsController> logger)
        {
            _service = studentService;
            _logger = logger;
           
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ApiResponse GetById(int id)
        {
            _logger.LogInformation("Incoming request: GET api/students/{StudentId}", id);
            var student = _service.GetById(id);
            return new ApiResponse(student);
        }

        [HttpGet]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ApiResponse GetAll()
        {
            _logger.LogInformation("Incoming request: GET api/students");
            return new ApiResponse(_service.GetAll());
        }

        [HttpPost]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ApiResponse Create([FromBody] CreateStudentForm form)
        {
            _logger.LogInformation("Incoming request: POST api/students");
            _service.Create(form);
            return new ApiResponse("Student created successfully");
        }

        [HttpPut("{id}")]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ApiResponse Update(int id, [FromBody] UpdateStudentForm form)
        {
            _logger.LogInformation("Incoming request: PUT api/students/{StudentId}", id);
            _service.Update(id, form);
            return new ApiResponse("Student updated successfully");
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ApiResponse Delete(int id)
        {
            _logger.LogInformation("Incoming request: DELETE api/students/{StudentId}", id);
            _service.Delete(id);
            return new ApiResponse("Student deleted successfully");
        }
    }
}
