using W4.model;
namespace W4.Interface
{
    public interface IScoreRepository
    {
        Task<Score> CreateAsync(Score score);
        Task<bool> UpdateAsync(Score score);
        Task<bool> DeleteAsync(Guid id);
        Task<Score?> GetByIdAsync(Guid id);

        Task<List<Score>> GetScoresByStudentAsync(Guid Student);

        Task<List<Score>> GetScoreBySubjectAsync(string subjectId);

        Task<Score?> GetSpecificScoreAsync(Guid studentId,string subjectId);
    }    
}