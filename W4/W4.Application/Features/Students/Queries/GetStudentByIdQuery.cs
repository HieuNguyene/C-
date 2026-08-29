using MediatR;
using Microsoft.Extensions.Logging;
using W4.Application.DTOs;
using W4.Domain.Entities;
using W4.Application.Interfaces;

namespace W4.Application.Features.Students.Queries
{
    public class GetStudentByIdQuery : IRequest<ApiResponse<StudentResponse>>
    {
        public Guid Id { get; set; }
        public GetStudentByIdQuery(Guid id)
        {
            Id = id;
        }
    }
    public class GetStudentByIdQueryHandler : IRequestHandler<GetStudentByIdQuery, ApiResponse<StudentResponse>>
    {
        private readonly ILogger<GetStudentByIdQueryHandler> _logger;
        private readonly IStudentRepository _repository;

        public GetStudentByIdQueryHandler(ILogger<GetStudentByIdQueryHandler> logger, IStudentRepository repository)
        {
            _logger = logger;
            _repository = repository;
        }
        public async Task<ApiResponse<StudentResponse>> Handle(GetStudentByIdQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Get student: id={Id}", request.Id);
            Student? student = await _repository.GetByIdAsync(request.Id);
            if (student == null)
            {
                _logger.LogWarning("Student not found. Id={Id}", request.Id);
                throw new KeyNotFoundException("Student not found");
            }
            return new ApiResponse<StudentResponse>
            {
                Success = true,
                Message = "Success",
                Data = new StudentResponse()
                {
                    Id = student.Id,
                    Name = student.Name,
                    Dob = student.DateOfBirth,
                    Gender = student.Gender,
                    ClassId = student.ClassId
                }
            };
        }
    }
}
