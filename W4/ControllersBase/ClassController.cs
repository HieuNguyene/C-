using Microsoft.AspNetCore.Mvc;
using W3.DTOs;
using W4.Interface;

namespace W3.Controllers
{
    [Route("api/classes")]
    [ApiController]
    public class ClassController : ControllerBase
    {
        private readonly IClassService _service;
        public ClassController(IClassService service)
        {
            _service = service;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var result = await _service.GetAllAsync();
            return Ok(result);
        }
        [HttpPost]
        public async Task<IActionResult> CreateAsync(CreateClassRequest request)
        {
            var result = await _service.CreateAsync(request);
            return Ok(result);
        }
        [HttpPut]
        public async Task<IActionResult> UpdateAsync(string classId, CreateClassRequest request)
        {
            var result = await _service.UpdateAsync(classId, request);
            return Ok(result);
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteAsync(string classId)
        {
            var result = await _service.DeleteAsync(classId);
            return Ok(result);
        }
    }
}