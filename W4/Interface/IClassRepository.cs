using W3.model;
using W4.model;

namespace W4.Interface
{
    public interface IClassRepository
    {
        Task<Class> CreateAsync(Class Class);
        Task<List<Class>> GetAllClassAsync();
        Task<Class?> GetByIdAsync(String ClassId);
        Task<bool> UpdateAsync(Class newClass);
        Task<bool> DeleteByIdAsync(String ClassId);
        
    }
}