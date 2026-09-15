using Microsoft.AspNetCore.Mvc;
using MediatR;
using W4.Application.Features.Subjects.Commands;
using W4.Application.Features.Subjects.Queries;
using Microsoft.AspNetCore.Authorization;

namespace W4.API.Controllers
{
    [Authorize]
    [Route("api/subject")]
    [ApiController]
    public class SubjectController : ApiControllerBase
    {
        private readonly IMediator _mediator;
        public SubjectController(IMediator mediator) => _mediator = mediator;

        // Bất kỳ ai đã đăng nhập đều có thể xem danh sách môn học
        [HttpGet]
        public async Task<IActionResult> GetAll() => Ok(await _mediator.Send(new GetAllSubjectsQuery()));

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id) => Ok(await _mediator.Send(new GetSubjectByIdQuery(id)));

        // Chỉ Admin mới được Thêm, Sửa, Xóa môn học
        [HttpPost]
        [Authorize(Policy = "AdminOnly")]
        public async Task<IActionResult> Create([FromBody] CreateSubjectCommand command) => Ok(await _mediator.Send(command));

        [HttpPut("{id}")]
        [Authorize(Policy = "AdminOnly")]
        public async Task<IActionResult> Update(string id, [FromBody] UpdateSubjectCommand command)
        {
            command.SubjectId = id;
            return HandleResult(await _mediator.Send(command));
        }

        [HttpDelete("{id}")]
        [Authorize(Policy = "AdminOnly")]
        public async Task<IActionResult> Delete(string id) => Ok(await _mediator.Send(new DeleteSubjectCommand(id)));
    }
}

