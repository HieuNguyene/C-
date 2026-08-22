using W3.DTOs.Request;
using W3.DTOs.Respones;
using W3.Responses;
namespace W3.Interface
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
