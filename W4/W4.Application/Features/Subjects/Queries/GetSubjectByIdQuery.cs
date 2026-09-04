using MediatR;
using W4.Application.DTOs;
using W4.Application.Interfaces;
using W4.Domain.Entities;
using W4.Application.DTOs.Responses;

namespace W4.Application.Features.Subjects.Queries
{
    public class GetSubjectByIdQuery : IRequest<ApiResponse<SubjectResponse>>
    {
        public string SubjectId { get; set; }
        public GetSubjectByIdQuery(string id) => SubjectId = id;
    }
    public class GetSubjectByIdQueryHandler : IRequestHandler<GetSubjectByIdQuery, ApiResponse<SubjectResponse>>
    {
        private readonly ISubjectRepository _repo;
        public GetSubjectByIdQueryHandler(ISubjectRepository repo) => _repo = repo;
        public async Task<ApiResponse<SubjectResponse>> Handle(GetSubjectByIdQuery request, CancellationToken token)
        {
            var result = await _repo.GetByIdAsync(request.SubjectId);
            if (result == null) return new ApiResponse<SubjectResponse> { Success = false, Message = "Not found" };
            var response = new SubjectResponse { SubjectId = result.SubjectId, SubjectName = result.SubjectName };
            return new ApiResponse<SubjectResponse> { Success = true, Data = response };
        }
    }
}
