using MediatR;
using W4.Application.DTOs;
using W4.Application.Interfaces;
using W4.Domain.Entities;
using W4.Application.DTOs.Responses;

namespace W4.Application.Features.Classes.Commands
{
    public class CreateClassCommand : IRequest<ApiResponse<ClassResponse>>
    {
        public string ClassId { get; set; } = string.Empty;
        public string ClassName { get; set; } = string.Empty;
    }
    public class CreateClassCommandHandler : IRequestHandler<CreateClassCommand, ApiResponse<ClassResponse>>
    {
        private readonly IClassRepository _repo;
        public CreateClassCommandHandler(IClassRepository repo) => _repo = repo;
        public async Task<ApiResponse<ClassResponse>> Handle(CreateClassCommand request, CancellationToken token)
        {
            var newClass = new Class(request.ClassId, request.ClassName);
            var result = await _repo.CreateAsync(newClass);
            
            var response = new ClassResponse { ClassId = result.ClassId, ClassName = result.ClassName };
            return new ApiResponse<ClassResponse> { Success = true, Data = response };
        }
    }
}
