using W4.Application.DTOs;
using W4.Application.Validations;
using W4.Infrastructure.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

using W4.Domain.Entities;

using W4.Infrastructure.Repositories.Interfaces;
using W4.Application.Interfaces;
namespace W4.API.Controllers
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









