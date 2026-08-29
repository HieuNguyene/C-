using MediatR;
using W4.Application.DTOs;
using W4.Infrastructure.Repositories.Interfaces;
using W4.Domain.Entities;

namespace W4.Application.Features.Classes.Commands
{
    public class CreateClassCommand : IRequest<ApiResponse<Class>>
    {
        public string ClassId { get; set; } = string.Empty;
        public string ClassName { get; set; } = string.Empty;
    }
    public class CreateClassCommandHandler : IRequestHandler<CreateClassCommand, ApiResponse<Class>>
    {
        private readonly IClassRepository _repo;
        public CreateClassCommandHandler(IClassRepository repo) => _repo = repo;
        public async Task<ApiResponse<Class>> Handle(CreateClassCommand request, CancellationToken token)
        {
            var newClass = new Class(request.ClassId, request.ClassName);
            var result = await _repo.CreateAsync(newClass);
            return new ApiResponse<Class> { Success = true, Data = result };
        }
    }
}
