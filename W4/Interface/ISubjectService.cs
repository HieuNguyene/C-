using W3.DTOs;
using W3.Responses;
using W4.model;

namespace W4.Interface
{
    public interface ISubjectService
    {
        Task<ApiResponse<Subject>> CreateAsync(CreateSubjectRequest request);
        Task<ApiResponse<List<Subject>>> GetAllAsync();
        Task<ApiResponse<Subject>> GetByIdAsync(string subjectId);
        Task<ApiResponse<bool>> UpdateAsync(string subjectId, UpdateSubjectRequest request);
        Task<ApiResponse<bool>> DeleteAsync(string subjectId);
    }
}
