using Microsoft.Extensions.Logging;
using W4.Service.DTOs;
using W4.Common.Responses;
using W4.Model.Entities;

namespace W4.Service.Interfaces
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






