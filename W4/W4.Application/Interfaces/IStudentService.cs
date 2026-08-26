using W4.Application.DTOs;
using W4.Application.Validations;
using W4.Infrastructure.Repositories.Interfaces;
using Microsoft.Extensions.Logging;


namespace W4.Application.Interfaces
{
    public interface IStudentService
    {
        Task<ApiResponse<List<StudentResponse>>> GetByKeyWordAsync(StudentQueryRequest request);

        Task<ApiResponse<StudentResponse>> GetByIdAsync(Guid id);

        Task<ApiResponse<StudentResponse>> CreateAsync(CreateStudentRequest request);

        Task<ApiResponse<bool>> UpdateByIdAsync(Guid id, UpdateStudentRequest request);

        Task<ApiResponse<bool>> DeleteByIdAsync(Guid id);
    }
}













