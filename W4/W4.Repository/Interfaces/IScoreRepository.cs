using W4.Model.Entities;
namespace W4.Repository.Interfaces
{
    public interface IScoreRepository
    {
        Task<Score> CreateAsync(Score score);
        Task<bool> UpdateAsync(Score score);
        Task<bool> DeleteAsync(Guid id);
        Task<Score?> GetByIdAsync(Guid id);

        Task<List<Score>> GetScoresByStudentAsync(Guid Student);

        Task<List<Score>> GetScoreBySubjectAsync(string subjectId);

        Task<Score?> GetSpecificScoreAsync(Guid studentId, string subjectId);
    }
}



