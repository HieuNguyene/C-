using Microsoft.AspNetCore.Mvc;
using W3.DTOs.Respones;
using W3.model;
using W3.DTOs.Request;
using W3.Interface;
namespace W3.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : Controller
    {
        private readonly IStudentService _service;
        public StudentController(IStudentService service)
        {
            _service = service;
        }
        [HttpGet]
        public IActionResult GetAll([FromQuery] StudentQueryRequest request)
        {
            var students = _service.GetAll(request);
            return Ok(students);
        }
        [HttpPost]
        public IActionResult Create(CreateStudentRequest request)
        {
            try
            {
                var student = _service.Create(request);
                return Created("", student);
            }
            catch
            {
                return BadRequest();
            }
        }
        [HttpGet("{id}")]
        public IActionResult GetById(Guid id)
        {
            try
            {
                var respone = _service.GetById(id);
                if (!respone.Success)
                {
                    return NotFound();
                }
                return Ok(respone);
            }
            catch
            {
                return BadRequest();
            }
            throw new Exception("Demo Exception");
        }
        [HttpPut("{id}")]
        public IActionResult Update(Guid id, UpdateStudentRequest request)
        {
            try
            {
                var student = _service.UpdateById(id, request);
                if (student.Success == false)
                {
                    return NotFound(student);
                }
                return NoContent();
            }
            catch
            {
                return BadRequest();
            }
        }
        [HttpDelete("{id}")]
        public IActionResult DeleteById(Guid id)
        {
            try
            {
                var result = _service.DeleteById(id);
                if (result.Success == false)
                {
                    return NotFound(result);
                }
                return NoContent();
            }
            catch
            {
                return BadRequest();
            }
            
        }
    }
}
