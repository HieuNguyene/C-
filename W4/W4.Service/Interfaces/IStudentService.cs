using Microsoft.Extensions.Logging;
using W4.Service.DTOs.Request;
using W4.Service.DTOs.Respones;
using W4.Common.Responses;
namespace W4.Service.Interfaces
{
    public interface IStudentService
    {
        Task<ApiResponse<List<StudentResponse>>> GetByKeyWordAsync(StudentQueryRequest request);

        Task<ApiResponse<StudentResponse>> GetByIdAsync(Guid id);

        Task<ApiResponse<StudentResponse>> CreateAsync(CreateStudentRequest request);

        Task<ApiResponse<bool>> UpdateByIdAsync(Guid id,UpdateStudentRequest request);

        Task<ApiResponse<bool>> DeleteByIdAsync(Guid id);
    }
}





