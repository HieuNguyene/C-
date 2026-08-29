using W4.Application.Interfaces;
using Microsoft.EntityFrameworkCore;
using W4.Infrastructure.Data;
using W4.Domain.Entities;
namespace W4.Infrastructure.Repositories.Implementations
{
    public class ClassRepository : IClassRepository
    {
        private readonly ApplicationDbContext _context;
        public ClassRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<Class> CreateAsync(Class Class)
        {
            _context.Add(Class);
            await _context.SaveChangesAsync();
            return Class;
        }

        public async Task<bool> DeleteByIdAsync(string ClassId)
        {
            Class? @class = await GetByIdAsync(ClassId);
            if (@class == null)
            {
                return false;
            }
            _context.Remove(@class);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<Class>> GetAllClassAsync()
        {
            return await _context.Classes.AsNoTracking().ToListAsync();
        }

        public async Task<Class?> GetByIdAsync(string ClassId)
        {
            return await _context.Classes.FirstOrDefaultAsync(c => c.ClassId == ClassId);
        }

        public async Task<bool> UpdateAsync(Class newClass)
        {
            _context.Update(newClass);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}











