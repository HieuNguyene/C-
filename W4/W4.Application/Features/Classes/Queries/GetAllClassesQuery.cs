using MediatR;
using W4.Application.DTOs;
using W4.Application.Interfaces;
using W4.Domain.Entities;
using W4.Application.DTOs.Responses;
using W4.Application.Common;
using AutoMapper;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Caching.Distributed;

namespace W4.Application.Features.Classes.Queries
{
    public class GetAllClassesQuery : IRequest<ApiResponse<List<ClassResponse>>> { }

    public class GetAllClassesQueryHandler(
        IClassRepository repo,
        IMapper mapper,
        IDistributedCache cache,
        ILogger<GetAllClassesQueryHandler> logger) : IRequestHandler<GetAllClassesQuery, ApiResponse<List<ClassResponse>>>
    {
        public async Task<ApiResponse<List<ClassResponse>>> Handle(GetAllClassesQuery request, CancellationToken token)
        {
            var cachedClasses = await cache.GetRecordAsync<List<ClassResponse>>(CacheKeys.ClassesAll, token);
            if (cachedClasses != null)
            {
                logger.LogInformation("[REDIS HIT] Lấy danh sách lớp học từ Redis");
                return new ApiResponse<List<ClassResponse>> { Success = true, Data = cachedClasses };
            }

            logger.LogInformation("[REDIS MISS] Chưa có cache cho Key '{CacheKey}'. Đang truy vấn từ Database...", CacheKeys.ClassesAll);
            var result = await repo.GetAllClassAsync();
            var response = mapper.Map<List<ClassResponse>>(result);

            await cache.SetRecordAsync(CacheKeys.ClassesAll, response, CacheKeys.DefaultAbsoluteExpiration, CacheKeys.DefaultSlidingExpiration, token);
            logger.LogInformation("[REDIS SET] Đã lưu danh sách lớp học vào Redis (Key: {CacheKey})", CacheKeys.ClassesAll);

            return new ApiResponse<List<ClassResponse>> { Success = true, Data = response };
        }
    }
}
