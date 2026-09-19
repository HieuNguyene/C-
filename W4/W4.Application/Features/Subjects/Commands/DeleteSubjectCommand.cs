using MediatR;
using W4.Application.DTOs;
using W4.Application.Interfaces;
using W4.Application.Common;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;

namespace W4.Application.Features.Subjects.Commands
{
    public class DeleteSubjectCommand : IRequest<ApiResponse<bool>>
    {
        public string SubjectId { get; set; }
        public DeleteSubjectCommand(string id) => SubjectId = id;
    }

    public class DeleteSubjectCommandHandler(
        ISubjectRepository repo,
        IMemoryCache cache,
        ILogger<DeleteSubjectCommandHandler> logger) : IRequestHandler<DeleteSubjectCommand, ApiResponse<bool>>
    {
        public async Task<ApiResponse<bool>> Handle(DeleteSubjectCommand request, CancellationToken token)
        {
            var result = await repo.DeleteAsync(request.SubjectId);
            if (result)
            {
                // Invalidate Caches
                cache.Remove(CacheKeys.SubjectsAll);
                cache.Remove(CacheKeys.SubjectById(request.SubjectId));
                logger.LogInformation("[CACHE EVICT] Đã xóa cache '{KeyAll}' và '{KeyById}' do xóa môn học",
                    CacheKeys.SubjectsAll, CacheKeys.SubjectById(request.SubjectId));
            }
            return new ApiResponse<bool> { Success = result, Data = result };
        }
    }
}
