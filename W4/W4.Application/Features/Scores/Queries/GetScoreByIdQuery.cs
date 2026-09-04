using MediatR;
using System;
using W4.Application.DTOs;
using W4.Application.Interfaces;
using W4.Domain.Entities;
using W4.Application.DTOs.Responses;

namespace W4.Application.Features.Scores.Queries
{
    public class GetScoreByIdQuery : IRequest<ApiResponse<ScoreResponse>>
    {
        public Guid Id { get; set; }
        public GetScoreByIdQuery(Guid id) => Id = id;
    }
    public class GetScoreByIdQueryHandler : IRequestHandler<GetScoreByIdQuery, ApiResponse<ScoreResponse>>
    {
        private readonly IScoreRepository _repo;
        public GetScoreByIdQueryHandler(IScoreRepository repo) => _repo = repo;
        public async Task<ApiResponse<ScoreResponse>> Handle(GetScoreByIdQuery request, CancellationToken token)
        {
            var result = await _repo.GetByIdAsync(request.Id);
            if (result == null) return new ApiResponse<ScoreResponse> { Success = false, Message = "Not found" };
            var response = new ScoreResponse { Id = result.Id, Value = result.Value, StudentId = result.StudentId, SubjectId = result.SubjectId };
            return new ApiResponse<ScoreResponse> { Success = true, Data = response };
        }
    }
}
