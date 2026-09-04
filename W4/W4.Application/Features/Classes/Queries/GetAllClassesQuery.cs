using MediatR;
using W4.Application.DTOs;
using W4.Application.Interfaces;
using W4.Domain.Entities;
using W4.Application.DTOs.Responses;

namespace W4.Application.Features.Classes.Queries
{
    public class GetAllClassesQuery : IRequest<ApiResponse<List<ClassResponse>>> { }
    public class GetAllClassesQueryHandler : IRequestHandler<GetAllClassesQuery, ApiResponse<List<ClassResponse>>>
    {
        private readonly IClassRepository _repo;
        public GetAllClassesQueryHandler(IClassRepository repo) => _repo = repo;
        public async Task<ApiResponse<List<ClassResponse>>> Handle(GetAllClassesQuery request, CancellationToken token)
        {
            var result = await _repo.GetAllClassAsync();
            var response = result.Select(c => new ClassResponse { ClassId = c.ClassId, ClassName = c.ClassName }).ToList();
            return new ApiResponse<List<ClassResponse>> { Success = true, Data = response };
        }
    }
}
