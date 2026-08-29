using MediatR;
using System;
using W4.Application.DTOs;
using W4.Infrastructure.Repositories.Interfaces;
using W4.Domain.Entities;

namespace W4.Application.Features.Scores.Commands
{
    public class CreateScoreCommand : IRequest<ApiResponse<Score>>
    {
        public float Value { get; set; }
        public Guid StudentId { get; set; }
        public string SubjectId { get; set; } = string.Empty;
    }
    public class CreateScoreCommandHandler : IRequestHandler<CreateScoreCommand, ApiResponse<Score>>
    {
        private readonly IScoreRepository _repo;
        public CreateScoreCommandHandler(IScoreRepository repo) => _repo = repo;
        public async Task<ApiResponse<Score>> Handle(CreateScoreCommand request, CancellationToken token)
        {
            var newEntity = new Score(Guid.NewGuid(), request.Value, request.StudentId, request.SubjectId);
            var result = await _repo.CreateAsync(newEntity);
            return new ApiResponse<Score> { Success = true, Data = result };
        }
    }
}
