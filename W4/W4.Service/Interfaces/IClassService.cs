using Microsoft.Extensions.Logging;
using W4.Service.DTOs;
using W4.Common.Responses;
using W4.Model.Entities;
namespace W4.Service.Interfaces
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





