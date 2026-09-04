using Microsoft.AspNetCore.Mvc;
using W4.Application.DTOs;

namespace W4.API.Controllers
{
    [ApiController]
    public abstract class ApiControllerBase : ControllerBase
    {
        protected IActionResult HandleResult<T>(ApiResponse<T> result)
        {
            if (result.Success)
            {
                return Ok(result);
            }

            if (result.Message.Contains("Not found") || result.Message.Contains("không tồn tại") || result.Message.Contains("Not found Student"))
            {
                return NotFound(result);
            }

            return BadRequest(result);
        }
    }
}
