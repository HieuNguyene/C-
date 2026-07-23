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
        public IActionResult GetAll()
        {
            var students = _service.GetAll();
            return Ok(students);
        }
        [HttpPost]
        public IActionResult Create(CreateStudentRequest request)
        {
            var student = _service.Create(request);
            return Created("", student);
        }
        [HttpGet("{id}")]
        public IActionResult GetById(string id)
        {
            try
            {
                var student= _service.GetById(id);
                return Ok(student);
            }
            catch
            {
                return BadRequest();
            }
        }
        [HttpPut("{id}")]
        public IActionResult Update(string id, UpdateStudentRequest request)
        {
            try
            {
                var student = _service.UpdateById(id, request);
                if (student != null) 
                {
                    return NotFound();
                }
                return Ok(student);
            }
            catch
            {
                return BadRequest();
            }
        }
        [HttpDelete]
        public IActionResult DeleteById(string id)
        {
            try
            {
                var result = _service.DeleteById(id);
                if (!result)
                {
                    return NotFound();
                }
                return Ok();
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}
