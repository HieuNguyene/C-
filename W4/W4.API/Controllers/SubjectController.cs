using Microsoft.AspNetCore.Mvc;
using MediatR;
using W4.Application.Features.Subjects.Commands;
using W4.Application.Features.Subjects.Queries;

namespace W4.API.Controllers
{
    [Route("api/subject")]
    [ApiController]
    public class SubjectController : ControllerBase
    {
        private readonly IMediator _mediator;
        public SubjectController(IMediator mediator) => _mediator = mediator;

        [HttpGet]
        public async Task<IActionResult> GetAll() => Ok(await _mediator.Send(new GetAllSubjectsQuery()));

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id) => Ok(await _mediator.Send(new GetSubjectByIdQuery(id)));

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateSubjectCommand command) => Ok(await _mediator.Send(command));

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, [FromBody] UpdateSubjectCommand command)
        {
            command.SubjectId = id;
            return Ok(await _mediator.Send(command));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id) => Ok(await _mediator.Send(new DeleteSubjectCommand(id)));
    }
}
