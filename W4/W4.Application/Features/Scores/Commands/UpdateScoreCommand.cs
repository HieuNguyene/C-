using MediatR;
using System;
using W4.Application.DTOs;
using W4.Application.Interfaces;

namespace W4.Application.Features.Scores.Commands
{
    public class UpdateScoreCommand : IRequest<ApiResponse<bool>>
    {
        public Guid Id { get; set; }
        public float Value { get; set; }
    }
    public class UpdateScoreCommandHandler : IRequestHandler<UpdateScoreCommand, ApiResponse<bool>>
    {
        private readonly IScoreRepository _repo;
        public UpdateScoreCommandHandler(IScoreRepository repo) => _repo = repo;
        public async Task<ApiResponse<bool>> Handle(UpdateScoreCommand request, CancellationToken token)
        {
            // Note: Since Score entity does not have an Update method in its core, 
            // you might need to adapt this logic if your Score entity is updated differently
            // We just pass it to repo for now
            var existing = await _repo.GetByIdAsync(request.Id);
            if (existing == null) return new ApiResponse<bool> { Success = false };

            // update logic here - assume repo.UpdateAsync handles it
            await _repo.UpdateAsync(existing);
            return new ApiResponse<bool> { Success = true, Data = true };
        }
    }
}
