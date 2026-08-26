using W4.Application.DTOs;
using W4.Application.Validations;
using W4.Infrastructure.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using W4.Application.DTOs;
using W4.Infrastructure.Repositories.Interfaces;
using W4.Application.Interfaces;

namespace W4.API.Controllers
{
    [Route("api/subjects")]
    [ApiController]
    public class SubjectController : ControllerBase
    {
        private readonly ISubjectService _service;

        public SubjectController(ISubjectService service)
        {
            _service = service;
        }


        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var result = await _service.GetAllAsync();
            return Ok(result);
        }


        [HttpGet("{subjectId}")]
        public async Task<IActionResult> GetByIdAsync(string subjectId)
        {
            var result = await _service.GetByIdAsync(subjectId);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] CreateSubjectRequest request)
        {
            var result = await _service.CreateAsync(request);
            return Ok(result);
        }


        [HttpPut("{subjectId}")]
        public async Task<IActionResult> UpdateAsync(string subjectId, [FromBody] UpdateSubjectRequest request)
        {
            var result = await _service.UpdateAsync(subjectId, request);
            return Ok(result);
        }

        [HttpDelete("{subjectId}")]
        public async Task<IActionResult> DeleteAsync(string subjectId)
        {
            var result = await _service.DeleteAsync(subjectId);
            return Ok(result);
        }
    }
}









