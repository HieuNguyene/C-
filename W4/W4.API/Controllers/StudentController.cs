using W4.Application.DTOs;
using W4.Application.Validations;
using W4.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

using W4.Domain.Entities;



using W4.Application.Features.Students.Commands;
using W4.Application.Features.Students.Queries;
namespace W4.API.Controllers
{
    [Route("api/student")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly MediatR.IMediator _mediator;
        public StudentController(MediatR.IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpGet("search")]
        public async Task<IActionResult> GetByKeyWordAsync([FromQuery] GetStudentByKeyWordQuery query)
        {
            var response = await _mediator.Send(query);
            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] CreateStudentCommand command)
        {
            var student = await _mediator.Send(command);
            return Ok(student);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(Guid id)
        {
            var query = new GetStudentByIdQuery(id);
            var result = await _mediator.Send(query);
            return Ok(result);

        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(Guid id, [FromBody] UpdateStudentCommand command)
        {
            command.Id = id;
            var result = await _mediator.Send(command);
            return Ok(result);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteById(Guid id)
        {
            var command = new DeleteStudentCommand(id);
            var result = await _mediator.Send(command);
            return Ok(result);
        }
    }
}









