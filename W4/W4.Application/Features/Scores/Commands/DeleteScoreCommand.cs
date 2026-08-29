using MediatR;
using System;
using W4.Application.DTOs;
using W4.Infrastructure.Repositories.Interfaces;

namespace W4.Application.Features.Scores.Commands
{
    public class DeleteScoreCommand : IRequest<ApiResponse<bool>>
    {
        public Guid Id { get; set; }
        public DeleteScoreCommand(Guid id) => Id = id;
    }
    public class DeleteScoreCommandHandler : IRequestHandler<DeleteScoreCommand, ApiResponse<bool>>
    {
        private readonly IScoreRepository _repo;
        public DeleteScoreCommandHandler(IScoreRepository repo) => _repo = repo;
        public async Task<ApiResponse<bool>> Handle(DeleteScoreCommand request, CancellationToken token)
        {
            var result = await _repo.DeleteAsync(request.Id);
            return new ApiResponse<bool> { Success = result, Data = result };
        }
    }
}
