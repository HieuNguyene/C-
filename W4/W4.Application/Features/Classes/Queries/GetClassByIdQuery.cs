using MediatR;
using W4.Application.DTOs;
using W4.Application.Interfaces;
using W4.Application.DTOs.Responses;
using W4.Application.Common;
using AutoMapper;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Caching.Distributed;

namespace W4.Application.Features.Classes.Queries
{
    public class GetClassByIdQuery : IRequest<ApiResponse<ClassResponse>>
    {
        public string ClassId { get; set; }
        public GetClassByIdQuery(string id) => ClassId = id;
    }

    public class GetClassByIdQueryHandler(
        IClassRepository repo,
        IMapper mapper,
        IDistributedCache cache,
        ILogger<GetClassByIdQueryHandler> logger) : IRequestHandler<GetClassByIdQuery, ApiResponse<ClassResponse>>
    {
        public async Task<ApiResponse<ClassResponse>> Handle(GetClassByIdQuery request, CancellationToken token)
        {
            var cacheKey = CacheKeys.ClassById(request.ClassId);
            var cachedClass = await cache.GetRecordAsync<ClassResponse>(cacheKey, token);
            if (cachedClass != null)
            {
                logger.LogInformation("[REDIS HIT] Lấy chi tiết lớp học từ Redis (Key: {CacheKey})", cacheKey);
                return new ApiResponse<ClassResponse> { Success = true, Data = cachedClass };
            }

            logger.LogInformation("[REDIS MISS] Chưa có cache cho Key '{CacheKey}'. Đang truy vấn từ Database...", cacheKey);
            var result = await repo.GetByIdAsync(request.ClassId);
            if (result == null) return new ApiResponse<ClassResponse> { Success = false, Message = "Not found" };

            var response = mapper.Map<ClassResponse>(result);
            await cache.SetRecordAsync<ClassResponse>(cacheKey, response, CacheKeys.DefaultAbsoluteExpiration, CacheKeys.DefaultSlidingExpiration, token);
            logger.LogInformation("[REDIS SET] Đã lưu chi tiết lớp học vào Redis (Key: {CacheKey})", cacheKey);

            return new ApiResponse<ClassResponse> { Success = true, Data = response };
        }
    }
}
