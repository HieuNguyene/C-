using MediatR;
using W4.Application.DTOs;
using W4.Application.Interfaces;
using W4.Domain.Entities;
using W4.Application.DTOs.Responses;

namespace W4.Application.Features.Classes.Queries
{
    public class GetClassByIdQuery : IRequest<ApiResponse<ClassResponse>>
    {
        public string ClassId { get; set; }
        public GetClassByIdQuery(string id) => ClassId = id;
    }
    public class GetClassByIdQueryHandler : IRequestHandler<GetClassByIdQuery, ApiResponse<ClassResponse>>
    {
        private readonly IClassRepository _repo;
        public GetClassByIdQueryHandler(IClassRepository repo) => _repo = repo;
        public async Task<ApiResponse<ClassResponse>> Handle(GetClassByIdQuery request, CancellationToken token)
        {
            var result = await _repo.GetByIdAsync(request.ClassId);
            if (result == null) return new ApiResponse<ClassResponse> { Success = false, Message = "Not found" };
            
            var response = new ClassResponse { ClassId = result.ClassId, ClassName = result.ClassName };
            return new ApiResponse<ClassResponse> { Success = true, Data = response };
        }
    }
}
