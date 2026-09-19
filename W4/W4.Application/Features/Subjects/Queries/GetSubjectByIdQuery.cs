using MediatR;
using W4.Application.DTOs;
using W4.Application.Interfaces;
using W4.Domain.Entities;
using W4.Application.DTOs.Responses;
using W4.Application.Common;
using AutoMapper;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;

namespace W4.Application.Features.Subjects.Queries
{
    public class GetSubjectByIdQuery : IRequest<ApiResponse<SubjectResponse>>
    {
        public string SubjectId { get; set; }
        public GetSubjectByIdQuery(string id) => SubjectId = id;
    }

    public class GetSubjectByIdQueryHandler(
        ISubjectRepository repo,
        IMapper mapper,
        IMemoryCache cache,
        ILogger<GetSubjectByIdQueryHandler> logger) : IRequestHandler<GetSubjectByIdQuery, ApiResponse<SubjectResponse>>
    {
        public async Task<ApiResponse<SubjectResponse>> Handle(GetSubjectByIdQuery request, CancellationToken token)
        {
            var cacheKey = CacheKeys.SubjectById(request.SubjectId);
            if (cache.TryGetValue(cacheKey, out SubjectResponse? cachedSubject) && cachedSubject != null)
            {
                logger.LogInformation("[CACHE HIT] Lấy chi tiết môn học từ In-Memory Cache (Key: {CacheKey})", cacheKey);
                return new ApiResponse<SubjectResponse> { Success = true, Data = cachedSubject };
            }

            logger.LogInformation("[CACHE MISS] Chưa có cache cho Key '{CacheKey}'. Đang truy vấn từ Database...", cacheKey);
            var result = await repo.GetByIdAsync(request.SubjectId);
            if (result == null) return new ApiResponse<SubjectResponse> { Success = false, Message = "Not found" };

            var response = mapper.Map<SubjectResponse>(result);
            cache.Set(cacheKey, response, CacheKeys.DefaultOptions);
            logger.LogInformation("[CACHE SET] Đã lưu chi tiết môn học vào In-Memory Cache (Key: {CacheKey})", cacheKey);

            return new ApiResponse<SubjectResponse> { Success = true, Data = response };
        }
    }
}
