using MediatR;
using W4.Application.DTOs;
using W4.Application.Interfaces;
using W4.Domain.Entities;
using W4.Application.DTOs.Responses;
using W4.Application.Common;
using AutoMapper;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;

namespace W4.Application.Features.Subjects.Commands
{
    public class CreateSubjectCommand : IRequest<ApiResponse<SubjectResponse>>
    {
        public string SubjectId { get; set; } = string.Empty;
        public string SubjectName { get; set; } = string.Empty;
    }

    public class CreateSubjectCommandHandler(
        ISubjectRepository repo,
        IMapper mapper,
        IMemoryCache cache,
        ILogger<CreateSubjectCommandHandler> logger) : IRequestHandler<CreateSubjectCommand, ApiResponse<SubjectResponse>>
    {
        public async Task<ApiResponse<SubjectResponse>> Handle(CreateSubjectCommand request, CancellationToken token)
        {
            var newEntity = new Subject(request.SubjectId, request.SubjectName);
            var result = await repo.CreateAsync(newEntity);

            // Invalidate Cache
            cache.Remove(CacheKeys.SubjectsAll);
            logger.LogInformation("[CACHE EVICT] Đã xóa cache '{CacheKey}' do thêm môn học mới", CacheKeys.SubjectsAll);

            var response = mapper.Map<SubjectResponse>(result);
            return new ApiResponse<SubjectResponse> { Success = true, Data = response };
        }
    }
}
