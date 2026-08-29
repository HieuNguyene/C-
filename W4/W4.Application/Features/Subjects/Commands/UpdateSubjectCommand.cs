using MediatR;
using W4.Application.DTOs;
using W4.Application.Interfaces;

namespace W4.Application.Features.Subjects.Commands
{
    public class UpdateSubjectCommand : IRequest<ApiResponse<bool>>
    {
        public string SubjectId { get; set; } = string.Empty;
        public string SubjectName { get; set; } = string.Empty;
    }
    public class UpdateSubjectCommandHandler : IRequestHandler<UpdateSubjectCommand, ApiResponse<bool>>
    {
        private readonly ISubjectRepository _repo;
        public UpdateSubjectCommandHandler(ISubjectRepository repo) => _repo = repo;
        public async Task<ApiResponse<bool>> Handle(UpdateSubjectCommand request, CancellationToken token)
        {
            var existing = await _repo.GetByIdAsync(request.SubjectId);
            if (existing == null) return new ApiResponse<bool> { Success = false };
            existing.UpdateSubjectName(request.SubjectName);
            await _repo.UpdateAsync(existing);
            return new ApiResponse<bool> { Success = true, Data = true };
        }
    }
}
