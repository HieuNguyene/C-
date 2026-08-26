using W4.Infrastructure.Repositories.Interfaces;
using W4.Domain.Entities;

namespace W4.Infrastructure.Repositories.Interfaces
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












