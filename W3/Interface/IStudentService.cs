using W3.DTOs.Request;
using W3.DTOs.Respones;
using W3.Responses;
namespace W3.Interface
{
    public interface IStudentService
    {
        ApiResponse<List<StudentResponse>> GetAll(StudentQueryRequest request);

        ApiResponse<StudentResponse> GetById(Guid id);

        ApiResponse<StudentResponse> Create(CreateStudentRequest request);

        ApiResponse<bool> UpdateById(Guid id, UpdateStudentRequest request);

        ApiResponse<bool> DeleteById(Guid id);
    }
}
