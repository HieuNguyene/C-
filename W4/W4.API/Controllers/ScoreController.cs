using Microsoft.AspNetCore.Mvc;
using MediatR;
using System;
using W4.Application.Features.Scores.Commands;
using W4.Application.Features.Scores.Queries;
using Microsoft.AspNetCore.Authorization;

namespace W4.API.Controllers
{
    [Authorize]
    [Route("api/score")]
    [ApiController]
    public class ScoreController : ApiControllerBase
    {
        private readonly IMediator _mediator;
        public ScoreController(IMediator mediator) => _mediator = mediator;

        // Bất kỳ ai đã đăng nhập đều có thể xem điểm
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id) => Ok(await _mediator.Send(new GetScoreByIdQuery(id)));

        // Admin hoặc Teacher được quyền Nhập điểm và Sửa điểm
        [HttpPost]
        [Authorize(Policy = "CanManageStudents")]
        public async Task<IActionResult> Create([FromBody] CreateScoreCommand command) => Ok(await _mediator.Send(command));

        [HttpPut("{id}")]
        [Authorize(Policy = "CanManageStudents")]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateScoreCommand command)
        {
            command.Id = id;
            return HandleResult(await _mediator.Send(command));
        }

        // Chỉ Admin mới được Xóa điểm
        [HttpDelete("{id}")]
        [Authorize(Policy = "AdminOnly")]
        public async Task<IActionResult> Delete(Guid id) => Ok(await _mediator.Send(new DeleteScoreCommand(id)));
    }
}

