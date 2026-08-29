using MediatR;
using W4.Application.DTOs;
using W4.Application.Interfaces;

namespace W4.Application.Features.Classes.Commands
{
    public class DeleteClassCommand : IRequest<ApiResponse<bool>>
    {
        public string ClassId { get; set; }
        public DeleteClassCommand(string id) => ClassId = id;
    }
    public class DeleteClassCommandHandler : IRequestHandler<DeleteClassCommand, ApiResponse<bool>>
    {
        private readonly IClassRepository _repo;
        public DeleteClassCommandHandler(IClassRepository repo) => _repo = repo;
        public async Task<ApiResponse<bool>> Handle(DeleteClassCommand request, CancellationToken token)
        {
            var result = await _repo.DeleteByIdAsync(request.ClassId);
            return new ApiResponse<bool> { Success = result, Data = result };
        }
    }
}
