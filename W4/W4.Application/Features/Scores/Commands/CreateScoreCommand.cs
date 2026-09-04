using MediatR;
using System;
using W4.Application.DTOs;
using W4.Application.Interfaces;
using W4.Domain.Entities;
using W4.Application.DTOs.Responses;

namespace W4.Application.Features.Scores.Commands
{
    public class CreateScoreCommand : IRequest<ApiResponse<ScoreResponse>>
    {
        public float Value { get; set; }
        public Guid StudentId { get; set; }
        public string SubjectId { get; set; } = string.Empty;
    }
    public class CreateScoreCommandHandler : IRequestHandler<CreateScoreCommand, ApiResponse<ScoreResponse>>
    {
        private readonly IScoreRepository _repo;
        public CreateScoreCommandHandler(IScoreRepository repo) => _repo = repo;
        public async Task<ApiResponse<ScoreResponse>> Handle(CreateScoreCommand request, CancellationToken token)
        {
            var newEntity = new Score(Guid.NewGuid(), request.Value, request.StudentId, request.SubjectId);
            var result = await _repo.CreateAsync(newEntity);
            var response = new ScoreResponse { Id = result.Id, Value = result.Value, StudentId = result.StudentId, SubjectId = result.SubjectId };
            return new ApiResponse<ScoreResponse> { Success = true, Data = response };
        }
    }
}
