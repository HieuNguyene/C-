using MediatR;
using W4.Application.DTOs;
using W4.Infrastructure.Repositories.Interfaces;
using W4.Domain.Entities;

namespace W4.Application.Features.Subjects.Queries
{
    public class GetAllSubjectsQuery : IRequest<ApiResponse<List<Subject>>> {}
    public class GetAllSubjectsQueryHandler : IRequestHandler<GetAllSubjectsQuery, ApiResponse<List<Subject>>>
    {
        private readonly ISubjectRepository _repo;
        public GetAllSubjectsQueryHandler(ISubjectRepository repo) => _repo = repo;
        public async Task<ApiResponse<List<Subject>>> Handle(GetAllSubjectsQuery request, CancellationToken token)
        {
            var result = await _repo.GetAllAsync();
            return new ApiResponse<List<Subject>> { Success = true, Data = result };
        }
    }
}
