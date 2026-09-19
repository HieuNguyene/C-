using MediatR;
using W4.Application.DTOs;
using W4.Application.Interfaces;
using W4.Application.Common;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;

namespace W4.Application.Features.Subjects.Commands
{
    public class UpdateSubjectCommand : IRequest<ApiResponse<bool>>
    {
        public string SubjectId { get; set; } = string.Empty;
        public string SubjectName { get; set; } = string.Empty;
    }

    public class UpdateSubjectCommandHandler(
        ISubjectRepository repo,
        IMemoryCache cache,
        ILogger<UpdateSubjectCommandHandler> logger) : IRequestHandler<UpdateSubjectCommand, ApiResponse<bool>>
    {
        public async Task<ApiResponse<bool>> Handle(UpdateSubjectCommand request, CancellationToken token)
        {
            var existing = await repo.GetByIdAsync(request.SubjectId);
            if (existing == null) return new ApiResponse<bool> { Success = false };

            existing.UpdateSubjectName(request.SubjectName);
            await repo.UpdateAsync(existing);

            // Invalidate Caches
            cache.Remove(CacheKeys.SubjectsAll);
            cache.Remove(CacheKeys.SubjectById(request.SubjectId));
            logger.LogInformation("[CACHE EVICT] Đã xóa cache '{KeyAll}' và '{KeyById}' do cập nhật môn học",
                CacheKeys.SubjectsAll, CacheKeys.SubjectById(request.SubjectId));

            return new ApiResponse<bool> { Success = true, Data = true };
        }
    }
}
