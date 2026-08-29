using Microsoft.AspNetCore.Mvc;
using MediatR;
using W4.Application.Features.Classes.Commands;
using W4.Application.Features.Classes.Queries;

namespace W4.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClassController : ControllerBase
    {
        private readonly IMediator _mediator;
        public ClassController(IMediator mediator) => _mediator = mediator;

        [HttpGet]
        public async Task<IActionResult> GetAll() => Ok(await _mediator.Send(new GetAllClassesQuery()));

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id) => Ok(await _mediator.Send(new GetClassByIdQuery(id)));

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateClassCommand command) => Ok(await _mediator.Send(command));

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, [FromBody] UpdateClassCommand command)
        {
            command.ClassId = id;
            return Ok(await _mediator.Send(command));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id) => Ok(await _mediator.Send(new DeleteClassCommand(id)));
    }
}
