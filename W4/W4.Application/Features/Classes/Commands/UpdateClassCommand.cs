using MediatR;
using W4.Application.DTOs;
using W4.Infrastructure.Repositories.Interfaces;

namespace W4.Application.Features.Classes.Commands
{
    public class UpdateClassCommand : IRequest<ApiResponse<bool>>
    {
        public string ClassId { get; set; } = string.Empty;
        public string ClassName { get; set; } = string.Empty;
    }
    public class UpdateClassCommandHandler : IRequestHandler<UpdateClassCommand, ApiResponse<bool>>
    {
        private readonly IClassRepository _repo;
        public UpdateClassCommandHandler(IClassRepository repo) => _repo = repo;
        public async Task<ApiResponse<bool>> Handle(UpdateClassCommand request, CancellationToken token)
        {
            var existing = await _repo.GetByIdAsync(request.ClassId);
            if (existing == null) return new ApiResponse<bool> { Success = false };
            existing.UpdateClassName(request.ClassName);
            await _repo.UpdateAsync(existing);
            return new ApiResponse<bool> { Success = true, Data = true };
        }
    }
}
