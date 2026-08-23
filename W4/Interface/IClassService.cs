using W3.DTOs;
using W3.Responses;
using W4.model;
namespace W4.Interface
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