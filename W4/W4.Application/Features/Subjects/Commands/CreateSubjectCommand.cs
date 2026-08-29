using MediatR;
using W4.Application.DTOs;
using W4.Infrastructure.Repositories.Interfaces;
using W4.Domain.Entities;

namespace W4.Application.Features.Subjects.Commands
{
    public class CreateSubjectCommand : IRequest<ApiResponse<Subject>>
    {
        public string SubjectId { get; set; } = string.Empty;
        public string SubjectName { get; set; } = string.Empty;
    }
    public class CreateSubjectCommandHandler : IRequestHandler<CreateSubjectCommand, ApiResponse<Subject>>
    {
        private readonly ISubjectRepository _repo;
        public CreateSubjectCommandHandler(ISubjectRepository repo) => _repo = repo;
        public async Task<ApiResponse<Subject>> Handle(CreateSubjectCommand request, CancellationToken token)
        {
            var newEntity = new Subject(request.SubjectId, request.SubjectName);
            var result = await _repo.CreateAsync(newEntity);
            return new ApiResponse<Subject> { Success = true, Data = result };
        }
    }
}
