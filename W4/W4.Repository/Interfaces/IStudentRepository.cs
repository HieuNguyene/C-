using W4.Model.Entities;
using W4.Common.Responses;

namespace W4.Repository.Interfaces
{
    public interface IStudentRepository
    {
        Task<Student> CreateStudentAsync(Student student);
        Task<List<Student>> GetAllStudentAsync();
        Task<Student?> GetByIdAsync(Guid id);
        Task<List<Student>> GetStudentByKeyWordAsync(string? keyword, int pageSize, int pageNumber);
        Task<Student> UpdateAsync(Student newStudent);
        Task<bool> DeleteByIdAsync(Guid id);
    }
}



