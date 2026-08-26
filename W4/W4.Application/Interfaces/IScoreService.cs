using W4.Application.DTOs;
using W4.Application.Validations;
using W4.Infrastructure.Repositories.Interfaces;
using Microsoft.Extensions.Logging;
using W4.Application.DTOs;
using W4.Domain.Entities;

namespace W4.Application.Interfaces
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












