using W4.Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using W4.Infrastructure.Data;
using W4.Domain.Entities;

namespace W4.Infrastructure.Repositories.Implementations
{
    public class ScoreRepository : IScoreRepository
    {
        private readonly ApplicationDbContext _context;
        public ScoreRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Score> CreateAsync(Score score)
        {
            _context.Scores.Add(score);
            await _context.SaveChangesAsync();
            return score;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {

            // Xóa trực tiếp dưới Database mà không cần tốn công lấy dữ liệu lên bộ nhớ trước
            int rowsDeleted = await _context.Scores.Where(sc => sc.Id == id).ExecuteDeleteAsync();
            return rowsDeleted > 0;
        }

        public async Task<Score?> GetByIdAsync(Guid id)
        {
            return await _context.Scores.FirstOrDefaultAsync(sc => sc.Id == id);
        }

        public async Task<List<Score>> GetScoreBySubjectAsync(string subjectId)
        {
            // Tối ưu: Dùng AsNoTracking cho các truy vấn chỉ để ĐỌC (Tăng tốc độ)
            return await _context.Scores.AsNoTracking().Where(sc => sc.SubjectId == subjectId).ToListAsync();
        }

        public async Task<List<Score>> GetScoresByStudentAsync(Guid studentId)
        {
            // Tối ưu: Dùng AsNoTracking cho các truy vấn chỉ để ĐỌC (Tăng tốc độ)
            return await _context.Scores.AsNoTracking().Where(sc => sc.StudentId == studentId).ToListAsync();
        }

        public async Task<Score?> GetSpecificScoreAsync(Guid studentId, string subjectId)
        {
            return await _context.Scores.FirstOrDefaultAsync(sc => sc.StudentId == studentId && sc.SubjectId == subjectId);
        }

        public async Task<bool> UpdateAsync(Score score)
        {
            _context.Scores.Update(score);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}











