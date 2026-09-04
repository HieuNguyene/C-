using MediatR;
using W4.Application.DTOs;
using W4.Application.Interfaces;
using W4.Domain.Entities;
using W4.Application.DTOs.Responses;

namespace W4.Application.Features.Subjects.Queries
{
    public class GetAllSubjectsQuery : IRequest<ApiResponse<List<SubjectResponse>>> { }
    public class GetAllSubjectsQueryHandler : IRequestHandler<GetAllSubjectsQuery, ApiResponse<List<SubjectResponse>>>
    {
        private readonly ISubjectRepository _repo;
        public GetAllSubjectsQueryHandler(ISubjectRepository repo) => _repo = repo;
        public async Task<ApiResponse<List<SubjectResponse>>> Handle(GetAllSubjectsQuery request, CancellationToken token)
        {
            var result = await _repo.GetAllAsync();
            var response = result.Select(x => new SubjectResponse { SubjectId = x.SubjectId, SubjectName = x.SubjectName }).ToList();
            return new ApiResponse<List<SubjectResponse>> { Success = true, Data = response };
        }
    }
}
