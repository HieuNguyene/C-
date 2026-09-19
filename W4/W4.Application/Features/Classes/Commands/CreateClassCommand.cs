using MediatR;
using W4.Application.DTOs;
using W4.Application.Interfaces;
using W4.Domain.Entities;
using W4.Application.DTOs.Responses;
using W4.Application.Common;
using AutoMapper;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Caching.Distributed;

namespace W4.Application.Features.Classes.Commands
{
    public class CreateClassCommand : IRequest<ApiResponse<ClassResponse>>
    {
        public string ClassId { get; set; } = string.Empty;
        public string ClassName { get; set; } = string.Empty;
    }

    public class CreateClassCommandHandler(
        IClassRepository repo,
        IMapper mapper,
        IDistributedCache cache,
        ILogger<CreateClassCommandHandler> logger) : IRequestHandler<CreateClassCommand, ApiResponse<ClassResponse>>
    {
        public async Task<ApiResponse<ClassResponse>> Handle(CreateClassCommand request, CancellationToken token)
        {
            var newClass = new Class(request.ClassId, request.ClassName);
            var result = await repo.CreateAsync(newClass);
            
            // Invalidate Redis
            await cache.RemoveRecordAsync(CacheKeys.ClassesAll,token);
            logger.LogInformation("[CACHE EVICT] Đã xóa cache '{CacheKey}' trên Redis do thêm lớp học mới", CacheKeys.ClassesAll);

            var response = mapper.Map<ClassResponse>(result);
            return new ApiResponse<ClassResponse> { Success = true, Data = response };
        }
    }
}
