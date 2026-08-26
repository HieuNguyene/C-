using W4.Application.DTOs;
using W4.Application.Validations;
using W4.Infrastructure.Repositories.Interfaces;
using Microsoft.Extensions.Logging;
using W4.Application.DTOs;
using W4.Domain.Entities;
namespace W4.Application.Interfaces
{
    public interface IClassService
    {
        Task<ApiResponse<List<Class>>> GetAllAsync();
        Task<ApiResponse<Class>> GetByIdAsync(string ClassId);
        Task<ApiResponse<Class>> CreateAsync(CreateClassRequest newClass);
        Task<ApiResponse<bool>> UpdateAsync(string id, CreateClassRequest newClass);
        Task<ApiResponse<bool>> DeleteAsync(string id);
    }
}












