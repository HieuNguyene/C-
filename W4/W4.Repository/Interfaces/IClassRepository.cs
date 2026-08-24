using W4.Model.Entities;
using W4.Model.Entities;

namespace W4.Repository.Interfaces
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



