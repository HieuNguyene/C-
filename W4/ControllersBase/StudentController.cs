using Microsoft.AspNetCore.Mvc;
using W3.DTOs.Respones;
using W3.model;
using W3.DTOs.Request;
using W3.Interface;
using W3.Responses;
namespace W3.Controllers
{
    [Route("api/students")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly IStudentService _service;
        public StudentController(IStudentService service)
        {
            _service = service;
        }
        [HttpGet("search")]
        public async Task<IActionResult> GetByKeyWordAsync([FromQuery] StudentQueryRequest request)
        {
            var response = await _service.GetByKeyWordAsync(request);
            return Ok(response);
        }
        [HttpPost]
        public async Task<IActionResult> CreateAsync(CreateStudentRequest request)
        {

            var student = await _service.CreateAsync(request);
            return Ok(student);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(Guid id)
        {
            var result = await _service.GetByIdAsync(id);
            return Ok(result);

        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(Guid id, UpdateStudentRequest request)
        {
            var result = await _service.UpdateByIdAsync(id, request);
            return Ok(result);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteById(Guid id)
        {

            var result = await _service.DeleteByIdAsync(id);
            return Ok(result);
        }
    }
}
