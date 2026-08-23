using Microsoft.EntityFrameworkCore;
using W4.Data;
using W4.Interface;
using W4.model;

namespace W4.Repository
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
            Score? score = await GetByIdAsync(id);
            if(score == null)
            {
                throw new KeyNotFoundException("Điểm số không tồn tại");
            }
            _context.Scores.Remove(score);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<Score?> GetByIdAsync(Guid id)
        {
            return await _context.Scores.FirstOrDefaultAsync( sc => sc.Id ==id);
        }

        public async Task<List<Score>> GetScoreBySubjectAsync(string subjectId)
        {
            return await  _context.Scores.Where(sc => sc.SubjectId == subjectId).ToListAsync();

        }

        public async Task<List<Score>> GetScoresByStudentAsync(Guid studentId)
        {
            return await _context.Scores.Where(sc => sc.StudentId ==studentId).ToListAsync();
        }

        public async Task<Score?> GetSpecificScoreAsync(Guid studentId, string subjectId)
        {
            return await _context.Scores.FirstOrDefaultAsync(sc => sc.StudentId ==studentId && sc.SubjectId ==subjectId);
        }

        public async Task<bool> UpdateAsync(Score score)
        {
            _context.Scores.Update(score);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}