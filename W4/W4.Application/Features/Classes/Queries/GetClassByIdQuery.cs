using MediatR;
using W4.Application.DTOs;
using W4.Infrastructure.Repositories.Interfaces;
using W4.Domain.Entities;

namespace W4.Application.Features.Classes.Queries
{
    public class GetClassByIdQuery : IRequest<ApiResponse<Class>>
    {
        public string ClassId { get; set; }
        public GetClassByIdQuery(string id) => ClassId = id;
    }
    public class GetClassByIdQueryHandler : IRequestHandler<GetClassByIdQuery, ApiResponse<Class>>
    {
        private readonly IClassRepository _repo;
        public GetClassByIdQueryHandler(IClassRepository repo) => _repo = repo;
        public async Task<ApiResponse<Class>> Handle(GetClassByIdQuery request, CancellationToken token)
        {
            var result = await _repo.GetByIdAsync(request.ClassId);
            return new ApiResponse<Class> { Success = result != null, Data = result! };
        }
    }
}
