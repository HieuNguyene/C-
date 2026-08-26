using W4.Application.DTOs;
using W4.Application.Validations;
using W4.Infrastructure.Repositories.Interfaces;
using Microsoft.Extensions.Logging;
using System.Reflection.Metadata.Ecma335;


using W4.Infrastructure.Repositories.Interfaces;
using W4.Application.Interfaces;
using W4.Domain.Entities;

namespace W4.Application.Implementations
{
    public class StudentService : IStudentService
    {
        private readonly ILogger<StudentService> _logger;
        private readonly IStudentRepository _repository;

        public StudentService(ILogger<StudentService> logger, IStudentRepository repository)
        {
            _logger = logger;
            _repository = repository;
        }
        public async Task<ApiResponse<List<StudentResponse>>> GetByKeyWordAsync(StudentQueryRequest request)
        {
            _logger.LogInformation("Get all students constain {keyword}", request.Keyword);

            var query = await _repository.GetStudentByKeyWordAsync(request.Keyword, request.PageSize, request.Page);

            var data = query.Select(s => new StudentResponse
            {
                Id = s.Id,
                Name = s.Name,
                Dob = s.DateOfBirth,
                Gender = s.Gender,
                ClassId = s.ClassId
            }).ToList();
            return new ApiResponse<List<StudentResponse>>()
            {
                Success = true,
                Message = data.Any() ? "Success" : "No student found",
                Data = data
            };

        }
        public async Task<ApiResponse<StudentResponse>> CreateAsync(CreateStudentRequest request)
        {
            _logger.LogInformation("Create student: Name ={Name}", request.Name);

            Student student = new Student(Guid.NewGuid(), request.Name, request.Dob, request.Gender, request.ClassId);
            await _repository.CreateStudentAsync(student);
            return new ApiResponse<StudentResponse>()
            {
                Success = true,
                Message = "Tạo sinh viên thành công",
                Data = new StudentResponse()
                {
                    Id = student.Id,
                    Name = request.Name,
                    Dob = request.Dob,
                    Gender = request.Gender,
                    ClassId = request.ClassId
                }
            };
        }
        public async Task<ApiResponse<StudentResponse>> GetByIdAsync(Guid id)
        {
            _logger.LogInformation("Get student: id={Id}", id);
            Student? student = await _repository.GetByIdAsync(id);
            if (student == null)
            {
                _logger.LogWarning("Student not found. Id={Id}", id);
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
        public async Task<ApiResponse<bool>> UpdateByIdAsync(Guid id, UpdateStudentRequest request)
        {
            _logger.LogInformation("Update Student: Id={Id}", id);
            Student? student = await _repository.GetByIdAsync(id);
            if (student == null)
            {
                _logger.LogWarning("Student not found. Id={Id}", id);
                throw new KeyNotFoundException("Sinh viên này không tồn tại");
            }
            student.ChangeName(request.Name ?? student.Name);
            student.ChangeDob(request.Dob ?? student.DateOfBirth);
            student.ChangeGender(request.Gender ?? student.Gender);
            await _repository.UpdateAsync(student);
            return new ApiResponse<bool>
            {
                Success = true,
                Message = "Cập nhật thành công!",
                Data = true
            };
        }
        public async Task<ApiResponse<bool>> DeleteByIdAsync(Guid id)
        {
            _logger.LogInformation("Delete Student: Id={Id}", id);
            Student? student = await _repository.GetByIdAsync(id);
            if (student == null)
            {
                _logger.LogWarning("Not found Student: Id={Id}", id);
                return new ApiResponse<bool>()
                {
                    Success = false,
                    Message = "Xóa sinh viên thất bại",
                };
            }
            await _repository.DeleteByIdAsync(id);
            return new ApiResponse<bool>()
            {
                Success = true,
                Message = "Xóa sinh viên thành công",
                Data = true
            };
        }


    }
}













