using MediatR;
using W4.Application.DTOs;
using W4.Application.Interfaces;
using W4.Application.Common;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Caching.Distributed;

namespace W4.Application.Features.Classes.Commands
{
    public class UpdateClassCommand : IRequest<ApiResponse<bool>>
    {
        public string ClassId { get; set; } = string.Empty;
        public string ClassName { get; set; } = string.Empty;
    }

    public class UpdateClassCommandHandler(
        IClassRepository repo,
        IDistributedCache cache,
        ILogger<UpdateClassCommandHandler> logger) : IRequestHandler<UpdateClassCommand, ApiResponse<bool>>
    {
        public async Task<ApiResponse<bool>> Handle(UpdateClassCommand request, CancellationToken token)
        {
            var existing = await repo.GetByIdAsync(request.ClassId);
            if (existing == null) return new ApiResponse<bool> { Success = false };

            existing.UpdateClassName(request.ClassName);
            await repo.UpdateAsync(existing);

            // Invalidate Redis
            await cache.RemoveRecordAsync(CacheKeys.ClassesAll);
            await cache.RemoveRecordAsync(CacheKeys.ClassById(request.ClassId));
            logger.LogInformation("[CACHE EVICT] Đã xóa cache '{KeyAll}' và '{KeyById}' do cập nhật lớp học",
                CacheKeys.ClassesAll, CacheKeys.ClassById(request.ClassId));

            return new ApiResponse<bool> { Success = true, Data = true };
        }
    }
}
