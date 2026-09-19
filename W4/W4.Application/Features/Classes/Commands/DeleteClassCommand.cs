using MediatR;
using W4.Application.DTOs;
using W4.Application.Interfaces;
using W4.Application.Common;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Caching.Distributed;

namespace W4.Application.Features.Classes.Commands
{
    public class DeleteClassCommand : IRequest<ApiResponse<bool>>
    {
        public string ClassId { get; set; }
        public DeleteClassCommand(string id) => ClassId = id;
    }

    public class DeleteClassCommandHandler(
        IClassRepository repo,
        IDistributedCache cache,
        ILogger<DeleteClassCommandHandler> logger) : IRequestHandler<DeleteClassCommand, ApiResponse<bool>>
    {
        public async Task<ApiResponse<bool>> Handle(DeleteClassCommand request, CancellationToken token)
        {
            var result = await repo.DeleteByIdAsync(request.ClassId);
            if (result)
            {
                // Invalidate Redis
                await cache.RemoveRecordAsync(CacheKeys.ClassesAll);
                await cache.RemoveRecordAsync(CacheKeys.ClassById(request.ClassId));
                logger.LogInformation("[CACHE EVICT] Đã xóa cache '{KeyAll}' và '{KeyById}' do xóa lớp học",
                CacheKeys.ClassesAll, CacheKeys.ClassById(request.ClassId));
                return ApiResponse<bool>.Ok(true,"Xóa lớp học thành công");
            }
            return ApiResponse<bool>.Fail($"Không tìm thấy lớp học với mã '{request.ClassId}' để xóa.")  ;
        }
    }
}
