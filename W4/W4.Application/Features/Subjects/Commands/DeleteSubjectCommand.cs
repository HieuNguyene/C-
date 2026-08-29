using MediatR;
using W4.Application.DTOs;
using W4.Application.Interfaces;

namespace W4.Application.Features.Subjects.Commands
{
    public class DeleteSubjectCommand : IRequest<ApiResponse<bool>>
    {
        public string SubjectId { get; set; }
        public DeleteSubjectCommand(string id) => SubjectId = id;
    }
    public class DeleteSubjectCommandHandler : IRequestHandler<DeleteSubjectCommand, ApiResponse<bool>>
    {
        private readonly ISubjectRepository _repo;
        public DeleteSubjectCommandHandler(ISubjectRepository repo) => _repo = repo;
        public async Task<ApiResponse<bool>> Handle(DeleteSubjectCommand request, CancellationToken token)
        {
            var result = await _repo.DeleteAsync(request.SubjectId);
            return new ApiResponse<bool> { Success = result, Data = result };
        }
    }
}
