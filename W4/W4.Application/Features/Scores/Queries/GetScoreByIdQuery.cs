using MediatR;
using System;
using W4.Application.DTOs;
using W4.Application.Interfaces;
using W4.Domain.Entities;
using W4.Application.DTOs.Responses;

using AutoMapper;

namespace W4.Application.Features.Scores.Queries
{
    public class GetScoreByIdQuery : IRequest<ApiResponse<ScoreResponse>>
    {
        public Guid Id { get; set; }
        public GetScoreByIdQuery(Guid id) => Id = id;
    }
    public class GetScoreByIdQueryHandler(IScoreRepository repo, IMapper mapper) : IRequestHandler<GetScoreByIdQuery, ApiResponse<ScoreResponse>>
    {
        public async Task<ApiResponse<ScoreResponse>> Handle(GetScoreByIdQuery request, CancellationToken token)
        {
            var result = await repo.GetByIdAsync(request.Id);
            if (result == null) return new ApiResponse<ScoreResponse> { Success = false, Message = "Not found" };
            var response = mapper.Map<ScoreResponse>(result);
            return new ApiResponse<ScoreResponse> { Success = true, Data = response };
        }
    }
}
