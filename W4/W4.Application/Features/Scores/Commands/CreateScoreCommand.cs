using MediatR;
using System;
using W4.Application.DTOs;
using W4.Application.Interfaces;
using W4.Domain.Entities;
using W4.Application.DTOs.Responses;

using AutoMapper;

namespace W4.Application.Features.Scores.Commands
{
    public class CreateScoreCommand : IRequest<ApiResponse<ScoreResponse>>
    {
        public float Value { get; set; }
        public Guid StudentId { get; set; }
        public string SubjectId { get; set; } = string.Empty;
    }
    public class CreateScoreCommandHandler(IScoreRepository repo, IMapper mapper) : IRequestHandler<CreateScoreCommand, ApiResponse<ScoreResponse>>
    {
        public async Task<ApiResponse<ScoreResponse>> Handle(CreateScoreCommand request, CancellationToken token)
        {
            var newEntity = new Score(Guid.NewGuid(), request.Value, request.StudentId, request.SubjectId);
            var result = await repo.CreateAsync(newEntity);
            var response = mapper.Map<ScoreResponse>(result);
            return new ApiResponse<ScoreResponse> { Success = true, Data = response };
        }
    }
}
