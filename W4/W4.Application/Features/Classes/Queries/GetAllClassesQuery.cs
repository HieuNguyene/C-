using MediatR;
using W4.Application.DTOs;
using W4.Application.Interfaces;
using W4.Domain.Entities;

namespace W4.Application.Features.Classes.Queries
{
    public class GetAllClassesQuery : IRequest<ApiResponse<List<Class>>> { }
    public class GetAllClassesQueryHandler : IRequestHandler<GetAllClassesQuery, ApiResponse<List<Class>>>
    {
        private readonly IClassRepository _repo;
        public GetAllClassesQueryHandler(IClassRepository repo) => _repo = repo;
        public async Task<ApiResponse<List<Class>>> Handle(GetAllClassesQuery request, CancellationToken token)
        {
            var result = await _repo.GetAllClassAsync();
            return new ApiResponse<List<Class>> { Success = true, Data = result };
        }
    }
}
