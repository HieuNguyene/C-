using W4.Infrastructure.Repositories.Interfaces;
using W4.Domain.Entities;

namespace W4.Infrastructure.Repositories.Interfaces
{
    public interface ISubjectRepository
    {
        Task<Subject> CreateAsync(Subject subject);
        Task<List<Subject>> GetAllAsync();
        Task<bool> DeleteAsync(string subjecId);
        Task<bool> UpdateAsync(Subject subject);
        Task<Subject?> GetByIdAsync(string subjecId);
    }
}












