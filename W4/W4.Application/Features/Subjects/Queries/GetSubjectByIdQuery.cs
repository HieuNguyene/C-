using MediatR;
using W4.Application.DTOs;
using W4.Application.Interfaces;
using W4.Domain.Entities;

namespace W4.Application.Features.Subjects.Queries
{
    public class GetSubjectByIdQuery : IRequest<ApiResponse<Subject>>
    {
        public string SubjectId { get; set; }
        public GetSubjectByIdQuery(string id) => SubjectId = id;
    }
    public class GetSubjectByIdQueryHandler : IRequestHandler<GetSubjectByIdQuery, ApiResponse<Subject>>
    {
        private readonly ISubjectRepository _repo;
        public GetSubjectByIdQueryHandler(ISubjectRepository repo) => _repo = repo;
        public async Task<ApiResponse<Subject>> Handle(GetSubjectByIdQuery request, CancellationToken token)
        {
            var result = await _repo.GetByIdAsync(request.SubjectId);
            return new ApiResponse<Subject> { Success = result != null, Data = result! };
        }
    }
}
