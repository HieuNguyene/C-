using W4.Application.DTOs;
using W4.Application.Validations;
using W4.Infrastructure.Repositories.Interfaces;
using Microsoft.Extensions.Logging;
using W4.Application.DTOs;
using W4.Domain.Entities;

namespace W4.Application.Interfaces
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













