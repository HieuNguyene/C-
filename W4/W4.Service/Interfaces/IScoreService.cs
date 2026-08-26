using Microsoft.Extensions.Logging;
using W4.Service.DTOs;
using W4.Common.Responses;
using W4.Model.Entities;

namespace W4.Service.Interfaces
{
    public interface IScoreService
    {
        Task<ApiResponse<Score>> CreateAsync(CreateScoreRequest request);
        Task<ApiResponse<List<Score>>> GetScoreByStudentAsync(Guid studentId);
        Task<ApiResponse<List<Score>>> GetScoreBySubjectAsync(string subjectId);
        Task<ApiResponse<bool>> UpdateAsync(Guid scoreId, UpdateScoreRequest request);
        Task<ApiResponse<bool>> DeleteAsync(Guid id);

    }
}





