using W3.DTOs;
using W3.Responses;
using W4.model;

namespace W4.Interface
{
    public interface IScoreService
    {
        Task<ApiResponse<Score>> CreateAsync(CreateScoreRequest request);
        Task<ApiResponse<List<Score>>> GetScoreByStudentAsync(Guid studentId);
        Task<ApiResponse<List<Score>>> GetScoreBySubjectAsync(string subjectId);
        Task<ApiResponse<bool>> UpdateAsync(CreateScoreRequest request);
        Task<ApiResponse<bool>> DeleteAsync(Guid id);
        
    }
}