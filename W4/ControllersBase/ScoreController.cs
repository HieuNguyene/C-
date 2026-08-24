using Microsoft.AspNetCore.Mvc;
using W3.DTOs;
using W4.Service;
using W4.Interface;

namespace W4.ControllersBase
{
    [Route("api/scores")]
    [ApiController]
    public class ScoreController(IScoreService service) : ControllerBase
    {
        private readonly IScoreService _service = service;

        [HttpPost]
        public async Task<IActionResult> CreateScoreAsync(CreateScoreRequest request)
        {
            var result = await _service.CreateAsync(request);
            return Ok(result);
        }
        [HttpGet("student/{studenId}")]
        public async Task<IActionResult> GetScoreByStudent(Guid studenId)
        {
            var result = await _service.GetScoreByStudentAsync(studenId);
            return Ok(result);
        }
        [HttpGet("subject/{subjectId}")]
        public async Task<IActionResult> GetScoreBySubject(string subjectId)
        {
            var result = await _service.GetScoreBySubjectAsync(subjectId);
            return Ok(result);
        }
        [HttpPut("{scoreId}")]
        public async Task<IActionResult> UpdateAsync(Guid scoreId, UpdateScoreRequest request)
        {
            var result = await _service.UpdateAsync(scoreId,request);
            return Ok(result);
        }
        [HttpDelete("{scoreId}")]
        public async Task<IActionResult> DeleteAsync(Guid scoreId)
        {
            var result = await _service.DeleteAsync(scoreId);
            return Ok(result);
        }
    }
}