using MediatR;
using W4.Application.DTOs;
using W4.Application.Interfaces;
using W4.Domain.Entities;
using W4.Domain.Enums;
using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
namespace W4.Application.Features.Students.Commands
{
    // Dữ liệu đầu vào
    public class CreateStudentCommand : IRequest<ApiResponse<StudentResponse>>
    {
        public string Name { get; set; } = string.Empty;
        public DateTime Dob { get; set; }
        public GenderType Gender { get; set; }
        
        private string? _classId;
        public string? ClassId 
        { 
            get => _classId; 
            set => _classId = string.IsNullOrWhiteSpace(value) ? null : value; 
        }
    }
    // Nơi xử lí logic
    public class CreateStudentCommandHandler : IRequestHandler<CreateStudentCommand, ApiResponse<StudentResponse>>
    {
        private readonly IStudentRepository _studentRepository;
        private readonly ILogger<CreateStudentCommandHandler> _logger;
        public CreateStudentCommandHandler(IStudentRepository studentRepository, ILogger<CreateStudentCommandHandler> logger)
        {
            _studentRepository = studentRepository;
            _logger = logger;
        }
        public async Task<ApiResponse<StudentResponse>> Handle(CreateStudentCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Đang tạo mới một sinh viên");
            var student = new Student(Guid.NewGuid(), request.Name, request.Dob, request.Gender, request.ClassId);

            var createdStudent = await _studentRepository.CreateStudentAsync(student);
            _logger.LogInformation("Đã tạo thành công một sinh viên");
            var responseData = new StudentResponse
            {
                Id = createdStudent.Id,
                Name = createdStudent.Name,
                Dob = createdStudent.DateOfBirth,
                Gender = createdStudent.Gender,
                ClassId = createdStudent.ClassId
            };

            return new ApiResponse<StudentResponse> { Success = true, Data = responseData, Message = "Tạo sinh viên thành công" };
        }
    }
}
