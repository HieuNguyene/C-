using Microsoft.AspNetCore.Mvc;
using MediatR;
using System;
using W4.Application.Features.Scores.Commands;
using W4.Application.Features.Scores.Queries;

namespace W4.API.Controllers
{
    [Route("api/score")]
    [ApiController]
    public class ScoreController : ControllerBase
    {
        private readonly IMediator _mediator;
        public ScoreController(IMediator mediator) => _mediator = mediator;

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id) => Ok(await _mediator.Send(new GetScoreByIdQuery(id)));

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateScoreCommand command) => Ok(await _mediator.Send(command));

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateScoreCommand command)
        {
            command.Id = id;
            return Ok(await _mediator.Send(command));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id) => Ok(await _mediator.Send(new DeleteScoreCommand(id)));
    }
}
