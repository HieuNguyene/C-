using MediatR;
using Microsoft.Extensions.Logging;
using W4.Application.DTOs;
using W4.Application.Interfaces;

namespace W4.Application.Features.Students.Queries
{
    public class GetAllStudentQuery : IRequest<ApiResponse<List<StudentResponse>>>
    {

    }
    public class GetAllStudentQueryHandler : IRequestHandler<GetAllStudentQuery, ApiResponse<List<StudentResponse>>>
    {
        private readonly ILogger<GetAllStudentQueryHandler> _logger;
        private readonly IStudentRepository _repository;

        public GetAllStudentQueryHandler(ILogger<GetAllStudentQueryHandler> logger, IStudentRepository repository)
        {
            _logger = logger;
            _repository = repository;
        }
        public async Task<ApiResponse<List<StudentResponse>>> Handle(GetAllStudentQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Lấy toàn bộ danh sách sinh viên");
            var query = await _repository.GetAllStudentAsync();
            var data = query.Select(s => new StudentResponse
            {
                ClassId = s.ClassId,
                Id = s.Id,
                Name = s.Name,
                Dob = s.DateOfBirth,
                Gender = s.Gender
            }).ToList();
            return new ApiResponse<List<StudentResponse>>
            {
                Success = true,
                Message = "Đã lấy thành công danh sách sinh viên",
                Data = data
            };
        }
    }
}
