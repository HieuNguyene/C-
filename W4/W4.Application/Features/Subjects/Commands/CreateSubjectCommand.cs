using MediatR;
using W4.Application.DTOs;
using W4.Application.Interfaces;
using W4.Domain.Entities;
using W4.Application.DTOs.Responses;

namespace W4.Application.Features.Subjects.Commands
{
    public class CreateSubjectCommand : IRequest<ApiResponse<SubjectResponse>>
    {
        public string SubjectId { get; set; } = string.Empty;
        public string SubjectName { get; set; } = string.Empty;
    }
    public class CreateSubjectCommandHandler : IRequestHandler<CreateSubjectCommand, ApiResponse<SubjectResponse>>
    {
        private readonly ISubjectRepository _repo;
        public CreateSubjectCommandHandler(ISubjectRepository repo) => _repo = repo;
        public async Task<ApiResponse<SubjectResponse>> Handle(CreateSubjectCommand request, CancellationToken token)
        {
            var newEntity = new Subject(request.SubjectId, request.SubjectName);
            var result = await _repo.CreateAsync(newEntity);
            var response = new SubjectResponse { SubjectId = result.SubjectId, SubjectName = result.SubjectName };
            return new ApiResponse<SubjectResponse> { Success = true, Data = response };
        }
    }
}
