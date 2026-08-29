using MediatR;
using System;
using W4.Application.DTOs;
using W4.Application.Interfaces;
using W4.Domain.Entities;

namespace W4.Application.Features.Scores.Queries
{
    public class GetScoreByIdQuery : IRequest<ApiResponse<Score>>
    {
        public Guid Id { get; set; }
        public GetScoreByIdQuery(Guid id) => Id = id;
    }
    public class GetScoreByIdQueryHandler : IRequestHandler<GetScoreByIdQuery, ApiResponse<Score>>
    {
        private readonly IScoreRepository _repo;
        public GetScoreByIdQueryHandler(IScoreRepository repo) => _repo = repo;
        public async Task<ApiResponse<Score>> Handle(GetScoreByIdQuery request, CancellationToken token)
        {
            var result = await _repo.GetByIdAsync(request.Id);
            return new ApiResponse<Score> { Success = result != null, Data = result! };
        }
    }
}
