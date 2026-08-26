using W4.Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using W4.Infrastructure.Data;
using W4.Domain.Entities;

namespace W4.Infrastructure.Repositories.Implementations
{
    public class SubjectRepository : ISubjectRepository
    {
        private readonly ApplicationDbContext _context;

        public SubjectRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Subject> CreateAsync(Subject subject)
        {
            _context.Subjects.Add(subject);
            await _context.SaveChangesAsync();
            return subject;
        }

        public async Task<bool> DeleteAsync(string subjectId)
        {
            // Sử dụng ExecuteDeleteAsync để tối ưu hiệu năng xóa
            int rowsDeleted = await _context.Subjects.Where(s => s.SubjectId == subjectId).ExecuteDeleteAsync();
            return rowsDeleted > 0;
        }

        public async Task<List<Subject>> GetAllAsync()
        {
            // Thêm AsNoTracking vì chỉ lấy ra để xem
            return await _context.Subjects.AsNoTracking().ToListAsync();
        }

        public async Task<Subject?> GetByIdAsync(string subjectId)
        {
            return await _context.Subjects.FirstOrDefaultAsync(s => s.SubjectId == subjectId);
        }

        public async Task<bool> UpdateAsync(Subject subject)
        {
            _context.Subjects.Update(subject);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}











